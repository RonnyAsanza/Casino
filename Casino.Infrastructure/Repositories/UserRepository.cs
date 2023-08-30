using Casino.Domain.Entities;
using Casino.Domain.Interfaces.Repositories;
using Casino.Infrastructure.Persistence;

namespace Casino.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CasinoDbContext _context;

        public UserRepository(CasinoDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
