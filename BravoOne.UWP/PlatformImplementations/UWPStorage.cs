using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

namespace BravoOne.UWP.PlatformImplementations
{
    public class UWPStorage : IStorage
    {
        private async Task<List<string>> GetFileNamesAsync(string subfolder, string rootFolder = "Assets")
        {
            var filenames = new List<string>();

            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder subFolder = await installedLocation.GetFolderAsync(rootFolder);
            subFolder = await subFolder.GetFolderAsync(subfolder);

            var files = await subFolder.GetFilesAsync();

            foreach (var file in files)
            {
                filenames.Add(file.Path);
            }

            return filenames;
        }

        public async Task<List<string>> GetAvatarImagesAsync() => await GetFileNamesAsync("Avatars");
    }
}