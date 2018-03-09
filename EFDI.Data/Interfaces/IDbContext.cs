using EFDI.CommonLibrary.Models.Base;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EFDI.Data.Interfaces
{
    public interface IDbContext
    {
        DbSet<TPersistenceModel> Set<TPersistenceModel>() where TPersistenceModel : PersistenceModelBase;

        int SaveChanges();

        DbEntityEntry Entry(object o);

        void Dispose();
    }
}
