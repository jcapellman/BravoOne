using System.Collections.Generic;

namespace BravoOne.lib.Objects
{
    public class TurnSummary
    {
        public long MoneyDelta { get; set; }
        public List<string> ContractsCompleted { get; } = new List<string>();
        public List<string> ContractsFailed { get; } = new List<string>();
        public List<string> OperatorsKilled { get; } = new List<string>();
        public List<string> OperatorsInjured { get; } = new List<string>();
        public List<string> OperatorLevelUps { get; } = new List<string>();
        public string RandomEventDescription { get; set; }

        public bool HasEvents =>
            ContractsCompleted.Count > 0 || ContractsFailed.Count > 0 ||
            OperatorsKilled.Count > 0 || OperatorsInjured.Count > 0 ||
            OperatorLevelUps.Count > 0 || !string.IsNullOrEmpty(RandomEventDescription);
    }
}
