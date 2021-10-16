using BravoOne.lib.DAL.Base;
using BravoOne.lib.Objects;

namespace BravoOne.lib
{
    public class GameWrapper
    {
        public BaseDAL DAL { get; private set; }

        public Game CurrentGame { get; set; }

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