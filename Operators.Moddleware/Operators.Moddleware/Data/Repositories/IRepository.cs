using Operators.Moddleware.Data.Entities;
using System.Linq.Expressions;

namespace Operators.Moddleware.Data.Repositories {
    public interface IRepository<T> where T : DomainEntity {
        T Get(long id);
        Task<T> GetAsync(long id);
        T Get(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] filters);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] filters);
        IList<T> GetAll();
        Task<IList<T>> GetAllAsync();
        IList<T> GetAll(Expression<Func<T, bool>> expression);
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        bool Insert(T entity);
        Task<bool> InsertAsync(T entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);
        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        /// <summary>
        /// Bulk inserts to the database
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> BulkyInsertAsync(T[] entities);
        /// <summary>
        /// Bulk updates to the database
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<bool> BulkyUpdateAsync(T[] entities);
    }
}
