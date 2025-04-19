using Domain.Models;
using InfraStructure.Data;
using InfraStructure.IRepository;
using InfraStructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public class PostRepo : IPostRepo
    {

        private readonly AppDbContext _context;
        public PostRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Category).ToListAsync();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public void AddPost(Post post)
        {
            throw new NotImplementedException();
        }

        public void DeletePost(Post post)
        {
            throw new NotImplementedException();
        }

       
        public void UpdatePost(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
