using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Helpers;
using System.Linq.Expressions;

namespace Operators.Moddleware.Data.Repositories {

    public class Repository<T>(IDbContextFactory<OpsDbContext> contextFactory) : IRepository<T> where T : DomainEntity {

        private readonly IDbContextFactory<OpsDbContext> _contextFactory = contextFactory;
        private readonly IServiceLogger _logger = new ServiceLogger("Operations_data") {
            Channel = "REPOSITORY"
        };

        public T Get(long id, bool includeDeleted = false) {
            T entity = null;
            using var context = _contextFactory.CreateDbContext();
            var entities = context.Set<T>();

            try {

                var query = entities.AsQueryable();
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
            using var context = _contextFactory.CreateDbContext();
            var entities = context.Set<T>();
            try {
                var query = entities.AsQueryable();
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
            using var context = _contextFactory.CreateDbContext();
            var entities = context.Set<T>();
            try {
                var query = entities.AsQueryable();

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
            using var context = _contextFactory.CreateDbContext();
            var entities = context.Set<T>();
            try {
                var query = entities.AsQueryable();

                //include related entities
                if (filters != null) {
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> where, bool includeDeleted = false) {

            using var context = _contextFactory.CreateDbContext();
            var entities = context.Set<T>();
            try {
                var query = entities.AsQueryable();
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

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool includeDeleted = false, params Expression<Func<T, object>>[] filters) {

            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();

            // Apply the deleted filter if needed
            if (!includeDeleted) {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            // Apply predicate
            query = query.Where(predicate);

            // Apply LEFT JOIN for each filter using Include
            foreach (var filter in filters) {
                //  we need to use Include() which creates LEFT JOIN in EF Core
                query = query.Include(filter);
            }

            return await query.FirstOrDefaultAsync();
        }


        public IList<T> GetAll(bool includeDeleted = false) {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();

            if (!includeDeleted) {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            return [.. query];
        }

        public async Task<IList<T>> GetAllAsync(bool includeDeleted = false) {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();
            return await query.Where(e => includeDeleted || EF.Property<bool>(e, "IsDeleted") == false).ToListAsync();
        }


        public IList<T> GetAll(Expression<Func<T, bool>> where, bool includeDeleted = false) {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();

            if (!includeDeleted) {
                query = query.Where(e => EF.Property<bool>(e, "IsDeleted") == false);
            }

            return [.. query.Where(where)];
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> where, bool includeDeleted = false) {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();
            return await query.Where(e => includeDeleted || EF.Property<bool>(e, "IsDeleted") == false)
                .Where(where).ToListAsync();
        }

        public bool Insert(T entity) {
            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();

            try {
                dbSet.Add(entity);
                return context.SaveChanges() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Insert operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> InsertAsync(T entity) {
            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();
            try {
                await dbSet.AddAsync(entity);
                return await context.SaveChangesAsync() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Insert operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public bool Update(T entity, bool includeDeleted = false) {
            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            try {
                if (!includeDeleted) {
                    var entry = context.Entry(entity);
                    var isDeleted = (bool)entry.Property("IsDeleted").CurrentValue;

                    if (isDeleted) {
                        _logger.LogToFile("Update operation skipped: Entity is marked as deleted.", "DBOPS");
                        return true;
                    }
                }

                context.Update(entity);
                return context.SaveChanges() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Update operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity, bool includeDeleted = false) {

            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            try {
                if (!includeDeleted) {
                    var entry = context.Entry(entity);
                    var isDeleted = (bool)entry.Property("IsDeleted").CurrentValue;

                    if (isDeleted) {
                        _logger.LogToFile("Update operation skipped: Entity is marked as deleted.", "DBOPS");
                        return true;
                    }
                }

                context.Update(entity);
                return await context.SaveChangesAsync() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Update operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public bool Delete(T entity, bool markAsDeleted = false) {
            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();
            try {
                if (markAsDeleted) {
                    var entry = context.Entry(entity);
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.State = EntityState.Modified;
                } else {
                    dbSet.Remove(entity);
                }

                return context.SaveChanges() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Delete operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity, bool markAsDeleted = false) {

            ArgumentNullException.ThrowIfNull(entity);

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();
            try {
                if (markAsDeleted) {
                    var entry = context.Entry(entity);
                    entry.Property("IsDeleted").CurrentValue = true;
                    entry.State = EntityState.Modified;
                } else {
                    dbSet.Remove(entity);
                }

                return await context.SaveChangesAsync() > 0;
            } catch (Exception ex) {
                _logger.LogToFile($"Delete operation failed: {ex.Message}", "DBOPS");
                _logger.LogToFile("STACKTRACE ::", "DBOPS");
                _logger.LogToFile($"{ex.StackTrace}", "ERROR");
                return false;
            }
        }

        public bool Exists(Expression<Func<T, bool>> where, bool excludeDeleted = false) {

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();
            var record = dbSet.FirstOrDefault(where);

            if (excludeDeleted) {
                return record != null && !record.IsDeleted;
            }

            return record == null;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> where, bool excludeDeleted = false) {

            using var context = _contextFactory.CreateDbContext();
            var dbSet = context.Set<T>();
            var record = await dbSet.FirstOrDefaultAsync(where);

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

                using var context = _contextFactory.CreateDbContext();
                await context.BulkInsertAsync(validEntities, bulkConfig);
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

                using var context = _contextFactory.CreateDbContext();
                await context.BulkUpdateAsync(validEntities, bulkConfig);
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
                using var context = _contextFactory.CreateDbContext();
                await context.BulkUpdateAsync(validEntities, bulkConfig);
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
