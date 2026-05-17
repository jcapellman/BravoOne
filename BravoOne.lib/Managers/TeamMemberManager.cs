using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;
using BravoOne.lib.DAL.Base;
using BravoOne.lib.Managers.Base;

using System.Linq;
using System;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class TeamMemberManager : BaseManager
    {
        public TeamMemberManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override async Task<(TurnStatus Status, Game CurrentGame)> ProcessTurnAsync(Game currentGame)
        {
            var activeMembers = currentGame.TeamMembers
                .Where(a => a.Status == TeamMemberStatus.OnTeam || a.Status == TeamMemberStatus.Injured)
                .ToList();

            foreach (TeamMember member in activeMembers)
            {
                // Only pay salaries for active (not injured) members
                if (member.Status == TeamMemberStatus.OnTeam)
                {
                    if (member.MonthlySalary > currentGame.Money)
                    {
                        return (TurnStatus.OUT_OF_MONEY, currentGame);
                    }

                    currentGame.Money -= member.MonthlySalary;

                    member.Health -= currentGame.Contracts.Where(a => a.Status == ContractStatus.InProgress && 
                        a.AssignedTeamMembers.Contains(member.Id)).Sum(c => c.TeamMemberToll);
                }
                else if (member.Status == TeamMemberStatus.Injured)
                {
                    // Injured members recover 10 health per turn
                    member.Health = Math.Min(100, member.Health + 10);

                    if (member.Health > 20)
                    {
                        member.Status = TeamMemberStatus.OnTeam;
                    }
                }

                // Age one month; convert to years every 12 turns
                member.AgeMonths++;

                if (member.AgeMonths >= 12)
                {
                    member.Age++;
                    member.AgeMonths = 0;
                }
            }

            currentGame.ApplyHealthChanges(activeMembers);

            currentGame = await InitializeAsync(currentGame);

            return (TurnStatus.OK, currentGame);
        }

        public override async Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame.TeamMembers = currentGame.TeamMembers
                .Where(a => a.Status != TeamMemberStatus.Available &&
                            a.Status != TeamMemberStatus.Deceased)
                .ToList();

            var rng = new Random();

            var specialties = (Specialties[])Enum.GetValues(typeof(Specialties));
            var avatarImages = await Storage.GetAvatarImagesAsync();

            for (var x = 0; x < 50; x++)
            {
                var member = new TeamMember
                {
                    Health = 100,
                    Status = TeamMemberStatus.Available,
                    Id = Guid.NewGuid(),
                    Age = rng.Next(22, 45),
                    AgeMonths = rng.Next(0, 11),
                    RetirementAge = rng.Next(55, 65)
                };

                do
                {
                    var firstName = Common.Constants.FIRST_NAMES[rng.Next(0, Common.Constants.FIRST_NAMES.Length - 1)];
                    var lastName = Common.Constants.LAST_NAMES[rng.Next(0, Common.Constants.LAST_NAMES.Length - 1)];

                    member.Name = $"{firstName} {lastName}";
                } while (currentGame.TeamMembers.Any(a => a.Name == member.Name));

                member.SkillPoints = (uint)rng.Next(1, currentGame.TeamLevel + 5);

                member.MonthlySalary = 10000 * member.SkillPoints;

                member.Specialty = specialties[rng.Next(0, specialties.Length)];
                member.AvatarImagePath = avatarImages[rng.Next(0, avatarImages.Count() - 1)];

                currentGame.TeamMembers.Add(member);
            }

            return currentGame;
        }
    }
}