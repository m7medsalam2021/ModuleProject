using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        // generic
        IBaseRepository<Post> _postRepo { get; }
        IBaseRepository<Category> _category { get; }
        // interface 
        IPostRepo _post { get; }

        int Complete();
        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}
