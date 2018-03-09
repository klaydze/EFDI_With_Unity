using EFDI.CommonLibrary.Models.Base;
using System.Collections.Generic;
using System.Linq;

namespace EFDI.Repository.Contracts
{
    public interface IRepository<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        void Delete(object id);

        void Delete(TPersistenceModel entity);

        void Insert(TPersistenceModel entity);

        void InsertRange(IEnumerable<TPersistenceModel> entities);

        IQueryFluent<TPersistenceModel> Query();

        IQueryable<TPersistenceModel> SelectAll();

        IQueryable<TPersistenceModel> SelectAll(string sqlQuery, params object[] parameters);

        void Update(TPersistenceModel entity);
    }
}
