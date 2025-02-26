using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.Repositories {

    public class BranchRepository(OpsDbContext context) : Repository<Branch>(context), IBranchRepository {

        private readonly OpsDbContext _context = context;
    }

}
