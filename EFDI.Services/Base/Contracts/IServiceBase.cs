using EFDI.CommonLibrary.Models.Base;
using System.Collections.Generic;

namespace EFDI.Services.Base.Contracts
{
    public interface IServiceBase<TDomainModel> where TDomainModel : DomainModelBase
    {
        void Delete(object id);

        TDomainModel GetEntityById(object id);

        void Insert(TDomainModel entity);

        IEnumerable<TDomainModel> Retrieve();

        void Save();

        void Update(TDomainModel entity);
    }
}
