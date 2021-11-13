﻿using BravoOne.lib.DAL.Base;
using BravoOne.lib.Managers.Base;
using BravoOne.lib.Objects;
using BravoOne.lib.PlatformAbstractions;

using System;
using System.Collections.Generic;
using System.IO;
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

        private List<BaseManager> _managers;

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

        public bool EndTurn()
        {
            var result = CurrentGame.EndTurn(Storage);

            if (!result)
            {
                return false;
            }

            foreach (var manager in _managers)
            {
                manager.ProcessTurn(CurrentGame);
            }

            return true;
        }
     }
}