using Core.Services;
using Domain.Models;
using InfraStructure.Helpers;
using InfraStructure.IRepository;
using InfraStructure.Repository;
using InfraStructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ProjectModule.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostService _postService;
        private readonly CategoryService _catService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly string[] _allowedExtentions = { ".jpg", ".png", ".jpeg" };
        private readonly IWebHostEnvironment _webHost;
        public PostsController(PostService postService, CategoryService categoryService, IWebHostEnvironment webHost, IUnitOfWork unitOfWork)
        {
            _postService = postService;
            _catService = categoryService;
            _webHost = webHost;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public  async Task<IActionResult> Index(int? categoryId)
        {

            ViewBag.Categories = await _catService.GetCategories();
            if (categoryId.HasValue)
            {
                // get posts by category
                // where post.categoryId = categoryId
                // Include category

                var postData = await _postService.GetAllPosts(
                    // where
                    p => p.CategoryId == categoryId,
                    // includes
                    new System.Linq.Expressions.Expression<Func<Post, object>>[]
                    {
                        p => p.Category
                    }
                );
                return View(postData.ToList());
            }



            var posts = await _postService.GetAllPosts();
            //return View(); // Controller : Post => Index : View
            //return View("getAll"); // Controller : Post => Index : GetAll
            return View(posts.ToList()); // Controller : Post => Index : var (posts)
            //return View("getAll", posts); // Controller : Post => Index : GetAll var (posts)
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var postViewModel = new PostViewModel();
            postViewModel.Categories = await _catService.GetCategoriesWithSelectListItems();
            return View(postViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                // extention validation 
                var fileExtention = Path.GetExtension(postViewModel.File.FileName).ToLower();
                bool isAllowed = _allowedExtentions.Contains(fileExtention);
                if (!isAllowed)
                {
                    ModelState.AddModelError("File", "File extention is not allowed");
                    postViewModel.Categories = await _catService.GetCategoriesWithSelectListItems();
                    return View(postViewModel);
                }
                // upload file
                postViewModel.Post.Image = await UploadFiles.UploadFile(postViewModel.File, "Posts", _webHost.WebRootPath);
                await _postService.AddPost(postViewModel.Post);
                return RedirectToAction(nameof(Index));
            }
            postViewModel.Categories = await _catService.GetCategoriesWithSelectListItems();
            return View(postViewModel);
        }

        public IActionResult Edit(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }
            _postService.UpdatePost(post);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> EditWithUnitOfWork(Post post)
        {

            // use transaction
            //1. begin transaction
            await _unitOfWork.BeginTransaction();
            //2. update 
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(post);
                }
                _postService.UpdatePost(post);
                // complete
                await _unitOfWork.CompleteAsync();
                // commit transaction
                await _unitOfWork.CommitTransaction();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // rollback transactoin
                await _unitOfWork.RollbackTransaction();
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IActionResult Details(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }

        public IActionResult Delete(int id)
        {
            var post = _postService.GetPostById(id);
            return View(post);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {

            if (ModelState.IsValid)
            {
                // get item 
                var PostData = _postService.GetPostById(id);
                // delete image
                string filePath = Path.Combine(_webHost.WebRootPath, PostData.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _postService.DeletePost(id);
                return RedirectToAction(nameof(Index));
            }
            var post = _postService.GetPostById(id);
            return View(post);
        }

    }
}
