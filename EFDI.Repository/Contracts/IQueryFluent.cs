using EFDI.CommonLibrary.Models.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFDI.Repository.Contracts
{
    public interface IQueryFluent<TPersistenceModel> : IQueryFluentFilter<TPersistenceModel>,
                                                      IQueryFluentInclude<TPersistenceModel>,
                                                      IQueryFluentOrder<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        IQueryable<TPersistenceModel> Retrieve();

        IQueryable<TResult> Retrieve<TResult>(Expression<Func<TPersistenceModel, TResult>> selectorExpression);
    }
}
