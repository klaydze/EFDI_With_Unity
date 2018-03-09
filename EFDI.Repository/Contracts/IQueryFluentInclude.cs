using EFDI.CommonLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EFDI.Repository.Contracts
{
    public interface IQueryFluentInclude<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        IQueryFluent<TPersistenceModel> Include(List<Expression<Func<TPersistenceModel, object>>> includePropertyExpressions);

        IQueryFluent<TPersistenceModel> Include(Expression<Func<TPersistenceModel, object>> includePropertyExpression);
    }
}
