using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BravoOne.lib.Managers
{
    public class AssetManager : BaseManager
    {
        public AssetManager(IStorage storage, BaseDAL dal) : base(storage, dal)
        {
        }

        private async Task<Game> InitializeActivityTypes(Game currentGame)
        {
            currentGame.ActivityTypeImages = new Dictionary<string, string>();

            var values = Enum.GetNames(typeof(ActivityType));
            var images = await Storage.GetActivityTypesImagesAsync();

            foreach (var enumValue in values)
            {
                currentGame.ActivityTypeImages.Add(enumValue, images.FirstOrDefault(a => a.ToLower().Contains(enumValue.ToLower())));
            }

            return currentGame;
        }

        private async Task<Game> InitializeEquipment(Game currentGame)
        {
            currentGame.AvailableEquipment = (await Storage.GetEquipmentListAsync()).OrderByDescending(a => a.Cost).ThenBy(b => b.Name).ToList();

            return currentGame;
        }

        public override async Task<Game> InitializeAsync(Game currentGame)
        {
            currentGame = await InitializeActivityTypes(currentGame);

            currentGame = await InitializeEquipment(currentGame);

            return currentGame;
        }

        public override (TurnStatus Status, Game CurrentGame) ProcessTurn(Game currentGame)
        {
            return (TurnStatus.OK, currentGame);
        }
    }
}
