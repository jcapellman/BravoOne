using BravoOne.lib.Objects;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace BravoOne.lib.PlatformAbstractions
{
    public interface IStorage
    {
        Task<List<string>> GetAvatarImagesAsync();

        Task<List<Equipment>> GetEquipmentListAsync();

        Task<List<string>> GetActivityTypesImagesAsync();

        Task<string> GetFullPathAsync(string subFolderName);
    }
}