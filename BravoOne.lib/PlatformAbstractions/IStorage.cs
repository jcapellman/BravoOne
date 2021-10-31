using System.Collections.Generic;
using System.Threading.Tasks;

namespace BravoOne.lib.PlatformAbstractions
{
    public interface IStorage
    {
        Task<List<string>> GetAvatarImagesAsync();
    }
}