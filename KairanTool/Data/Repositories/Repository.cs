using System;
using System.Linq;
using System.Linq.Expressions;
using KairanTool.Data.Interfaces;
using KairanTool.Models;
using Microsoft.EntityFrameworkCore;

namespace KairanTool.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
{
    private readonly KairanToolDbContext _context;
    private readonly DbSet<T> _entity;

    public Repository(KairanToolDbContext context)
    {
        _context = context;
        _entity = _context.Set<T>();
    }
    public virtual IQueryable<T> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _entity;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }

//        public IQueryable<T> GetAll()
//        {
//            return _entity;
//        }

       

    public T Get(int? id)
    {
        return _entity.Find(id);
    }

    public void Insert(T entity)
    {
        _entity.Add(entity);
    }

    public void Update(T entity)
    {
        _entity.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

        

    public void Remove(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _entity.Attach(entity);
        }

        _entity.Remove(entity);
    }
    public void Delete(object id)
    {
        var entity = _entity.Find(id);
        Remove(entity);
    }
}
}