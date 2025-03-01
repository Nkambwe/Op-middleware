using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.Repositories {

    public class BranchRepository(IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<Branch>(contextFactory), IBranchRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;
    }

}
