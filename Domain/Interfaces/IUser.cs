

using Domain.Entities;

namespace Domain.Interfaces;
    public interface IUser : IGenericRepository<User>
    {
        Task<User> GetByIdAsync(int id);
        Task<bool> IsExists (string userName);
    }
