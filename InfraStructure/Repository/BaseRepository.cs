using InfraStructure.Data;
using InfraStructure.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfraStructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> criteria = null, 
            Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if(criteria is not null)
            {
                query = query.Where(criteria);
            }
            if(includes is not null)
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task add(T entity, Action<string> LogAction)
        {
            LogAction?.Invoke($"{typeof(T).Name} Add Successfully");
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task update(T entity, Action<string> LogAction)
        {
            LogAction?.Invoke($"{typeof(T).Name} Update Successfully");
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task delete(int id, Action<string> LogAction)
        {
            var item = GetById(id);
            if (item is not null)
            {
                LogAction?.Invoke($"{typeof(T).Name} Delete Successfully");
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                LogAction?.Invoke($"{typeof(T).Name} Not Found");
            }
        }

        
    }



}
