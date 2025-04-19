using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IRepository
{
    public interface IPostRepo
    {
        public Task<IEnumerable<Post>> GetAllPostsAsync();
        public Post GetPostById(int id);
        public void AddPost(Post post);
        public void UpdatePost(Post post);
        public void DeletePost(Post post);



    }
}
