using BravoOne.lib;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class ManageTeamMemberViewModel : BaseViewModel
    {
        private ObservableCollection<ContractAssignmentItem> _contractAssignments;

        public ObservableCollection<ContractAssignmentItem> ContractAssignments
        {
            get => _contractAssignments;

            set
            {
                _contractAssignments = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OperatorEquipmentItem> _operatorEquipment;

        public ObservableCollection<OperatorEquipmentItem> OperatorEquipment
        {
            get => _operatorEquipment;

            set
            {
                _operatorEquipment = value;
                OnPropertyChanged();
            }
        }

        public ManageTeamMemberViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
            Refresh();
        }

        public void ToggleAssignment(Contract contract, TeamMember member)
        {
            if (contract.AssignedTeamMembers.Contains(member.Id))
                contract.AssignedTeamMembers.Remove(member.Id);
            else
                contract.AssignedTeamMembers.Add(member.Id);

            Refresh();
        }

        public void AssignEquipment(TeamMember member, Equipment equipment)
        {
            member.Equipment.Add(new TeamEquipment
            {
                Id = Guid.NewGuid(),
                EquipmentId = equipment.Id,
                Status = 100
            });

            Refresh();
        }

        public void UnassignEquipment(TeamMember member, TeamEquipment slot)
        {
            member.Equipment.Remove(slot);
            Refresh();
        }

        private void Refresh()
        {
            var onTeam = gWrapper.CurrentGame.TeamMembers
                .Where(a => a.Status == TeamMemberStatus.OnTeam)
                .OrderBy(b => b.Name)
                .ToList();

            // ── Contract assignments ──────────────────────────────────
            ContractAssignments = new ObservableCollection<ContractAssignmentItem>(
                gWrapper.CurrentGame.Contracts
                    .Where(c => c.Status == ContractStatus.InProgress)
                    .Select(c => new ContractAssignmentItem
                    {
                        Contract = c,
                        DeadlineLabel = c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate) <= 1
                            ? "⚠ OVERDUE"
                            : $"{c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate)} months left",
                        SpecialtiesLabel = string.Join(", ", c.SpecialtiesRequired.Keys.Select(k => k.ToString())),
                        SkillPointsCompleted = c.SkillPointsTotal - c.SkillPointsRemaining,
                        Members = new ObservableCollection<MemberAssignmentItem>(
                            onTeam.Select(m => new MemberAssignmentItem
                            {
                                Member = m,
                                Contract = c,
                                IsAssigned = c.AssignedTeamMembers.Contains(m.Id),
                                ButtonLabel = c.AssignedTeamMembers.Contains(m.Id) ? "UNASSIGN" : "ASSIGN",
                                SpecialtyMatchLabel = c.SpecialtiesRequired.ContainsKey(m.Specialty) ? "✓ MATCH" : string.Empty
                            }))
                    }));

            // ── Equipment assignments ─────────────────────────────────
            OperatorEquipment = new ObservableCollection<OperatorEquipmentItem>(
                onTeam.Select(m =>
                {
                    var assignedIds = new HashSet<int>(m.Equipment.Select(te => te.EquipmentId));

                    return new OperatorEquipmentItem
                    {
                        Member = m,
                        AssignedSlots = new ObservableCollection<AssignedEquipmentSlot>(
                            m.Equipment.Select(te =>
                            {
                                var eq = gWrapper.CurrentGame.TeamEquipment.FirstOrDefault(e => e.Id == te.EquipmentId);
                                return new AssignedEquipmentSlot
                                {
                                    Member = m,
                                    Slot = te,
                                    EquipmentName = eq?.Name ?? "Unknown",
                                    ConditionLabel = te.Status >= 75 ? "GOOD"
                                                   : te.Status >= 40 ? "WORN"
                                                   : "DEGRADED",
                                    ConditionColor = te.Status >= 75 ? "#FF44DD44"
                                                   : te.Status >= 40 ? "#FFFFCC44"
                                                   : "#FFFF6666"
                                };
                            })),
                        PoolItems = new ObservableCollection<PoolEquipmentItem>(
                            gWrapper.CurrentGame.TeamEquipment
                                .Select(e => new PoolEquipmentItem
                                {
                                    Member = m,
                                    Equipment = e,
                                    AlreadyAssigned = assignedIds.Contains(e.Id),
                                    ButtonLabel = assignedIds.Contains(e.Id) ? "ASSIGNED" : "ASSIGN"
                                }))
                    };
                }));
        }
    }

    public class ContractAssignmentItem
    {
        public Contract Contract { get; set; }
        public string DeadlineLabel { get; set; }
        public string SpecialtiesLabel { get; set; }
        public uint SkillPointsCompleted { get; set; }
        public ObservableCollection<MemberAssignmentItem> Members { get; set; }
    }

    public class MemberAssignmentItem
    {
        public TeamMember Member { get; set; }
        public Contract Contract { get; set; }
        public bool IsAssigned { get; set; }
        public string ButtonLabel { get; set; }
        public string SpecialtyMatchLabel { get; set; }
    }

    public class OperatorEquipmentItem
    {
        public TeamMember Member { get; set; }
        public ObservableCollection<AssignedEquipmentSlot> AssignedSlots { get; set; }
        public ObservableCollection<PoolEquipmentItem> PoolItems { get; set; }
    }

    public class AssignedEquipmentSlot
    {
        public TeamMember Member { get; set; }
        public TeamEquipment Slot { get; set; }
        public string EquipmentName { get; set; }
        public string ConditionLabel { get; set; }
        public string ConditionColor { get; set; }
    }

    public class PoolEquipmentItem
    {
        public TeamMember Member { get; set; }
        public Equipment Equipment { get; set; }
        public bool AlreadyAssigned { get; set; }
        public bool CanAssign => !AlreadyAssigned;
        public string ButtonLabel { get; set; }
    }
}
