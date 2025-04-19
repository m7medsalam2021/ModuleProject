using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> criteria = null,
            Expression<Func<T, object>>[] includes = null
        );
        T GetById(int id);
        Task add(T entity, Action<string> LogAction);
        Task update(T entity, Action<string> LogAction);
        Task delete(int id, Action<string> LogAction);
    }

}
