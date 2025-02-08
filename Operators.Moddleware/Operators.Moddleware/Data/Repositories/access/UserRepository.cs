using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.Repositories.access {

    public class UserRepository : Repository<User>, IUserRepository {

        private readonly OpsDbContext _context;

        public UserRepository(OpsDbContext context) : base(context) {
            _context = context;
        }
    }
}
