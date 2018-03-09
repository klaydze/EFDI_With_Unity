using EFDI.CommonLibrary.Models.Base;
using EFDI.Data.Context;
using EFDI.Data.Interfaces;
using EFDI.Repository.Contracts;
using System;
using System.Collections;

namespace EFDI.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbContext _context;

        private bool _disposed;

        private Hashtable _repositoryCollection;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public UnitOfWork()
        {
            _context = new EFDIDatabaseContainer();
        }

        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }

        public IRepository<TPersistenceModel> Repository<TPersistenceModel>() where TPersistenceModel : PersistenceModelBase
        {
            // If collection of activated repository instances
            // doesn't exist, create it.
            if (_repositoryCollection == null)
                _repositoryCollection = new Hashtable();

            // Get entity type name.
            var entityType = typeof(TPersistenceModel).Name;

            // Check if repository of type TPersistenceModel already exist in the
            // collection of activated repository instances.
            if (_repositoryCollection.ContainsKey(entityType))
                return (IRepository<TPersistenceModel>)_repositoryCollection[entityType];

            // If not, then add it to the collection.
            var repositoryType = typeof(Repository<>);

            // Create an instance of repository of type TPersistenceModel.
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TPersistenceModel)), _context);

            // Proceed on adding to repository collection.
            _repositoryCollection.Add(entityType, repositoryInstance);

            // Return the added or existing repository of type TPersistenceModel.
            return (IRepository<TPersistenceModel>)_repositoryCollection[entityType];
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
