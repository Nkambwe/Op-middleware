using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Helpers;
using System;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Operators.Moddleware.Data.Repositories {

    public class Repository<T>(DbContext context) : IRepository<T> where T : DomainEntity {

        private readonly IServiceLogger _logger = new ServiceLogger("Operations_data") {
            Channel = "REPOSITORY"
        };

        private readonly DbContext _context = context;
        private readonly DbSet<T> _entities = context.Set<T>();

        public T Get(long id, bool includeDeleted = false) {
            T entity = null;
            try {
                var query = _entities.AsQueryable();

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                entity = query.FirstOrDefault(e => e.Id == id);
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
            }

            return entity;
        }

        public async Task<T> GetAsync(long id, bool includeDeleted = false) {
            T entity = null;
            try {
                var query = _entities.AsQueryable();

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                entity = await query.FirstOrDefaultAsync(e => e.Id == id);
                
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
            }

            return entity;
        }

        public T Get(Expression<Func<T, bool>> where, bool includeDeleted = false) {
            T entity = null;
            try {
                var query = _entities.AsQueryable();

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                entity = query.FirstOrDefault(where);
                
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
            }

            return entity;
        }

        public T Get(Expression<Func<T, bool>> where, bool includeDeleted = false, params Expression<Func<T, object>>[] filters) {
            T entity = null;
            try {
                 var query = _entities.AsQueryable();

                //include related entities
                if(filters != null ){
                    query = filters.Aggregate(query,
                            (current, next) => current.Include(next));
                }

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                entity = query.FirstOrDefault(where);
                
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
            }

            return entity;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, bool includeDeleted = false){
            try {
                var query = _entities.AsQueryable();

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                return await query.FirstOrDefaultAsync(where);
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return null;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> where,bool includeDeleted = false, params Expression<Func<T, object>>[] filters) {
            try {
                var query = _entities.AsQueryable();

                // Apply Includes if filters exist
                if (filters?.Length > 0) {
                    query = filters.Aggregate(query, (current, next) => current.Include(next));
                }

                // Apply soft-delete filter if includeDeleted is false
                if (!includeDeleted) {
                    query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
                }

                return await query.FirstOrDefaultAsync(where);
            } catch (Exception ex) {
                _logger.LogToFile($"Get operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return null;
            }
        }


        public IList<T> GetAll(bool includeDeleted = false) {
            var query = _entities.AsQueryable();

            if (!includeDeleted) {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            return [.. query];
        }

        public async Task<IList<T>> GetAllAsync(bool includeDeleted = false)
            => await _entities.Where(e => includeDeleted || EF.Property<bool>(e, "IsDeleted") == false)
                .ToListAsync();


        public IList<T> GetAll(Expression<Func<T, bool>> where, bool includeDeleted = false) {
            var query = _entities.AsQueryable();

            if (!includeDeleted) {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            return [.. query.Where(where)];
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where, bool includeDeleted = false)
             => await _entities.Where(e => includeDeleted || EF.Property<bool>(e, "IsDeleted") == false)
                .Where(where)
                .ToListAsync();

        public bool Insert(T entity) {
            ArgumentNullException.ThrowIfNull(entity);
            try {
                _entities.Add(entity);
                return _context.SaveChanges() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Insert operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
             }
        }

        public async Task<bool> InsertAsync(T entity) {
             ArgumentNullException.ThrowIfNull(entity);
             try {
                await _entities.AddAsync(entity);
                return await _context.SaveChangesAsync() > 0;
             } catch (Exception ex) {
                _logger.LogToFile($"Insert operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
             }
        }

        public bool Update(T entity, bool includeDeleted = false) {
            ArgumentNullException.ThrowIfNull(entity);
            try {
                if (!includeDeleted) {
                    var entry = _context.Entry(entity);
                    var isDeleted = (bool)entry.Property("IsDeleted").CurrentValue;

                    if (isDeleted) {
                        _logger.LogToFile("Update operation skipped: Entity is marked as deleted.", "DBOPS");
                        return true;
                    }
                }

                _context.Update(entity);
                return _context.SaveChanges() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Update operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity, bool includeDeleted = false) {

            ArgumentNullException.ThrowIfNull(entity);

            try {
                if (!includeDeleted) {
                    var entry = _context.Entry(entity);
                    var isDeleted = (bool)entry.Property("IsDeleted").CurrentValue;

                    if (isDeleted) {
                        _logger.LogToFile("Update operation skipped: Entity is marked as deleted.", "DBOPS");
                        return true;
                    }
                }

                _context.Update(entity);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Update operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public bool Delete(T entity, bool markAsDeleted = false) {
            ArgumentNullException.ThrowIfNull(entity);
            try{ 
                 if (markAsDeleted) {
                    var entry = _context.Entry(entity);
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.State = EntityState.Modified;
                 } else {
                    _entities.Remove(entity);
                 }

               return _context.SaveChanges() > 0;
            } catch(Exception ex) {
                _logger.LogToFile($"Delete operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity, bool markAsDeleted = false) {

            ArgumentNullException.ThrowIfNull(entity);

            try {
                if (markAsDeleted) {
                    var entry = _context.Entry(entity);
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.State = EntityState.Modified;
                } else {
                    _entities.Remove(entity);
                }

                return await _context.SaveChangesAsync() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Delete operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public bool Exists(Expression<Func<T, bool>> where, bool excludeDeleted = false) {
            var record =  _entities.FirstOrDefault(where);
            
            if (excludeDeleted) {
                return record != null && !record.IsDeleted;
            }

            return record == null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> where, bool excludeDeleted = false) {
            var record = await _entities.FirstOrDefaultAsync(where);

            if (excludeDeleted) {
                return record != null && !record.IsDeleted;
            }

            return record == null;
        }

        public async Task<bool> BulkyInsertAsync(T[] entities) {
            if (entities is null || entities.Length == 0) {
                _logger.LogToFile("Bulk insert operation canceled: Empty list.", "DBOPS");
                return false;
            }

            var validEntities = entities.Where(e => e is not null).ToList();
            if (validEntities.Count == 0) {
                _logger.LogToFile("Bulk insert operation canceled: Only null values found.", "DBOPS");
                return false;
            }

            _logger.LogToFile($"Bulk insert operation started: {validEntities.Count} entities.", "DBOPS");

            var bulkConfig = new BulkConfig {
                SetOutputIdentity = false,
                PreserveInsertOrder = false,
                SqlBulkCopyOptions = SqlBulkCopyOptions.KeepIdentity
            };

            try {
                await _context.BulkInsertAsync(validEntities, bulkConfig);
                _logger.LogToFile("Bulk insert operation completed successfully.", "DBOPS");
                return true;
            } catch (Exception ex) {
                _logger.LogToFile($"Bulk insert operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }


        public async Task<bool> BulkyUpdateAsync(T[] entities) {
            if (entities is null || entities.Length == 0) {
                _logger.LogToFile("Bulk update operation canceled: Empty list.", "DBOPS");
                return false;
            }

            var validEntities = entities.Where(e => e is not null).ToList();
            if (validEntities.Count == 0) {
                _logger.LogToFile("Bulk update operation canceled: Only null values found.", "DBOPS");
                return false;
            }

            var bulkConfig = new BulkConfig {
                SetOutputIdentity = true,
                PreserveInsertOrder = true,
                //update all properties
                UpdateByProperties = null  
            };

            try {
                await _context.BulkUpdateAsync(validEntities, bulkConfig);
                return true;
            } catch (Exception ex) {
                 _logger.LogToFile($"Bulk update operation. Error! {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> BulkyUpdateAsync(T[] entities, params Expression<Func<T, object>>[] propertySelectors) {
            if (entities is null || entities.Length == 0) {
                _logger.LogToFile("Bulk update operation canceled: Empty list.", "DBOPS");
                return false;
            }

            var validEntities = entities.Where(e => e is not null).ToList();
            if (validEntities.Count == 0) {
                _logger.LogToFile("Bulk update operation canceled: Only null values found.", "DBOPS");
                return false;
            }

            // Convert expressions to property names
            var propertyNames = propertySelectors
                .Select(GetPropertyName)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            var bulkConfig = new BulkConfig {
                SetOutputIdentity = true,
                PreserveInsertOrder = true,
                //update selected properties
                UpdateByProperties = propertyNames
            };

            try {
                await _context.BulkUpdateAsync(validEntities, bulkConfig);
                return true;
            } catch (Exception ex) {
                 _logger.LogToFile($"Bulk update operation. Error! {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        #region Helper Methods
        private static string GetPropertyName(Expression<Func<T, object>> where) {
            if (where.Body is MemberExpression exMember) {
                return exMember.Member.Name;
            } else if (where.Body is UnaryExpression unary) {
                // Handle nullable properties
                if (unary.Operand is MemberExpression operand) {
                    return operand.Member.Name;
                }
            }

            return null;
        }

        #endregion

        
    }
}
