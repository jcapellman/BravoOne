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

        public override (TurnStatus Status, Game CurrentGame) ProcessTurn(Game currentGame)
        {
            foreach (TeamMember member in currentGame.TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam))
            {
                if (member.MonthlySalary > currentGame.Money)
                {
                    return (TurnStatus.OUT_OF_MONEY, currentGame);
                }

                currentGame.Money -= member.MonthlySalary;
            }

            currentGame = InitializeAsync(currentGame).Result;

            return (TurnStatus.OK, currentGame);
        }

        public override async Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame.TeamMembers = currentGame.TeamMembers.Where(a => a.Status != TeamMemberStatus.Available).ToList();

            var randomFirst = new Random((int)DateTime.Now.Ticks);
            var randomLast = new Random((int)DateTime.Now.Ticks + 1);
            var randomSkill = new Random((int)DateTime.Now.Ticks);
            var randomSpecialty = new Random((int)DateTime.Now.Ticks);

            var randomAvatar = new Random((int)DateTime.Now.Ticks + 5);

            var specialties = Enum.GetNames(typeof(Specialties));
            var avatarImages = await Storage.GetAvatarImagesAsync();

            for (var x = 0; x < 50; x++)
            {
                var member = new TeamMember
                {
                    Health = 100,
                    Status = TeamMemberStatus.Available,
                    Id = Guid.NewGuid()
                };

                do
                {
                    var firstName = Common.Constants.FIRST_NAMES[randomFirst.Next(0, Common.Constants.FIRST_NAMES.Length - 1)];
                    var lastName = Common.Constants.LAST_NAMES[randomLast.Next(0, Common.Constants.LAST_NAMES.Length - 1)];

                    member.Name = $"{firstName} {lastName}";
                } while (currentGame.TeamMembers.Any(a => a.Name == member.Name));

                member.SkillPoints = (uint)randomSkill.Next(1, currentGame.TeamLevel + 5);

                member.MonthlySalary = 10000 * member.SkillPoints;

                member.Specialty = specialties[randomSpecialty.Next(0, specialties.Length - 1)];
                member.AvatarImagePath = avatarImages[randomAvatar.Next(0, avatarImages.Count() - 1)];

                currentGame.TeamMembers.Add(member);
            }

            return currentGame;
        }
    }
}