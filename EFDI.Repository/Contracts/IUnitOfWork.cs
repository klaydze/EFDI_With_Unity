using EFDI.CommonLibrary.Models.Base;

namespace EFDI.Repository.Contracts
{
    public interface IUnitOfWork
    {
        void Dispose();

        void Save();

        void Dispose(bool disposing);

        IRepository<TPersistenceModel> Repository<TPersistenceModel>() where TPersistenceModel : PersistenceModelBase;
    }
}
