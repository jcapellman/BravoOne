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
            foreach (TeamMember member in currentGame.TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam))
            {
                if (member.MonthlySalary > currentGame.Money)
                {
                    return (TurnStatus.OUT_OF_MONEY, currentGame);
                }

                currentGame.Money -= member.MonthlySalary;

                member.Health -= currentGame.Contracts.Where(a => a.Status == ContractStatus.InProgress && 
                    a.AssignedTeamMembers.Contains(member.Id)).Sum(c => c.TeamMemberToll);
            }

            currentGame = await InitializeAsync(currentGame);

            return (TurnStatus.OK, currentGame);
        }

        public override async Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame.TeamMembers = currentGame.TeamMembers.Where(a => a.Status != TeamMemberStatus.Available).ToList();

            var rng = new Random();
            var randomFirst = rng;
            var randomLast = rng;
            var randomSkill = rng;
            var randomSpecialty = rng;
            var randomAvatar = rng;

            var specialties = (Specialties[])Enum.GetValues(typeof(Specialties));
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

                member.Specialty = specialties[randomSpecialty.Next(0, specialties.Length)];
                member.AvatarImagePath = avatarImages[randomAvatar.Next(0, avatarImages.Count() - 1)];

                currentGame.TeamMembers.Add(member);
            }

            return currentGame;
        }
    }
}