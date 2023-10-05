

using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class UserRepository : GenericRepository<User>, IUser
{
    private readonly APIAuthTwoStepContext _context;
    public UserRepository(APIAuthTwoStepContext context) : base(context)
    {
        _context = context;
    }
}
