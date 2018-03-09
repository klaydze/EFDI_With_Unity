using EFDI.Domain.Models;
using EFDI.Services.Base.Contracts;

namespace EFDI.Services.Contracts
{
    public interface IRecipeService : IServiceBase<RecipeDomain>
    {
        /*Recipe AddRecipe(Recipe recipe);
        List<Recipe> GetAllRecipes();*/

        RecipeDomain GetEntityById(int Id);
    }
}
