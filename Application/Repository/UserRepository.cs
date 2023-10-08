

using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class UserRepository : GenericRepository<User>, IUser
{
    private readonly APIAuthTwoStepContext _context;
    public UserRepository(APIAuthTwoStepContext context) : base(context)
    {
        _context = context;
    }

    public async virtual Task<bool> IsExists (string userName)
    {
        if (!string.IsNullOrEmpty(userName))
        {
            var exists = await _context.Users.AnyAsync(p => p.UserName == userName);
            return exists; 
        }

        return false;
    }

}
