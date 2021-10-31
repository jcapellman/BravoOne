using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Windows.Storage;

namespace BravoOne.UWP.PlatformImplementations
{
    public class UWPStorage : IStorage
    {
        public async Task<List<string>> GetAvatarImagesAsync()
        {
            var avatars = new List<string>();

            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder subFolder = await installedLocation.GetFolderAsync("Assets");
            subFolder = await subFolder.GetFolderAsync("Avatars");

            var images = await subFolder.GetFilesAsync();

            foreach (var image in images)
            {
                avatars.Add(image.Path);
            }

            return avatars;
        }
    }
}