using EShop.Data.Context;
using EShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EShop.Data.Repositories
{
    public class KeyRepository<T, TKey> : BaseRepository<T>, IKeyRepository<T, TKey>
        where T : BaseEntity
        where TKey: struct
    {
        private readonly DbContext dbContext;

        public KeyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetById(TKey id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T> GetById<TProperty>(TKey id, Expression<Func<T, TProperty>> include)
        {
            return await DbSet.Include(include).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task DeleteAsync(TKey id)
        {
            T entity = await DbSet.FindAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} with id {id} not found.");

            DbSet.Remove(entity);
            await dbContext.SaveChangesAsync();
        }
    }

}
