using Operators.Moddleware.Data.Entities;
using Operators.Moddleware.Data.Repositories;

namespace Operators.Moddleware.Data.Transactions {

    public interface IUnitOfWork : IDisposable {

        IRepository<T> GetRepository<T>() where T : DomainEntity;
        IPermissionRepository PermissionRepository { get; }

    }

}
