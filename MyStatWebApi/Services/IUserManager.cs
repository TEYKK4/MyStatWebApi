using MyStatWebApi.Models;

namespace MyStatWebApi.Services;

public interface IUserManager : IEnumerable<User>
{
    Task<bool> AddAsync(User user);
    Task<bool> RemoveAsync(int? id);
    Task<User?> GetByIdAsync(int? id);
    Task<User?> GetByLoginPasswordAsync(string login, string password);
}