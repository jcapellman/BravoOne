using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Windows.Storage;

namespace BravoOne.UWP.PlatformImplementations
{
    public class UWPStorage : IStorage
    {
        private StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

        private const string RootFolder = "Assets";

        private async Task<List<string>> GetFileNamesAsync(string subFolderName, string rootFolderName = RootFolder)
        {
            var filenames = new List<string>();

            StorageFolder rootFolder = await installedLocation.GetFolderAsync(rootFolderName);

            var subFolder = await rootFolder.GetFolderAsync(subFolderName);

            var files = await subFolder.GetFilesAsync();

            foreach (var file in files)
            {
                filenames.Add(file.Path);
            }

            return filenames;
        }

        public async Task<List<string>> GetAvatarImagesAsync() => await GetFileNamesAsync("Avatars");

        public async Task<List<Equipment>> GetEquipmentListAsync()
        {
            StorageFolder rootFolder = await installedLocation.GetFolderAsync(RootFolder);

            var subFolder = await rootFolder.GetFolderAsync("Data");

            var equipmentFile = await subFolder.GetFileAsync("Equipment.json");

            var jsonText = await Windows.Storage.FileIO.ReadTextAsync(equipmentFile);

            return JsonConvert.DeserializeObject<List<Equipment>>(jsonText);
        }
    }
}