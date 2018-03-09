using EFDI.CommonLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDI.Domain.Mapping.Interfaces.Mappers
{
    public interface IModelMapperBase<TDomainModel, TPersistenceModel>
        where TDomainModel : DomainModelBase
        where TPersistenceModel : PersistenceModelBase
    {
        /// <summary>
        /// Converts a supplied persistence model object to a domain model object.
        /// </summary>
        /// <param name="persistenceModel">The persistence model.</param>
        /// <returns></returns>
        TDomainModel ConvertToDomainModel(TPersistenceModel persistenceModel);

        /// <summary>
        /// Converts a supplied persistence model object to a domain model object.
        /// </summary>
        /// <param name="persistenceModel">The persistence model.</param>
        /// <param name="domainModel">The domain model.</param>
        /// <returns></returns>
        TDomainModel ConvertToDomainModel(TPersistenceModel persistenceModel, TDomainModel domainModel);

        /// <summary>
        /// Converts a supplied list of persistence model objects to a list of domain model objects.
        /// </summary>
        /// <param name="persistenceModelList">The persistence model list.</param>
        /// <returns></returns>
        IList<TDomainModel> ConvertToDomainModelList(IList<TPersistenceModel> persistenceModelList);

        /// <summary>
        /// Converts a supplied domain model object to a persistence model object.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <returns></returns>
        TPersistenceModel ConvertToPersistenceModel(TDomainModel domainModel);

        /// <summary>
        /// Converts a supplied domain model object to a persistence model object.
        /// </summary>
        /// <param name="domainModel">The domain model.</param>
        /// <param name="persistenceModel">The persistence model.</param>
        /// <returns></returns>
        TPersistenceModel ConvertToPersistenceModel(TDomainModel domainModel, TPersistenceModel persistenceModel);

        /// <summary>
        /// Converts a supplied list of domain model objects to a list of persistence model objects.
        /// </summary>
        /// <param name="domainModelList">The domain model list.</param>
        /// <returns></returns>
        IList<TPersistenceModel> ConvertToPersistenceModelList(IList<TDomainModel> domainModelList);
    }
}
