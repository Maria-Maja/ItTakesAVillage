using ItTakesAVillage.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItTakesAVillage.Repository
{
    public interface IRepository<T>
    {
        Task AddAsync(T t);
        Task GetAsync();
        Task GetAsync(int id);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
        Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> expression);
    }
}
