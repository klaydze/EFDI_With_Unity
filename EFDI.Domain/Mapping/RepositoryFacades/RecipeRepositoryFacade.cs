using System;
using EFDI.Domain.Mapping.Base;
using EFDI.Domain.Mapping.Interfaces.Facades;
using EFDI.Domain.Mapping.Interfaces.Mappers;
using EFDI.Domain.Models;
using EFDI.Repository.Contracts;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;
using EFDI.Data.Models;

namespace EFDI.Domain.Mapping.RepositoryFacades
{
    public class RecipeRepositoryFacade : RepositoryFacadeBase<RecipeDomain, Recipe>, IRecipeRepositoryFacade
    {
        [Dependency]
        public IModelMapperBase<RecipeDomain, Recipe> RecipeMapper { get; set; }

        public RecipeRepositoryFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        public RecipeDomain FindByRecipeId(int Id)
        {
            return FindEntityByField(x => x.Id == Id);
        }

        protected override string[] GetAutoMapperIgnoreMembers()
        {
            return null;
        }

        protected override Expression<Func<Recipe, bool>> GetEntityIdExpression(object id)
        {
            string entityId = id != null
                                ? id.ToString()
                                : "0";
            int entity;
            if (!int.TryParse(entityId, out entity))
            {
                entity = 0;
            };

            return recipe => recipe.Id == entity;
        }

        protected override IModelMapperBase<RecipeDomain, Recipe> GetMapper()
        {
            return RecipeMapper;
        }
    }
}
