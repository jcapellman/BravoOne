using BravoOne.lib.DAL.Base;
using BravoOne.lib.PlatformAbstractions;

namespace BravoOne.lib.Managers.Base
{
    public class BaseManager
    {
        protected IStorage Storage;

        protected BaseDAL DAL;

        protected BaseManager(IStorage storage, BaseDAL dal)
        {
            Storage = storage;
            DAL = dal;
        }
    }
}