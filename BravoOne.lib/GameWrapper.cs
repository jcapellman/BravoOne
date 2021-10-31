using BravoOne.lib.DAL.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

namespace BravoOne.lib
{
    public class GameWrapper
    {
        public BaseDAL DAL { get; private set; }

        public IStorage Storage { get; private set; }

        public Game CurrentGame { get; set; }

        private Options _option;

        public Options Option
        {
            get
            {
                if (_option == null)
                {
                    _option = DAL.Get<Options>(a => a != null);
                }

                return _option ?? new Options();
            }

            set
            {
                _option = value;

                DAL.Update<Options>(value);
            }
        }

        public GameWrapper(BaseDAL dal, IStorage storage, Game aGame = null)
        {
            DAL = dal;

            if (aGame != null)
            {
                CurrentGame = aGame;
            }

            Storage = storage;
        }
     }
}