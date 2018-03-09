using EFDI.Domain.Models;
using EFDI.Domain.Mapping.Base;
using EFDI.Data.Models;

namespace EFDI.Domain.Mapping.ModelMappers
{
    public class RecipeMapper : ModelMapperBase<RecipeDomain, Recipe>
    {
        public override RecipeDomain ConvertToDomainModel(Recipe persistenceModel, RecipeDomain domainModel)
        {
            var mappedModel = domainModel ?? new RecipeDomain();

            if (persistenceModel == null)
                return mappedModel;

            mappedModel.RecipeId = persistenceModel.Id;
            mappedModel.RecipeName = persistenceModel.Name;
            mappedModel.CategoryId = persistenceModel.CategoryId;
            mappedModel.ObjectState = persistenceModel.ObjectState;

            return mappedModel;
        }

        public override Recipe ConvertToPersistenceModel(RecipeDomain domainModel, Recipe persistenceModel)
        {
            var mappedModel = persistenceModel ?? new Recipe();

            if (domainModel == null)
                return mappedModel;

            mappedModel.Id = domainModel.RecipeId;
            mappedModel.Name = domainModel.RecipeName;
            mappedModel.CategoryId = domainModel.CategoryId;
            mappedModel.ObjectState = domainModel.ObjectState;

            return mappedModel;
        }
    }
}
