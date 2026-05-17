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
    public class ContractsViewModel : BaseViewModel
    {
        private ObservableCollection<ContractListingItem> _availableContracts;

        public ObservableCollection<ContractListingItem> AvailableContracts
        {
            get => _availableContracts;

            set
            {
                _availableContracts = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ContractListingItem> _activeContracts;

        public ObservableCollection<ContractListingItem> ActiveContracts
        {
            get => _activeContracts;

            set
            {
                _activeContracts = value;
                OnPropertyChanged();
            }
        }

        public ContractsViewModel(GameWrapper wrapper) : base(wrapper)
        {
            Refresh();
        }

        public void AcceptContract(Contract contract)
        {
            gWrapper.CurrentGame.AddContract(contract);
            Refresh();
        }

        private void Refresh()
        {
            AvailableContracts = new ObservableCollection<ContractListingItem>(
                gWrapper.CurrentGame.AvailableContracts.Select(c => new ContractListingItem
                {
                    Contract = c,
                    CanAccept = gWrapper.CurrentGame.CanAcceptContract(c),
                    AcceptLabel = gWrapper.CurrentGame.CanAcceptContract(c)
                        ? "Accept Contract"
                        : "Missing Required Specialists",
                    DeadlineLabel = $"{c.DeadlineMonths} month window",
                    SpecialtiesLabel = string.Join(", ", c.SpecialtiesRequired.Keys.Select(k => k.ToString()))
                }));

            ActiveContracts = new ObservableCollection<ContractListingItem>(
                gWrapper.CurrentGame.Contracts
                    .Where(c => c.Status == ContractStatus.InProgress)
                    .Select(c => new ContractListingItem
                    {
                        Contract = c,
                        CanAccept = false,
                        DeadlineLabel = c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate) <= 1
                            ? "⚠ DEADLINE CRITICAL"
                            : $"{c.TurnsRemaining(gWrapper.CurrentGame.CurrentDate)} months left",
                        SpecialtiesLabel = $"{c.AssignedTeamMembers.Count} operators assigned"
                    }));
        }

        public List<TeamMember> GetEligibleTeamMembers(Contract contract)
        {
            return gWrapper.CurrentGame.TeamMembers
                .Where(a => a.Status == TeamMemberStatus.OnTeam &&
                    contract.SpecialtiesRequired.ContainsKey(a.Specialty))
                .OrderByDescending(b => b.SkillPoints)
                .ToList();
        }

        public void AssignTeamMember(Contract contract, TeamMember member)
        {
            if (!contract.AssignedTeamMembers.Contains(member.Id))
            {
                contract.AssignedTeamMembers.Add(member.Id);
            }
        }

        public void UnassignTeamMember(Contract contract, Guid memberId)
        {
            contract.AssignedTeamMembers.Remove(memberId);
        }
    }

    public class ContractListingItem
    {
        public Contract Contract { get; set; }
        public bool CanAccept { get; set; }
        public string AcceptLabel { get; set; }
        public string DeadlineLabel { get; set; }
        public string SpecialtiesLabel { get; set; }
    }
}
