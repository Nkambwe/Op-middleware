using Microsoft.EntityFrameworkCore;
using Operators.Moddleware.Data.Entities.Access;

namespace Operators.Moddleware.Data.Repositories.access {

    public class UserRepository (IDbContextFactory<OpsDbContext> contextFactory) :
        Repository<User>(contextFactory), IUserRepository {

        private readonly IDbContextFactory<OpsDbContext>  _context = contextFactory;

    }
}
