using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Repositories;

namespace Operators.Moddleware.Data.Transactions {
    public class UnitOfWork(IDbContextFactory<OpsDbContext> contextFactory, 
        IPermissionRepository permissionRepository) : IUnitOfWork {

        private readonly IDbContextFactory<OpsDbContext> _contextFactory = contextFactory;
        private readonly Dictionary<Type, object> _repositories = [];

        private IPermissionRepository _permissionRepository = permissionRepository;
        public IPermissionRepository PermissionRepository => _permissionRepository;

        /// <summary>
        /// Get an instance of a repository class of type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Enity type repository belongs to</typeparam>
        /// <returns>Repositoy of type specified</returns>
        public IRepository<T> GetRepository<T>() where T : DomainEntity {
            if (_repositories.TryGetValue(typeof(T), out var repo)) {
                return (IRepository<T>)repo;
            }

            var repository = new Repository<T>(_contextFactory);
            _repositories[typeof(T)] = repository;
            return repository;
        }

        private bool _disposed;
        /// <summary>
        /// Manual disposal of unit of work
        /// </summary>
        /// <param name="isManuallyDisposing"></param>
        protected virtual void Dispose(bool isManuallyDisposing) {
            if (!_disposed) {
                if (isManuallyDisposing) {
                    _permissionRepository = null;
                }
            }

            _disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() { Dispose(false); }


    }
}
