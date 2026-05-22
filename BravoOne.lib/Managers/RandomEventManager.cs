using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class RandomEventManager : BaseManager
    {
        // Chance of any event firing each turn (out of 100).
        private const int EVENT_CHANCE = 40;

        public RandomEventManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override Task<Game> InitializeAsync(Game currentGame) => Task.FromResult(currentGame);

        public override Task<(TurnStatus Status, Game CurrentGame)> ProcessTurnAsync(Game currentGame)
        {
            var rng = new Random();

            if (rng.Next(0, 100) >= EVENT_CHANCE)
                return Task.FromResult((TurnStatus.OK, currentGame));

            var eventType = (RandomEventType)rng.Next(1, Enum.GetValues(typeof(RandomEventType)).Length);

            switch (eventType)
            {
                case RandomEventType.IntelTip:
                    ApplyIntelTip(rng, currentGame);
                    break;

                case RandomEventType.EquipmentMalfunction:
                    ApplyEquipmentMalfunction(rng, currentGame);
                    break;

                case RandomEventType.RivalPMC:
                    ApplyRivalPMC(rng, currentGame);
                    break;

                case RandomEventType.RecruitmentDrive:
                    ApplyRecruitmentDrive(rng, currentGame);
                    break;

                case RandomEventType.MediaCoverage:
                    ApplyMediaCoverage(currentGame);
                    break;
            }

            return Task.FromResult((TurnStatus.OK, currentGame));
        }

        private static void ApplyIntelTip(Random rng, Game currentGame)
        {
            if (!currentGame.Contracts.Any()) return;

            var idx = rng.Next(0, currentGame.Contracts.Count);
            var contract = currentGame.Contracts[idx];

            contract.CompleteDate = contract.CompleteDate.AddMonths(1);

            var msg = $"Intel tip received — deadline for {contract.Name} extended by 1 month";
            currentGame.AddActivityLog(ActivityType.CONTRACT_ACCEPTED, "INTEL: Deadline Extended", msg);
            if (currentGame.LastTurnSummary != null)
                currentGame.LastTurnSummary.RandomEventDescription = msg;
        }

        private static void ApplyEquipmentMalfunction(Random rng, Game currentGame)
        {
            var equipped = currentGame.TeamMembers
                .Where(m => m.Status == TeamMemberStatus.OnTeam && m.Equipment.Count > 0)
                .ToList();

            if (!equipped.Any()) return;

            var member = equipped[rng.Next(0, equipped.Count)];
            var slot = member.Equipment[rng.Next(0, member.Equipment.Count)];
            slot.Status = Math.Max(0, slot.Status - 30);

            var eq = currentGame.TeamEquipment.FirstOrDefault(e => e.Id == slot.EquipmentId);
            var name = eq?.Name ?? "equipment";
            var msg = $"Field malfunction — {member.Name}'s {name} degraded by 30 points";
            currentGame.AddActivityLog(ActivityType.CONTRACT_FAILED, "EVENT: Equipment Failure", msg);
            if (currentGame.LastTurnSummary != null)
                currentGame.LastTurnSummary.RandomEventDescription = msg;
        }

        private static void ApplyRivalPMC(Random rng, Game currentGame)
        {
            if (!currentGame.AvailableContracts.Any()) return;

            var idx = rng.Next(0, currentGame.AvailableContracts.Count);
            var stolen = currentGame.AvailableContracts[idx];
            currentGame.AvailableContracts.RemoveAt(idx);

            var msg = $"Rival PMC intercepted contract {stolen.Name} before you could accept it";
            currentGame.AddActivityLog(ActivityType.CONTRACT_FAILED, "EVENT: Contract Poached", msg);
            if (currentGame.LastTurnSummary != null)
                currentGame.LastTurnSummary.RandomEventDescription = msg;
        }

        private static void ApplyRecruitmentDrive(Random rng, Game currentGame)
        {
            var available = currentGame.TeamMembers
                .Where(m => m.Status == TeamMemberStatus.Available)
                .ToList();

            if (!available.Any()) return;

            var recruit = available[rng.Next(0, available.Count)];
            // Offer a one-turn 25% salary discount.
            var discount = recruit.MonthlySalary / 4;
            recruit.MonthlySalary -= discount;

            var msg = $"Recruitment drive: {recruit.Name} is offering their services at a discounted rate this month";
            currentGame.AddActivityLog(ActivityType.TEAM_MEMBER_HIRED, "EVENT: Recruitment Drive", msg);
            if (currentGame.LastTurnSummary != null)
                currentGame.LastTurnSummary.RandomEventDescription = msg;
        }

        private static void ApplyMediaCoverage(Game currentGame)
        {
            // Positive press: the team gains a free XP boost represented as a small cash bonus.
            var bonus = (ulong)(currentGame.TeamLevel * 5000);
            currentGame.Money += bonus;

            var msg = $"Positive media coverage of your operations — sponsors contributed ${bonus}";
            currentGame.AddActivityLog(ActivityType.TEAM_LEVEL_UP, "EVENT: Media Coverage", msg);
            if (currentGame.LastTurnSummary != null)
                currentGame.LastTurnSummary.RandomEventDescription = msg;
        }
    }
}
