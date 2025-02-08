using Operators.Moddleware.Data.Entities;

namespace Operators.Moddleware.Data.Repositories {

    public class BranchRepository: Repository<Branch>, IBranchRepository {

        private readonly OpsDbContext _context;

        public BranchRepository(OpsDbContext context) : base(context) {
            _context = context;
        }
    }

}
