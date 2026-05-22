using System;
using System.Collections.Generic;
using System.Linq;

using BravoOne.lib.Enums;

namespace BravoOne.lib.Objects
{
    public class TeamMember
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public uint MonthlySalary { get; set; }

        public DateTime StartDate { get; set; }

        public int Health { get; set; }

        public TeamMemberStatus Status { get; set; }

        public uint SkillPoints { get; set; }

        public int Age { get; set; }

        public int AgeMonths { get; set; }

        public int RetirementAge { get; set; }

        public Specialties Specialty { get; set; }

        public string Comments { get; set; }

        public string AvatarImagePath { get; set; }

        public bool Available { get; set; }

        // Maximum health — permanently reduced after critical injuries.
        public int MaxHealth { get; set; } = 100;

        // Consecutive months assigned to a contract; resets to 0 when unassigned for a month.
        public int FatigueMonths { get; set; }

        public List<TeamEquipment> Equipment { get; set; }

        // Returns effective skill points factoring in health, fatigue, and equipment condition.
        // A medic at full health contributes only healing, not combat skill points.
        public uint EffectiveSkillPoints(List<Equipment> teamEquipment)
        {
            if (Specialty == Specialties.MEDIC)
            {
                return 0;
            }

            var healthFactor = Math.Max(0.0, Health / 100.0);

            // Fatigue: each month beyond the 2nd on a contract reduces effectiveness by 10%, capped at -50%.
            var fatiguePenalty = Math.Min(0.5, Math.Max(0.0, (FatigueMonths - 2) * 0.1));
            var effectiveFactor = healthFactor * (1.0 - fatiguePenalty);

            var equipmentBonus = Equipment
                .Where(te => te.Status > 0)
                .Join(teamEquipment, te => te.EquipmentId, e => e.Id, (te, e) =>
                {
                    var conditionFactor = te.Status / 100.0;
                    return (uint)(e.Damage * conditionFactor);
                })
                .DefaultIfEmpty(0u)
                .Aggregate(0u, (sum, v) => sum + v);

            return (uint)(SkillPoints * effectiveFactor) + equipmentBonus;
        }

        // Returns health restored by this member if they are a medic.
        public int HealingValue()
        {
            if (Specialty != Specialties.MEDIC)
            {
                return 0;
            }

            var healthFactor = Math.Max(0.0, Health / 100.0);
            return (int)(SkillPoints * healthFactor * 2);
        }

        public TeamMember()
        {
            Equipment = new List<TeamEquipment>();
            MaxHealth = 100;
        }
    }
}