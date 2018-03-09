using EFDI.CommonLibrary.Models.Base;
using EFDI.Repository.Contracts;
using EFDI.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EFDI.Repository.Helpers
{
    public sealed class QueryFluent<TPersistenceModel> : IQueryFluent<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        private readonly List<Expression<Func<TPersistenceModel, bool>>> _filters;
        private readonly List<Expression<Func<TPersistenceModel, object>>> _includeEntityProperties;
        private readonly Repository<TPersistenceModel> _repository;
        private Func<IQueryable<TPersistenceModel>, IOrderedQueryable<TPersistenceModel>> _orderBy;

        public QueryFluent(Repository<TPersistenceModel> repository)
        {
            _repository = repository;
            _includeEntityProperties = new List<Expression<Func<TPersistenceModel, object>>>();
            _filters = new List<Expression<Func<TPersistenceModel, bool>>>();
        }

        public IQueryFluent<TPersistenceModel> Filter(Expression<Func<TPersistenceModel, bool>> filterExpression)
        {
            _filters.Add(filterExpression);
            return this;
        }

        public IQueryFluent<TPersistenceModel> Filter(List<Expression<Func<TPersistenceModel, bool>>> filterExpressions)
        {
            _filters.AddRange(filterExpressions);
            return this;
        }

        public IQueryFluent<TPersistenceModel> Include(List<Expression<Func<TPersistenceModel, object>>> includePropertyExpressions)
        {
            _includeEntityProperties.AddRange(includePropertyExpressions);
            return this;
        }

        public IQueryFluent<TPersistenceModel> Include(Expression<Func<TPersistenceModel, object>> includePropertyExpression)
        {
            _includeEntityProperties.Add(includePropertyExpression);
            return this;
        }

        public IQueryFluent<TPersistenceModel> OrderBy(Func<IQueryable<TPersistenceModel>, IOrderedQueryable<TPersistenceModel>> orderByFunc)
        {
            _orderBy = orderByFunc;
            return this;
        }

        public IQueryable<TPersistenceModel> Retrieve()
        {
            return _repository.Retrieve(_filters, _includeEntityProperties, _orderBy);
        }

        public IQueryable<TResult> Retrieve<TResult>(Expression<Func<TPersistenceModel, TResult>> selectorExpression)
        {
            return _repository.Retrieve(_filters, _includeEntityProperties, _orderBy).Select(selectorExpression);
        }
    }
}
