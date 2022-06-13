using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.CoreLayer
{
    public interface IRepository<T>
    {
        List<T> List();
        List<T> List(Expression<Func<T, bool>> predicate);

        IQueryable<T> QList();

        int Insert(T entity);
        int Update(T entity);
        int Delete(T entity);
        int Save();
        T Find(Expression<Func<T, bool>> predicate);
    }
}
