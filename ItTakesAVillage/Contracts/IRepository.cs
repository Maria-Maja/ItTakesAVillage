using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItTakesAVillage.Contracts
{
    public interface IRepository<T>
    {
        Task AddAsync(T t);
        Task<List<T>> GetAsync();
        Task<T?> GetAsync(int id);
        Task<T?> GetAsync(string id);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> expression);
        Task<List<UserGroup>> GetByIncludeFilterAsync(string userId);
    }
}
