using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class ContractManager : BaseManager
    {
        private static readonly Dictionary<ContractType, (int DeadlineMin, int DeadlineMax, int TollMin, int TollMax)> TYPE_PARAMS =
            new Dictionary<ContractType, (int, int, int, int)>
        {
            { ContractType.RECON,            (1, 2,  3, 10) },
            { ContractType.DEMOLITION,       (1, 3,  5, 15) },
            { ContractType.SABOTAGE,         (2, 3,  8, 18) },
            { ContractType.EXTRACTION,       (1, 2,  8, 18) },
            { ContractType.RESCUE,           (1, 2, 10, 20) },
            { ContractType.ASSET_PROTECTION, (2, 4,  5, 12) },
            { ContractType.INFILTRATION,     (2, 4, 10, 20) },
            { ContractType.ASSASSINATION,    (1, 2, 12, 25) },
        };

        public ContractManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame.AvailableContracts = new ObservableCollection<Contract>();

            var rng = new Random();
            var poolSize = Math.Min(5 + currentGame.TeamLevel * 2, 20);

            for (var x = 0; x < poolSize; x++)
            {
                var contract = BuildContract(rng, currentGame);
                if (contract != null)
                    currentGame.AvailableContracts.Add(contract);
            }

            return Task.FromResult(currentGame);
        }

        public override async Task<(TurnStatus Status, Game CurrentGame)> ProcessTurnAsync(Game currentGame)
        {
            var contractsToProcess = currentGame.Contracts
                .Where(c => c.Status == ContractStatus.InProgress)
                .ToList();

            // Track which members are actively working this turn (for fatigue).
            var activeAssignments = new HashSet<Guid>();

            foreach (var contract in contractsToProcess)
            {
                var affectedMembers = new List<TeamMember>();
                var allHealthy = true;

                foreach (Guid guid in contract.AssignedTeamMembers)
                {
                    var teamMember = currentGame.TeamMembers.FirstOrDefault(a => a.Id == guid);
                    if (teamMember == null)
                        continue;

                    activeAssignments.Add(guid);

                    var brokenPenalty = teamMember.Equipment
                        .Where(te => te.Status == 0)
                        .Join(currentGame.TeamEquipment, te => te.EquipmentId, e => e.Id, (te, e) => e.Reliability)
                        .DefaultIfEmpty(0)
                        .Sum();

                    teamMember.Health -= brokenPenalty / 10;
                    if (teamMember.Health < 100) allHealthy = false;

                    var effective = teamMember.EffectiveSkillPoints(currentGame.TeamEquipment);

                    if (contract.SkillPointsRemaining > effective)
                        contract.SkillPointsRemaining -= effective;
                    else
                        contract.SkillPointsRemaining = 0;

                    affectedMembers.Add(teamMember);
                }

                foreach (Guid guid in contract.AssignedTeamMembers)
                {
                    var medic = currentGame.TeamMembers.FirstOrDefault(a =>
                        a.Id == guid && a.Specialty == Specialties.MEDIC);
                    if (medic == null)
                        continue;

                    var healing = medic.HealingValue();
                    foreach (var member in affectedMembers)
                    {
                        if (member.Id != medic.Id)
                            member.Health = Math.Min(member.MaxHealth, member.Health + healing);
                    }
                }

                currentGame.ApplyHealthChanges(affectedMembers);

                if (contract.SkillPointsRemaining == 0)
                {
                    // Performance bonus: +20% income if all assigned operators stayed healthy.
                    if (allHealthy && affectedMembers.Count > 0)
                    {
                        contract.Income = (ulong)(contract.Income * 1.20);
                        currentGame.AddActivityLog(ActivityType.CONTRACT_COMPLETED, "Clean Op Bonus",
                            $"Flawless execution on {contract.Name} — 20% bonus payout");
                        currentGame.LastTurnSummary?.ContractsCompleted.Add($"{contract.Name} (CLEAN OP +20%)");
                    }

                    // Award 1 XP to each surviving operator on the contract.
                    foreach (Guid guid in contract.AssignedTeamMembers)
                    {
                        var member = currentGame.TeamMembers.FirstOrDefault(a =>
                            a.Id == guid && a.Status == TeamMemberStatus.OnTeam);
                        if (member != null)
                            currentGame.AwardOperatorXP(member);
                    }

                    currentGame.CompleteContract(contract);
                }
                else if (currentGame.CurrentDate > contract.CompleteDate)
                {
                    currentGame.FailContract(contract);
                }
            }

            // Update fatigue: increment for active members, reset for those who rested.
            foreach (var member in currentGame.TeamMembers.Where(m => m.Status == TeamMemberStatus.OnTeam))
            {
                if (activeAssignments.Contains(member.Id))
                    member.FatigueMonths++;
                else
                    member.FatigueMonths = 0;
            }

            currentGame = await RefreshAvailableContractsAsync(currentGame);
            return (TurnStatus.OK, currentGame);
        }

        private Task<Game> RefreshAvailableContractsAsync(Game currentGame)
        {
            var rng = new Random();
            var poolSize = Math.Min(5 + currentGame.TeamLevel * 2, 20);
            var needed = poolSize - currentGame.AvailableContracts.Count;

            for (var x = 0; x < needed; x++)
            {
                var contract = BuildContract(rng, currentGame);
                if (contract != null)
                    currentGame.AvailableContracts.Add(contract);
            }

            return Task.FromResult(currentGame);
        }

        private static Contract BuildContract(Random rng, Game currentGame)
        {
            var contract = new Contract
            {
                Id = Guid.NewGuid(),
                Status = ContractStatus.NotStarted,
                SpecialtiesRequired = new Dictionary<Specialties, int>(),
                AssignedTeamMembers = new List<Guid>()
            };

            var attempts = 0;
            do
            {
                var name = Common.Constants.MISSION_NAMES[rng.Next(0, Common.Constants.MISSION_NAMES.Length)];
                var prefix = Common.Constants.MISSION_PREFIX[rng.Next(0, Common.Constants.MISSION_PREFIX.Length)];
                contract.Name = $"{prefix} {name}";
                attempts++;
            }
            while (attempts < 50 &&
                (currentGame.AvailableContracts.Any(a => a.Name == contract.Name) ||
                 currentGame.Contracts.Any(a => a.Name == contract.Name)));

            if (attempts >= 50)
                return null;

            contract.CType = (ContractType)rng.Next(0, Enum.GetValues(typeof(ContractType)).Length);

            ApplySpecialties(contract);

            var p = TYPE_PARAMS[contract.CType];

            var spMin = Math.Max(1, currentGame.TeamLevel);
            var spMax = Math.Max(spMin + 1, currentGame.TeamLevel * (currentGame.TeamMembers.Count + 1) * 5);
            contract.SkillPointsRemaining = (uint)rng.Next(spMin, spMax);
            contract.SkillPointsTotal = contract.SkillPointsRemaining;

            contract.Income = (ulong)rng.Next(
                (int)(contract.SkillPointsRemaining * 10),
                (int)(contract.SkillPointsRemaining * 25));

            contract.TeamMemberToll = rng.Next(p.TollMin, p.TollMax);

            contract.Penalty = (ulong)rng.Next(
                (int)(contract.Income / 2),
                (int)(contract.Income * 2));

            contract.DeadlineMonths = rng.Next(p.DeadlineMin, p.DeadlineMax + 1);

            var briefings = Common.Constants.CONTRACT_BRIEFINGS[contract.CType];
            contract.Briefing = briefings[rng.Next(0, briefings.Length)];

            return contract;
        }

        private static void ApplySpecialties(Contract contract)
        {
            switch (contract.CType)
            {
                case ContractType.RECON:
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                    break;
                case ContractType.INFILTRATION:
                    contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                    contract.SpecialtiesRequired.Add(Specialties.DEMOLITION, 1);
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                    break;
                case ContractType.RESCUE:
                    contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                    contract.SpecialtiesRequired.Add(Specialties.MEDIC, 1);
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    break;
                case ContractType.DEMOLITION:
                    contract.SpecialtiesRequired.Add(Specialties.DEMOLITION, 1);
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    break;
                case ContractType.EXTRACTION:
                    contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                    contract.SpecialtiesRequired.Add(Specialties.MEDIC, 1);
                    break;
                case ContractType.SABOTAGE:
                    contract.SpecialtiesRequired.Add(Specialties.DEMOLITION, 1);
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    break;
                case ContractType.ASSASSINATION:
                    contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                    contract.SpecialtiesRequired.Add(Specialties.RECON, 1);
                    break;
                case ContractType.ASSET_PROTECTION:
                    contract.SpecialtiesRequired.Add(Specialties.ASSAULT, 1);
                    contract.SpecialtiesRequired.Add(Specialties.MEDIC, 1);
                    contract.SpecialtiesRequired.Add(Specialties.SNIPER, 1);
                    break;
            }
        }
    }
}
