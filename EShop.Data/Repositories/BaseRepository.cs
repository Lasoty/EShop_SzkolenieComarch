using EShop.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EShop.Data.Repositories
{
    /// <summary>
    ///     The EF-dependent, generic repository for data access
    /// </summary>
    /// <typeparam name="T">Type of entity for this Repository.</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public BaseRepository(ApplicationDbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext),
                "An instance of DbContext is required to use this repository");
            DbSet = Context.Set<T>();
        }

        protected DbContext Context { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);

            Context.SaveChanges();
        }

        public virtual async Task AddAsync(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                await DbSet.AddAsync(entity);

            await Context.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = UpdateState(entity);
            Context.SaveChanges();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            EntityEntry dbEntityEntry = UpdateState(entity);
            await Context.SaveChangesAsync();
        }

        private EntityEntry UpdateState(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached) DbSet.Attach(entity);
            dbEntityEntry.State = EntityState.Modified;
            return dbEntityEntry;
        }

        public virtual void Delete(T entity)
        {
            EntityEntry dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }

            Context.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            Context.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
            Context.SaveChanges();
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

    }
}
