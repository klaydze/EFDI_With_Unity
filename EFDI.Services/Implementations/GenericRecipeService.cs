using EFDI.Data.Context;
using EFDI.Data.Models;
using EFDI.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EFDI.Services.Implementations
{
    public class GenericRecipeService : IGenericService<Recipe>
    {
        private readonly EFDIDatabaseContainer _context;

        public GenericRecipeService(EFDIDatabaseContainer context)
        {
            _context = context;
        }
        
        public Recipe Add(Recipe value)
        {
            var newRecipe = _context.Recipes.Add(value);
            _context.SaveChanges();

            return newRecipe;
        }

        public List<Recipe> GetItems()
        {
            return _context.Recipes.ToList();
        }
    }
}
