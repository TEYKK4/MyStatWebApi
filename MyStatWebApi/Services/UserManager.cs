using System.Collections;
using Microsoft.EntityFrameworkCore;
using MyStatWebApi.Models;

namespace MyStatWebApi.Services;

public class UserManager : IUserManager
{
    private readonly MyStatDbContext _dbContext;

    public UserManager(MyStatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerator<User> GetEnumerator()
    {
        return ((IEnumerable<User>)_dbContext.Users).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public async Task<bool> AddAsync(User user)
    {
        if (await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == user.Login) is not null)
        {
            return false;
        }
        
        try
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveAsync(int? id)
    {
        try
        {
            var result = await GetByIdAsync(id);

            if (result == null) return false;
            
            _dbContext.Users.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<User?> GetByIdAsync(int? id)
    {
        if (id == null)
        {
            return null;
        }

        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByLoginPasswordAsync(string login, string password)
    {
        if (string.IsNullOrWhiteSpace(login) && string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Login == login && u.Password == password);
    }
}