using System;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class Equipment
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public int RequiredLevel { get; set; }

        public int Cost { get; set; }

        public EquipmentType EType { get; set; }

        public int Reliability { get; set; }

        public int Damage { get; set; }
    }
}