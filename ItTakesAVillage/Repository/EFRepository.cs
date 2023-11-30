using ItTakesAVillage.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItTakesAVillage.Repository
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly ItTakesAVillageContext _context;
        public EFRepository(ItTakesAVillageContext context)
        {
            _context = context;
        }
        public Task AddAsync(T t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public Task UpdateAsync(T t)
        {
            throw new NotImplementedException();
        }
    }
}
