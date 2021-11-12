using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using BravoOne.lib.DAL.Base;

namespace BravoOne.lib.DAL
{
    public class LiteDBDAL : BaseDAL
    {
        public LiteDBDAL(string rootFolderName) : base(rootFolderName)
        {
        }

        protected override string FileName => "litedb.db";

        public override void Add<T>(T item)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                var collection = db.GetCollection<T>();

                collection.Insert(item);
            }
        }

        public override void Delete<T>(int id)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                var collection = db.GetCollection<T>();

                collection.Delete(id);
            }
        }

        public override void Delete<T>(Expression<Func<T, bool>> expression)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                var collection = db.GetCollection<T>();

                collection.DeleteMany(expression);
            }
        }

        public override T Get<T>(Expression<Func<T, bool>> expression)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                var collection = db.GetCollection<T>();

                return collection.FindOne(expression);
            }
        }

        public override List<T> GetAll<T>(Expression<Func<T, bool>> expression = null)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                var collection = db.GetCollection<T>();

                if (expression != null)
                {
                    return collection.Find(expression).ToList();
                }

                return collection.FindAll().ToList();
            }
        }

        public override void Update<T>(T item)
        {
            using (var db = new LiteDB.LiteDatabase(ComputedFileName))
            {
                db.GetCollection<T>().Update(item);
            }
        }
    }
}