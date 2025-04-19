using Domain.Models;
using InfraStructure.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService
    {
        //private readonly IBaseRepository<Category> _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Action<string> _logAction;
        public CategoryService(/*IBaseRepository<Category> categoryRepo,*/ IUnitOfWork unitOfWork)
        {
            //_categoryRepo = categoryRepo;
            _logAction = message => Console.WriteLine(message);
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesWithSelectListItems()
        {
            var Categories = await _unitOfWork._category.GetAllAsync();
            return Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            });
        }
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitOfWork._category.GetAllAsync();
        }

    }
}
