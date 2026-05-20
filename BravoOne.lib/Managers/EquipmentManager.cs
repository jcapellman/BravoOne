using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    internal class EquipmentManager : BaseManager
    {
        private const int TURN_STATUS = 5;

        public EquipmentManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        public override Task<Game> InitializeAsync(Game currentGame) => Task.FromResult(currentGame);

        public override Task<(TurnStatus Status, Game CurrentGame)> ProcessTurnAsync(Game currentGame)
        {
            foreach (var teamMember in currentGame.TeamMembers.Where(a => a.Status == TeamMemberStatus.OnTeam))
            {
                if (!teamMember.Equipment.Any())
                {
                    continue;
                }

                foreach (var equipment in teamMember.Equipment)
                {
                    if (equipment.Status <= 0)
                    {
                        equipment.Status = 0;
                        continue;
                    }

                    equipment.Status = Math.Max(0, equipment.Status - TURN_STATUS);
                }
            }

            return Task.FromResult((TurnStatus.OK, currentGame));
        }
    }
}