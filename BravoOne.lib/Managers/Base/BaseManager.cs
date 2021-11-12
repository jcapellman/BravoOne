using BravoOne.lib.PlatformAbstractions;

namespace BravoOne.lib.Managers.Base
{
    public class BaseManager
    {
        protected IStorage Storage;

        protected BaseManager(IStorage storage)
        {
            Storage = storage;
        }
    }
}