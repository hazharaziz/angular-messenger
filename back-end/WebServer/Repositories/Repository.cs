using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebServer.DataContext;

namespace WebServer.Repositories
{
    public class Repository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public virtual List<TEntity> GetAll()
            => _context.Set<TEntity>().ToList();

        public virtual List<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            => _context.Set<TEntity>().Where(predicate).ToList();

        public virtual void Add(TEntity entity)
            => _context.Set<TEntity>().Add(entity);

        public virtual void AddRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().AddRange(entities);

        public virtual void Remove(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
            => _context.Set<TEntity>().RemoveRange(entities);

    }
}
