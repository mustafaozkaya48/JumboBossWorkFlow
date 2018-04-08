using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T,bool>> where);

        T GetById(int id);

        T Get(Expression<Func<T, bool>> where);

        void Insert(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Updete(T entity);
    }
}
