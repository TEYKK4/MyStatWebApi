using MyStatWebApi.Models;

namespace MyStatWebApi.Services;

public interface IHomeworkManager : IEnumerable<Homework>
{
    Task<bool> AddAsync(Homework homework);
    Task<bool> RemoveAsync(int? id);
    Task<Homework?> GetByIdAsync(int? id);
}