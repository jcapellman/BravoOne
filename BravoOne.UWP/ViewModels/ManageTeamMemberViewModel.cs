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
                    .Select(c =>
                    {
                        // Which specialties are filled (at least one assigned member matches)
                        var assignedSpecialties = new HashSet<Specialties>(
                            onTeam
                                .Where(m => c.AssignedTeamMembers.Contains(m.Id))
                                .Select(m => m.Specialty));

                        return new ContractAssignmentItem
                        {
                            Contract = c,
                            DeadlineLabel = c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate) <= 1
                                ? "⚠ OVERDUE"
                                : $"{c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate)} months left",
                            SkillPointsCompleted = c.SkillPointsTotal - c.SkillPointsRemaining,
                            AssignedCount = c.AssignedTeamMembers.Count,
                            // One badge per required specialty showing filled/missing state
                            RequirementSlots = c.SpecialtiesRequired.Keys
                                .Select(k => new RequirementSlotItem
                                {
                                    Label = k.ToString(),
                                    IsFilled = assignedSpecialties.Contains(k)
                                })
                                .ToList(),
                            // Assigned operator name-tags
                            AssignedNames = string.Join("  //  ",
                                onTeam
                                    .Where(m => c.AssignedTeamMembers.Contains(m.Id))
                                    .Select(m => m.Name)),
                            Members = new ObservableCollection<MemberAssignmentItem>(
                                onTeam
                                    .Select(m => new MemberAssignmentItem
                                    {
                                        Member = m,
                                        Contract = c,
                                        IsAssigned = c.AssignedTeamMembers.Contains(m.Id),
                                        ButtonLabel = c.AssignedTeamMembers.Contains(m.Id) ? "REMOVE" : "ASSIGN",
                                        IsMatch = c.SpecialtiesRequired.ContainsKey(m.Specialty),
                                        SpecialtyMatchLabel = c.SpecialtiesRequired.ContainsKey(m.Specialty) ? "MATCH" : string.Empty
                                    })
                                    // Assigned first, then unassigned matches, then the rest
                                    .OrderByDescending(m => m.IsAssigned)
                                    .ThenByDescending(m => m.IsMatch)
                                    .ThenBy(m => m.Member.Name))
                        };
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
        public uint SkillPointsCompleted { get; set; }
        public int AssignedCount { get; set; }
        public string AssignedNames { get; set; }
        public List<RequirementSlotItem> RequirementSlots { get; set; }
        public ObservableCollection<MemberAssignmentItem> Members { get; set; }
    }

    public class RequirementSlotItem
    {
        public string Label { get; set; }
        public bool IsFilled { get; set; }
        // Drives border/text colour in XAML via a pre-computed string
        public string FilledColor => IsFilled ? "#FF44FF55" : "#FFFF4400";
        public string FilledBorderColor => IsFilled ? "#FF226622" : "#FF661100";
        public string FilledIcon => IsFilled ? "✓" : "✗";
    }

    public class MemberAssignmentItem
    {
        public TeamMember Member { get; set; }
        public Contract Contract { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsMatch { get; set; }
        public string ButtonLabel { get; set; }
        public string SpecialtyMatchLabel { get; set; }
        // Background tint: assigned rows get a subtle highlight
        public string RowBackground => IsAssigned ? "#FF1C2A00" : "#FF110E00";
        public string RowBorderColor => IsAssigned ? "#FF446600" : "#FF664400";
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
