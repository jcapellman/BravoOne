using BravoOne.lib;

namespace BravoOne.UWP.ViewModels
{
    public class EquipmentViewModel : Base.BaseViewModel
    {
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
                } else if (equipment.Cost > gWrapper.CurrentGame.Money)
                {
                    equipment.Comments = "Equipment is too expensive for your team";
                    equipment.Available = false;
                }
            }
        }

        internal void AddEquipment(lib.Objects.Equipment equipment)
        {
            gWrapper.CurrentGame.AddEquipment(equipment);

            InitializeEquipment();
        }
    }
}