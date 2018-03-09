using EFDI.CommonLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFDI.Repository.Contracts
{
    public interface IQueryFluentFilter<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        IQueryFluent<TPersistenceModel> Filter(List<Expression<Func<TPersistenceModel, bool>>> filterExpressions);

        IQueryFluent<TPersistenceModel> Filter(Expression<Func<TPersistenceModel, bool>> filterExpression);
    }
}
