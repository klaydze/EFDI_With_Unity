using EFDI.Domain.Models;
using EFDI.Domain.Mapping.Interfaces.Facades;
using EFDI.Services.Base.Implementations;
using EFDI.Services.Contracts;

namespace EFDI.Services.Implementations
{
    public class RecipeService : ServiceBase<RecipeDomain>, IRecipeService
    {
        public RecipeService(IRepositoryFacadeBase<RecipeDomain> facade)
            : base(facade)
        {
            
        }

        public RecipeDomain GetEntityById(int Id)
        {
            var repositoryFacade = RepositoryFacade as IRecipeRepositoryFacade;

            return repositoryFacade != null
                    ? repositoryFacade.FindByRecipeId(Id)
                    : null;
        }

        /*private readonly MRSEntities _context;

        public RecipeService(MRSEntities context)
        {
            _context = context;
        }

        public Recipe AddRecipe(Recipe recipe)
        {
            var newRecipe = _context.Recipes.Add(recipe);
            _context.SaveChanges();

            return newRecipe;
        }

        public List<Recipe> GetAllRecipes()
        {
            return _context.Recipes.ToList();
        }*/
    }
}
