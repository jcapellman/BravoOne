using BravoOne.lib.DAL.Base;
using BravoOne.lib.Enums;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

namespace BravoOne.lib.Managers.Base
{
    public abstract class BaseManager
    {
        protected IStorage Storage;

        protected BaseDAL DAL;

        protected BaseManager(IStorage storage, BaseDAL dal)
        {
            Storage = storage;
            DAL = dal;
        }

        public abstract (TurnStatus Status, Game CurrentGame) ProcessTurn(Game currentGame);
    }
}