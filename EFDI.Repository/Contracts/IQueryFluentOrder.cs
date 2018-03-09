using EFDI.CommonLibrary.Models.Base;
using System;
using System.Linq;

namespace EFDI.Repository.Contracts
{
    public interface IQueryFluentOrder<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        IQueryFluent<TPersistenceModel> OrderBy(Func<IQueryable<TPersistenceModel>, IOrderedQueryable<TPersistenceModel>> orderByFunc);
    }
}
