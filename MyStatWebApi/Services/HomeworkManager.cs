using System.Collections;
using Microsoft.EntityFrameworkCore;
using MyStatWebApi.Models;

namespace MyStatWebApi.Services;

public class HomeworkManager : IHomeworkManager
{
    private readonly MyStatDbContext _dbContext;

    public HomeworkManager(MyStatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Homework?> GetByIdAsync(int? id)
    {
        if (id == null)
        {
            return null;
        }

        return await _dbContext.Homeworks.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> AddAsync(Homework homework)
    {
        try
        {
            await _dbContext.Homeworks.AddAsync(homework);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerator<Homework> GetEnumerator()
    {
        return ((IEnumerable<Homework>)_dbContext.Homeworks).GetEnumerator();
    }

    public async Task<bool> RemoveAsync(int? id)
    {
        try
        {
            var result = await GetByIdAsync(id);

            if (result == null) return false;
            
            _dbContext.Homeworks.Remove(result);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}