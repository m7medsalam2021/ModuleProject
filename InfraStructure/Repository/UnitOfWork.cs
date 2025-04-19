using Domain.Models;
using InfraStructure.Data;
using InfraStructure.IRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private  IDbContextTransaction _transaction;
        
        public IBaseRepository<Post> _postRepo { get; private set; }
        public IBaseRepository<Category> _category { get; private set; }
        public IPostRepo _post { get; private set; }

       

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _postRepo = new BaseRepository<Post>(_context); 
            _category = new BaseRepository<Category>(_context); 
            _post = new PostRepo(_context); 
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            try
            {
                if(_transaction is not null)
                {
                    await _transaction.CommitAsync();
                }
            }
            finally
            {
                if(_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransaction()
        {
            try
            {
                if(_transaction is not null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if(_transaction is not null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
