using EFDI.CommonLibrary.Models.Base;
using EFDI.Domain.Mapping.Interfaces.Facades;
using EFDI.Services.Base.Contracts;
using System.Collections.Generic;

namespace EFDI.Services.Base.Implementations
{
    public abstract class ServiceBase<TDomainModel> : IServiceBase<TDomainModel>
        where TDomainModel : DomainModelBase
    {
        protected ServiceBase(IRepositoryFacadeBase<TDomainModel> repositoryFacade)
        {
            //Evaluate.ParameterNotNull(repositoryFacade, "repositoryFacade");
            RepositoryFacade = repositoryFacade;
        }

        internal IRepositoryFacadeBase<TDomainModel> RepositoryFacade { get; private set; }

        public virtual void Delete(object id)
        {
            RepositoryFacade.Delete(id);
        }

        public virtual TDomainModel GetEntityById(object id)
        {
            return RepositoryFacade.FindEntityById(id);
        }

        public virtual void Insert(TDomainModel entity)
        {
            RepositoryFacade.Insert(entity);
        }

        public virtual IEnumerable<TDomainModel> Retrieve()
        {
            return RepositoryFacade.Retrieve();
        }

        public virtual void Save()
        {
            RepositoryFacade.Save();
        }

        public virtual void Update(TDomainModel entity)
        {
            RepositoryFacade.Update(entity);
        }
    }
}
