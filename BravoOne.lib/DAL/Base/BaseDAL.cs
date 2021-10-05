using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BravoOne.lib.DAL.Base
{
    public abstract class BaseDAL
    {
        public abstract void Add<T>(T item);

        public abstract void Update<T>(T item);

        public abstract void Delete<T>(int id);

        public abstract void Delete<T>(Expression<Func<T, bool>> expression);

        public abstract T Get<T>(Expression<Func<T, bool>> expression);

        public abstract List<T> GetAll<T>(Expression<Func<T, bool>> expression);
    }
}