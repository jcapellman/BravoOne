using BravoOne.lib.DAL.Base;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        private readonly List<BaseManager> _managers;

        public GameWrapper(BaseDAL dal, IStorage storage, Game aGame = null)
        {
            DAL = dal;

            if (aGame != null)
            {
                CurrentGame = aGame;
            }

            Storage = storage;

            _managers = typeof(GameWrapper).Assembly.GetTypes().Where(a =>
                a.BaseType == typeof(BaseManager) && !a.IsAbstract).Select(b => (BaseManager)Activator.CreateInstance(b, args: new object[] { storage, dal })).ToList();
        }

        public T GetManager<T>() where T : BaseManager => (T)_managers.FirstOrDefault(a => a.GetType() == typeof(T));

        public async Task StartGame(string teamLeaderName, string selectedLogo)
        {
            ulong startingMoney;

            switch (Option.Difficulty)
            {
                case 1:
                    startingMoney = 200000;
                    break;
                case 3:
                    startingMoney = 50000;
                    break;
                default:
                    startingMoney = 100000;
                    break;
            }

            CurrentGame = new Game
            {
                TeamLeaderName = teamLeaderName,
                TeamLogo = selectedLogo,
                Money = startingMoney
            };

            foreach (var manager in _managers)
            {
                CurrentGame = await manager.InitializeAsync(CurrentGame);
            }
        }

        public async Task<bool> EndTurn()
        {
            CurrentGame.EndTurn();

            foreach (var manager in _managers)
            {
                var turnResult = await manager.ProcessTurnAsync(CurrentGame);

                CurrentGame = turnResult.CurrentGame;

                if (turnResult.Status == Enums.TurnStatus.OK)
                {
                    continue;
                }

                switch (turnResult.Status)
                {
                    case Enums.TurnStatus.OUT_OF_MONEY:
                        CurrentGame.FinalizeTurnSummary();
                        return false;
                    default:
                        CurrentGame.FinalizeTurnSummary();
                        return true;
                }
            }

            CurrentGame.FinalizeTurnSummary();
            return true;
        }
     }
}