using EFDI.Data.Context;
using EFDI.Data.Models;
using EFDI.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EFDI.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly EFDIDatabaseContainer _context;

        public CategoryService(EFDIDatabaseContainer context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
            var newCategory = _context.Categories.Add(category);
            _context.SaveChanges();

            return newCategory;
        }

        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
    }
}
