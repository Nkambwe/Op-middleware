using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Repositories;

namespace Operators.Moddleware.Data.Transactions {

    /// <summary>
    /// Unit of Work factory class
    /// </summary>
    public class UnitOfWorkFactory(IServiceProvider serviceProvider, 
                                   IDbContextFactory<OpsDbContext> contextFactory)
                                   :IUnitOfWorkFactory {

        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly IDbContextFactory<OpsDbContext> _contextFactory = contextFactory;

        /// <summary>
        /// Creates a new instance of UnitOfWork.
        /// </summary>
        /// <returns>Created instance of IUnitOfWork</returns>
        public IUnitOfWork Create()
            => new UnitOfWork(_contextFactory, _serviceProvider.GetRequiredService<IPermissionRepository>());
    }
}
