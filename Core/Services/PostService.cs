using Domain.Models;
using InfraStructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class PostService
    {
        //private readonly IBaseRepository<Post> _postRepo;
        //private readonly IPostRepo _post;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Action<string> _logAction;
        public PostService(/* IBaseRepository<Post> postRepo, IPostRepo post,*/ IUnitOfWork unitOfWork)
        {
            //_post = post;
            //_postRepo = postRepo;
            _logAction = message => Console.WriteLine(message);
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Post>> GetAllPosts(
            Expression<Func<Post, bool>> criteria = null,
            Expression<Func<Post, object>>[] includes = null)
        {
            //return await _post.GetAllPostsAsync();
            return await _unitOfWork._postRepo.GetAll(criteria, includes);
            
        }
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            //return await _postRepo.GetAllAsync();
            return await _unitOfWork._post.GetAllPostsAsync();
        }

        public Post GetPostById(int id)
        {
            return _unitOfWork._postRepo.GetById(id);
        }

        public async Task AddPost(Post post)
        {
            await _unitOfWork._postRepo.add(post, _logAction);
        }

        public void UpdatePost(Post post)
        {
            _unitOfWork._postRepo.update(post, _logAction);
        }
        public void DeletePost(int id)
        {
            _unitOfWork._postRepo.delete(id, _logAction);
        }

    }
}
