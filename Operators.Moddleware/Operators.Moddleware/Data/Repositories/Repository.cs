using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Operators.Moddleware.Data.Entities;
using System.Linq.Expressions;

namespace Operators.Moddleware.Data.Repositories {
    public class Repository<T>(DbContext context) : IRepository<T> where T : DomainEntity {

        private readonly DbContext _context = context;
        private readonly DbSet<T> _entities = context.Set<T>();

        public T Get(long id) {
            return _entities.SingleOrDefault(t => t.Id == id);
        }

        public async Task<T> GetAsync(long id)
            => await _entities.SingleOrDefaultAsync(t => t.Id == id);

        public T Get(Expression<Func<T, bool>> expression)
            => _entities.FirstOrDefault(expression);

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] filters) {
            T entity = null;
            try {
                 var itemslist = _entities.AsQueryable();
                if(filters != null ){
                    entity = filters.Aggregate(itemslist,
                            (current, next) => current.Include(next)).FirstOrDefault();
                } 

                entity = _entities.FirstOrDefault(predicate);
                
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
            => await _entities.FirstOrDefaultAsync(expression);

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] filters) {
            T entity = null;
            try {
                var itemslist = _entities.AsQueryable();

                // Apply Includes first if filters exist
                if (filters != null && filters.Length > 0) {
                    itemslist = filters.Aggregate(itemslist, (current, next) => current.Include(next));
                }

                // Apply the predicate to filter results
                entity = await itemslist.FirstOrDefaultAsync(predicate);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }

            return entity;
        }


        public IList<T> GetAll()
            => [.. _entities];

        public async Task<IList<T>> GetAllAsync()
            => await _entities.ToListAsync();


        public IList<T> GetAll(Expression<Func<T, bool>> expression)
            => [.. _entities.Where(expression)];

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression)
             => await _entities.Where(expression).ToListAsync();

        public bool Insert(T entity) {
            ArgumentNullException.ThrowIfNull(entity);
            try {
                _entities.Add(entity);
                return _context.SaveChanges() > 0;
            } catch {
                return false;
            }
        }

        public async Task<bool> InsertAsync(T entity) {
             ArgumentNullException.ThrowIfNull(entity);
             try {
                await _entities.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
             } catch {
                return false;
             }
        }

        public bool Update(T entity) {
            ArgumentNullException.ThrowIfNull(entity);
            try {
                return _context.SaveChanges() > 0;
            } catch {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity) {
            ArgumentNullException.ThrowIfNull(entity);
            try{ 
                return await _context.SaveChangesAsync() > 0;
            } catch {
                return false;
            }
        }

        public bool Delete(T entity) {
            ArgumentNullException.ThrowIfNull(entity);
            try{ 
               _entities.Remove(entity);
               return _context.SaveChanges() > 0;
            } catch {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity) {
             ArgumentNullException.ThrowIfNull(entity);
            try{ 
               _entities.Remove(entity);
               return await _context.SaveChangesAsync() > 0;
            } catch {
                return false;
            }
        }

        public bool Exists(Expression<Func<T, bool>> expression)
            => _entities.FirstOrDefault(expression) != null;

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
            => await _entities.FirstOrDefaultAsync(expression) != null;

        public async Task<bool> BulkyInsertAsync(T[] parameters) {
            if (parameters == null || parameters.Length == 0)
                return false;

            await _context.BulkInsertAsync(parameters.ToList());
            return true;
        }

        public async Task<bool> BulkyUpdateAsync(T[] parameters) {
            if (parameters == null || parameters.Length == 0)
                return false;

            await _context.BulkUpdateAsync(parameters.ToList());
            return true;
        }
    }
}
