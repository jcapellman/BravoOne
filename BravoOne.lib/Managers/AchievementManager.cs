using BravoOne.lib.Achievements.Base;
using BravoOne.lib.DAL.Base;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;
using BravoOne.lib.UIObjects;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class AchievementManager : BaseManager
    {
        private readonly List<BaseAchievement> achievements;

        public AchievementManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
            achievements = typeof(AchievementManager).Assembly.GetTypes().Where(a => 
                a.BaseType == typeof(BaseAchievement) && !a.IsAbstract).Select(b => (BaseAchievement)Activator.CreateInstance(b)).ToList();
        }

        public async Task<List<AchievementsListingItem>> GetAchievementListingAsync()
        {
            var listing = new List<AchievementsListingItem>();

            var obtainedAchievements = DAL.GetAll<Objects.Achievements>();

            var achievementRootPath = await Storage.GetFullPathAsync("Achievements");

            foreach (var achievement in achievements)
            {
                var unlocked = obtainedAchievements.FirstOrDefault(a => a.AchievementType == achievement.GetType());

                var listingItem = new AchievementsListingItem
                {
                    Description = achievement.Description,
                    Unlocked = unlocked != null,
                    Title = achievement.Title,
                    TimeStamp = unlocked?.TimeStamp,
                    ImagePath = Path.Combine(achievementRootPath, $"{achievement.GetType().Name}.png"),
                    ButtonLabel = unlocked?.TimeStamp == null ? "Locked" : $"Unlocked on {unlocked?.TimeStamp.Date}"
                };

                listing.Add(listingItem);
            }

            return listing.OrderBy(a => a.Unlocked).ThenBy(a => a.TimeStamp).ToList();
        }
        
        public void CheckAchievements(Game currentGame)
        {
            var obtainedAchievements = DAL.GetAll<Objects.Achievements>();

            foreach (var achievement in achievements)
            {
                if (obtainedAchievements.Any(a => a.AchievementType == achievement.GetType()))
                {
                    continue;
                }

                var unlocked = achievement.VerifyAchievement(currentGame);

                if (!unlocked)
                {
                    continue;
                }

                var unlockedAchievement = new Objects.Achievements
                {
                    AchievementType = achievement.GetType(),
                    TimeStamp = DateTime.Now
                };

                DAL.Add(unlockedAchievement);
            }
        }
    }
}