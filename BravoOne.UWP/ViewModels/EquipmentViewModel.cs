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
                    equipment.Comments = "Not skilled enough";
                }
            }
        }
    }
}