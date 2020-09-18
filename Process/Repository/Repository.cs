using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.BaseModels;
using Core.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Process.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly ApplicationDbContext Context;


        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }
        public virtual IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }
        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            return Context.Set<T>().IncludeAll(includeProperties);
        }
        public virtual IQueryable<T> GetAll(params string[] includeProperties)
        {
            return Context.Set<T>().IncludeAll(includeProperties);
        }

        public Task<T> GetSingleFirstAsync()
        {

            return Context.Set<T>().FirstOrDefaultAsync();
        }


        public virtual Task<T> GetSingleFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public virtual Task<T> GetSingleFirstAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);

            return query.FirstOrDefaultAsync(predicate);
        }

        public virtual Task<T> GetSingleFirstAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);


            return query.FirstOrDefaultAsync(predicate);
        }

        public virtual Task<T> GetSingleLastAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Context.Set<T>();
            return query.LastOrDefaultAsync(predicate);
        }

        public Task<T> GetSingleLastAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            return query.LastOrDefaultAsync(predicate);
        }

        public Task<T> GetSingleLastAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            return query.LastOrDefaultAsync(predicate);
        }

        public virtual Task<T> GetSingleLastAsync()
        {
            IQueryable<T> query = Context.Set<T>();
            return query.LastOrDefaultAsync();
        }


        //public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        //{
        //    return Context.Set<T>().Where(predicate);
        //}
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool ignoreQueryFilter = false)
        {
            IQueryable<T> query = Context.Set<T>();
            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();

            return query.Where(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            return query.Where(predicate);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool ignoreQueryFilter, params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();
            return query.Where(predicate);
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);

            return query.Where(predicate);
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, bool ignoreQueryFilter, params string[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            if (ignoreQueryFilter)
                query = query.IgnoreQueryFilters();
            return query.Where(predicate);
        }

        public virtual Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().AnyAsync(predicate);
        }
        public virtual Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            return query.AnyAsync(predicate);
        }

        public Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<T> query = Context.Set<T>().IncludeAll(includeProperties);
            return query.AnyAsync(predicate);
        }

        public virtual async Task AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity).ConfigureAwait(false);
        }
        public virtual Task AddRangeAsync(List<T> entity)
        {

            return Context.Set<T>().AddRangeAsync(entity);
        }
        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> entities = Context.Set<T>().Where(predicate);
            Context.Set<T>().RemoveRange(entities);
        }

    }

}
