using EFDI.CommonLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDI.Domain.Mapping.Interfaces.Facades
{
    public interface IRepositoryFacadeBase<TDomainModel> where TDomainModel : DomainModelBase
    {
        void Delete(object id);

        void Delete(TDomainModel domain);

        TDomainModel FindEntityById(object id);

        void Insert(TDomainModel domain);

        IEnumerable<TDomainModel> Retrieve();

        void Save();

        void Update(TDomainModel domain);
    }
}
