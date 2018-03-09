using EFDI.CommonLibrary.Models.Base;
using EFDI.Domain.Mapping.Interfaces.Facades;
using EFDI.Domain.Mapping.Interfaces.Mappers;
using EFDI.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFDI.CommonLibrary.Extensions;

namespace EFDI.Domain.Mapping.Base
{
    public abstract class RepositoryFacadeBase<TDomainModel, TPersistenceModel> : IRepositoryFacadeBase<TDomainModel>
        where TDomainModel : DomainModelBase
        where TPersistenceModel : PersistenceModelBase
    {
        protected RepositoryFacadeBase(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            UnitOfWork = unitOfWork;
            Repository = unitOfWork.Repository<TPersistenceModel>();
            //IgnoreMemberList = GetAutoMapperIgnoreMembers();
        }

        //internal string[] IgnoreMemberList { get; private set; }

        internal IModelMapperBase<TDomainModel, TPersistenceModel> Mapper { get; private set; }

        internal IRepository<TPersistenceModel> Repository { get; private set; }

        internal IUnitOfWork UnitOfWork { get; private set; }

        public virtual void Delete(object id)
        {
            Repository.Delete(id);
        }

        public virtual void Delete(TDomainModel domain)
        {
            Repository.Delete(ConvertToPersistenceModel(domain));
        }

        public virtual IEnumerable<TDomainModel> FilterEntities(Expression<Func<TPersistenceModel, bool>> filter)
        {
            var persistents = Filter(filter);
            return ConvertToDomainModelList(persistents.ToList());
        }

        public virtual TDomainModel FindEntityByField(Expression<Func<TPersistenceModel, bool>> filter)
        {
            var persistent = Find(filter);
            return ConvertToDomainModel(persistent);
        }

        public virtual TDomainModel FindEntityById(object id)
        {
            var persistent = Find(GetEntityIdExpression(id));
            return ConvertToDomainModel(persistent);
        }

        public virtual void Insert(TDomainModel domain)
        {
            Repository.Insert(ConvertToPersistenceModel(domain));
        }

        public IQueryFluent<TPersistenceModel> Query()
        {
            return Repository.Query();
        }

        public virtual IEnumerable<TDomainModel> Retrieve()
        {
            var persistents = GetQuery().Retrieve();
            return ConvertToDomainModelList(persistents.ToList());
        }

        public void Save()
        {
            UnitOfWork.Save();
        }

        public virtual void Update(TDomainModel domain)
        {
            Repository.Update(ConvertToPersistenceModel(domain));
        }

        protected TDomainModel ConvertToDomainModel(TPersistenceModel persistent)
        {
            if (persistent == null)
                return null;
            
            Mapper = GetMapper();
            return Mapper != null
                       ? Mapper.ConvertToDomainModel(persistent)
                       : persistent.Convert<TPersistenceModel, TDomainModel>(GetAutoMapperIgnoreMembers());
        }

        protected IList<TDomainModel> ConvertToDomainModelList(IList<TPersistenceModel> persistentList)
        {
            if (persistentList == null || !persistentList.Any())
                return null;

            Mapper = GetMapper();
            return Mapper != null
                ? Mapper.ConvertToDomainModelList(persistentList)
                : persistentList.Select(persistent => persistent
                    .Convert<TPersistenceModel, TDomainModel>(GetAutoMapperIgnoreMembers())).ToList();
        }

        protected TPersistenceModel ConvertToPersistenceModel(TDomainModel domain)
        {
            if (domain == null)
                return null;

            Mapper = GetMapper();
            return Mapper != null
                       ? Mapper.ConvertToPersistenceModel(domain)
                       : domain.Convert<TDomainModel, TPersistenceModel>(GetAutoMapperIgnoreMembers());
        }

        protected abstract string[] GetAutoMapperIgnoreMembers();

        protected abstract Expression<Func<TPersistenceModel, bool>> GetEntityIdExpression(object id);

        protected abstract IModelMapperBase<TDomainModel, TPersistenceModel> GetMapper();

        //protected abstract List<Expression<Func<TPersistenceModel, object>>> IncludeEntityProperties();

        //protected abstract IOrderedQueryable<TPersistenceModel> OrderBy(IQueryable<TPersistenceModel> arg);

        protected IQueryFluent<TPersistenceModel> GetQuery()
        {
            var queryIncludeProperties = GetQueryIncludingProperties();

            //return queryIncludeProperties != null
            //           ? queryIncludeProperties.OrderBy(OrderBy)
            //           : Query();

            return queryIncludeProperties ?? Query();
        }

        private IEnumerable<TPersistenceModel> Filter(Expression<Func<TPersistenceModel, bool>> filter)
        {
            return GetQuery().Filter(filter).Retrieve();
        }

        private TPersistenceModel Find(Expression<Func<TPersistenceModel, bool>> filter)
        {
            return GetQuery().Filter(filter).Retrieve().FirstOrDefault();
        }

        private IQueryFluent<TPersistenceModel> GetQueryIncludingProperties()
        {
            //var queryIncludeProperties = IncludeEntityProperties() == null
            //                                 ? Query()
            //                                 : Query().Include(IncludeEntityProperties());
            //return queryIncludeProperties;

            return Query();
        }
    }
}
