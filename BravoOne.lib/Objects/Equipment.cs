using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class Equipment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int RequiredLevel { get; set; }

        public ulong Cost { get; set; }

        public EquipmentType EType { get; set; }

        public int Reliability { get; set; }

        public int Damage { get; set; }

        public bool Available { get; set; }

        public string Comments { get; set; }

        public string ImagePath { get; set; }
    }
}