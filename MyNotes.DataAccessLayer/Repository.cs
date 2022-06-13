using MyNotes.CoreLayer;
using MyNotes.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyNotes.Common;

namespace MyNotes.DataAccessLayer
{
    public class Repository<T> :BaseRepository, IRepository<T> where T: class
    {
        private readonly MyNoteContext _db;
        private DbSet<T> objSet;

        public Repository()
        {
            _db = BaseRepository.CreateContext();
            objSet = _db.Set<T>();
        }
        public int Delete(T entity)
        {
            objSet.Remove(entity);
            return Save();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return objSet.FirstOrDefault(predicate);
        }

        public int Insert(T entity)
        {
            objSet.Add(entity);
            if (entity is BaseEntity)
            {
                DateTime now = DateTime.Now;
                BaseEntity o = entity as BaseEntity;
                o.ModifiedUserName = "system";
                o.ModifiedOn = now;
                o.CreatedOn = now;
            }

            return Save();
        }

        public List<T> List()
        {
            return objSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            return objSet.Where(predicate).ToList();
        }

        public IQueryable<T> QList()
        {
            return objSet.AsQueryable();
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public int Update(T entity)
        {
            if (entity is BaseEntity o)
            {
                o.ModifiedUserName = App.Common.GetCurrentUsername();
                o.ModifiedOn = DateTime.Now;
                

            }

            return Save();
        }
    }
}
