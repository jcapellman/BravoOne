using BravoOne.lib;
using BravoOne.lib.Objects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BravoOne.UWP.ViewModels
{
    public class EquipmentViewModel : BaseViewModel
    {
        private ObservableCollection<OwnedEquipmentItem> _ownedEquipment;

        public ObservableCollection<OwnedEquipmentItem> OwnedEquipment
        {
            get => _ownedEquipment;
            set { _ownedEquipment = value; OnPropertyChanged(); }
        }

        public EquipmentViewModel(GameWrapper wrapper) : base(wrapper)
        {
            InitializeEquipment();
        }

        private void InitializeEquipment()
        {
            foreach (var equipment in gWrapper.CurrentGame.AvailableEquipment)
            {
                equipment.Available = true;
                equipment.Comments = string.Empty;

                if (equipment.RequiredLevel > gWrapper.CurrentGame.TeamLevel)
                {
                    equipment.Available = false;
                    equipment.Comments = "Equipment experience is too much for your team";
                }
                else if (equipment.Cost > gWrapper.CurrentGame.Money)
                {
                    equipment.Comments = "Equipment is too expensive for your team";
                    equipment.Available = false;
                }
            }

            // Build the owned equipment list: all TeamEquipment cross-referenced with assigned operators.
            var items = new List<OwnedEquipmentItem>();
            foreach (var eq in gWrapper.CurrentGame.TeamEquipment)
            {
                var assignedTo = gWrapper.CurrentGame.TeamMembers
                    .Where(m => m.Equipment.Any(te => te.EquipmentId == eq.Id))
                    .Select(m => m.Name)
                    .ToList();

                var slots = gWrapper.CurrentGame.TeamMembers
                    .SelectMany(m => m.Equipment.Where(te => te.EquipmentId == eq.Id)
                        .Select(te => new OwnedEquipmentSlot
                        {
                            OwnerName = m.Name,
                            Slot = te,
                            ConditionLabel = te.Status >= 75 ? "GOOD" : te.Status >= 40 ? "WORN" : "DEGRADED",
                            ConditionColor = te.Status >= 75 ? "#FF44DD44" : te.Status >= 40 ? "#FFFFCC44" : "#FFFF6666",
                            RepairCost = (ulong)(eq.Cost / 10 * (100 - te.Status) / 100),
                            CanRepair = te.Status < 100 && (ulong)(eq.Cost / 10 * (100 - te.Status) / 100) <= gWrapper.CurrentGame.Money
                        }))
                    .ToList();

                if (slots.Count > 0)
                {
                    items.Add(new OwnedEquipmentItem
                    {
                        Equipment = eq,
                        Slots = new ObservableCollection<OwnedEquipmentSlot>(slots)
                    });
                }
            }

            OwnedEquipment = new ObservableCollection<OwnedEquipmentItem>(items);
        }

        internal void AddEquipment(Equipment equipment)
        {
            gWrapper.CurrentGame.AddEquipment(equipment);
            InitializeEquipment();
        }

        internal void RepairEquipment(TeamEquipment slot)
        {
            gWrapper.CurrentGame.RepairEquipment(slot, 100 - slot.Status);
            InitializeEquipment();
        }
    }

    public class OwnedEquipmentItem
    {
        public Equipment Equipment { get; set; }
        public ObservableCollection<OwnedEquipmentSlot> Slots { get; set; }
    }

    public class OwnedEquipmentSlot
    {
        public string OwnerName { get; set; }
        public TeamEquipment Slot { get; set; }
        public string ConditionLabel { get; set; }
        public string ConditionColor { get; set; }
        public ulong RepairCost { get; set; }
        public bool CanRepair { get; set; }
    }
}
