using EFDI.Data.Models;
using System.Collections.Generic;
namespace EFDI.Services.Contracts
{
    public interface ICategoryService
    {
        Category AddCategory(Category category);
        List<Category> GetAllCategories();
    }
}
