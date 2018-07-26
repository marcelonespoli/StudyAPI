using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ComplyfileAPI.Infra.Data.Context;
using ComplyfileAPI.Infra.Data.Repository.Interfaces;

namespace ComplyfileAPI.Infra.Data.Repository.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ComplyfileApiContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ComplyfileApiContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual TEntity FindByKey(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public IEnumerable<TEntity> AllInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public virtual IEnumerable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }
        
    }
}
