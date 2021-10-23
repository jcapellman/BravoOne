using BravoOne.lib.DAL.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib
{
    public class GameWrapper
    {
        public BaseDAL DAL { get; private set; }

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

        public GameWrapper(BaseDAL dal, Game aGame = null)
        {
            DAL = dal;

            if (aGame != null)
            {
                CurrentGame = aGame;
            }
        }
     }
}