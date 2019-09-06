using System;
using System.Linq;
using System.Linq.Expressions;

namespace KairanTool.Data.Interfaces
{
    public interface IRepository<T> where T: class
    {
        IQueryable < T > GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");  
        T Get(int? id);  
        void Insert(T entity);  
        void Update(T entity);  
        void Remove(T entity);
    }
}