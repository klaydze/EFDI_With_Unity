using EFDI.CommonLibrary.Models.Contracts;
using EFDI.CommonLibrary.Models.Enums;
using System;
using System.Data.Entity;
using System.Linq;

namespace EFDI.Data.Extensions
{
    public static class DbContextExtension
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IObjectState")]
        public static void ApplyStateChanges(this DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            CheckObjectStateImplemented(context);

            foreach (var dbEntityEntry in context.ChangeTracker.Entries<IObjectState>())
            {
                var entity = dbEntityEntry.Entity;
                if (!dbEntityEntry.State.Equals(EntityState.Unchanged))
                    dbEntityEntry.State = ConvertState(entity.ObjectState);
            }
        }

        private static void CheckObjectStateImplemented(DbContext context)
        {
            var isObjectStateImplemented = context.ChangeTracker.Entries().Any(o => o.Entity is IObjectState);
            if (!isObjectStateImplemented)
            {
                throw new NotImplementedException(
                    "All entities must implement the IObjectState interface, this interface " +
                    "must be implemented so each entity's state can explicitly be determined when updating graphs.");
            }
        }

        private static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;

                case ObjectState.Modified:
                    return EntityState.Modified;

                case ObjectState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
