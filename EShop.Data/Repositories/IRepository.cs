using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace EShop.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        void RemoveRange(IEnumerable<T> entities);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }

    public interface IKeyRepository<T, in TKey> : IRepository<T>
    where T : class
    where TKey : struct
    {
        Task<T> GetById(TKey id);

        Task<T> GetById<TProperty>(TKey id, Expression<Func<T, TProperty>> include);

        Task DeleteAsync(TKey id);
    }
}
