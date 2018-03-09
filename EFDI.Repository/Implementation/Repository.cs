using EFDI.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using EFDI.CommonLibrary.Models.Base;
using System.Linq.Expressions;
using EFDI.CommonLibrary.Models.Contracts;
using EFDI.CommonLibrary.Models.Enums;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using LinqKit;
using EFDI.Repository.Helpers;
using EFDI.Data.Interfaces;

namespace EFDI.Repository.Implementation
{
    public class Repository<TPersistenceModel> : IRepository<TPersistenceModel> where TPersistenceModel : PersistenceModelBase
    {
        internal readonly IDbContext Context;

        internal readonly DbSet<TPersistenceModel> DbSet;

        // TODO: Get Session User
        private const string DefaultUser = @"Nextgen\NGAdmin";

        public Repository(IDbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Context = context;
            DbSet = Context.Set<TPersistenceModel>();
        }

        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);
            SetDeletedEntityState(entity);
        }

        public virtual void Delete(TPersistenceModel entity)
        {
            SetDeletedEntityState(entity);
        }

        public virtual void Insert(TPersistenceModel entity)
        {
            SetCreatedEntityValues(entity);
            SetCreatedEntityState(entity);
        }

        public virtual void InsertRange(IEnumerable<TPersistenceModel> entities)
        {
            if (entities == null)
                return;

            var persistenceModels = entities.ToList();
            if (!persistenceModels.Any())
                return;

            foreach (var entity in persistenceModels)
                Insert(entity);
        }

        public virtual IQueryFluent<TPersistenceModel> Query()
        {
            return new QueryFluent<TPersistenceModel>(this);
        }

        public IQueryable<TPersistenceModel> SelectAll()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<TPersistenceModel> SelectAll(string sqlQuery, params object[] parameters)
        {
            // As with any API that accepts SQL it is important to parameterize any user input
            // to protect against a SQL injection attack. You can include parameter place holders
            // in the SQL query string and then supply parameter values as additional arguments.
            // Any parameter values you supply will automatically be converted to a DbParameter.

            // ex. Repository.Select("SELECT * FROM dbo.Posts WHERE Author = @p0", userSuppliedAuthor);

            // Alternatively, you can also construct a DbParameter and supply it to SqlQuery.
            // This allows you to use named parameters in the SQL query string.

            // ex. Repository.Select("SELECT * FROM dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));

            return DbSet.SqlQuery(sqlQuery, parameters).AsQueryable();
        }

        public virtual void Update(TPersistenceModel entity)
        {
            SetModifiedEntityValues(entity);
            SetModifiedEntityState(entity);
        }

        internal IQueryable<TPersistenceModel> Retrieve(List<Expression<Func<TPersistenceModel, bool>>> filters = null,
                                            List<Expression<Func<TPersistenceModel, object>>> includeEntityProperties = null,
                                            Func<IQueryable<TPersistenceModel>, IOrderedQueryable<TPersistenceModel>> orderBy = null,
                                            int? page = null,
                                            int? pageSize = null)
        {
            IQueryable<TPersistenceModel> query = DbSet;

            if (filters != null && !filters.Count.Equals(0))
                query = filters.Aggregate(query, (current, filter) => current.AsExpandable().Where(filter));

            if (includeEntityProperties != null && !includeEntityProperties.Count.Equals(0))
                query = includeEntityProperties.Aggregate(query, (current, entityProperty) => current.Include(entityProperty));

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        private static object GetPrimaryKeyValue(DbEntityEntry entry, out string type)
        {
            var entity = entry.Entity;
            var property = entity.GetType()
                .GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            if (property != null)
            {
                type = property.PropertyType.ToString();
                return property.GetValue(entity, null);
            }
            type = string.Empty;
            return null;
        }

        private static void SetCreatedEntityValues(TPersistenceModel entity)
        {
            entity.CreatedBy = DefaultUser;
            //entity.CreatedDate = DateTime.UtcNow;
        }

        private static void SetModifiedEntityValues(TPersistenceModel entity)
        {
            entity.ModifiedBy = DefaultUser;
            entity.ModifiedDate = DateTime.UtcNow;
        }

        private static void SetModifiedState(TPersistenceModel modifiedEntity, DbEntityEntry entry)
        {
            entry.State = EntityState.Modified;
            ((IObjectState)modifiedEntity).ObjectState = ObjectState.Modified;
        }

        private void SetCreatedEntityState(TPersistenceModel entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            ((IObjectState)entity).ObjectState = ObjectState.Added;
        }

        private void SetDeletedEntityState(TPersistenceModel entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            ((IObjectState)entity).ObjectState = ObjectState.Deleted;
        }

        //private void SetModifiedEntityState(TPersistenceModel modifiedEntity)
        //{
        //    var entry = Context.Entry(modifiedEntity);
        //    try
        //    {
        //        SetModifiedState(modifiedEntity, entry);
        //    }
        //    catch (InvalidOperationException)
        //    {
        //        string propertyType;
        //        var keyValue = GetPrimaryKeyValue(entry, out propertyType);

        //        if (entry.State == EntityState.Detached)
        //        {
        //            var existingEntity = DbSet.Find(keyValue);
        //            if (existingEntity != null)
        //            {
        //                var mergeEntry = Context.Entry(existingEntity);
        //                var createdBy = existingEntity.CreatedBy;
        //                var createdDate = existingEntity.CreatedDate;

        //                mergeEntry.CurrentValues.SetValues(modifiedEntity);
        //                var mergeEntity = ((TPersistenceModel)mergeEntry.Entity);
        //                mergeEntity.CreatedBy = createdBy;
        //                mergeEntity.CreatedDate = createdDate;
        //                ((IObjectState)mergeEntry.Entity).ObjectState = ObjectState.Modified;
        //            }
        //            else
        //            {
        //                Insert(modifiedEntity);
        //                //DbSet.Attach(modifiedEntity);
        //                //SetModifiedState(modifiedEntity, entry);
        //            }
        //        }
        //    }
        //}
        private void SetModifiedEntityState(TPersistenceModel modifiedEntity)
        {
            var entry = Context.Entry(modifiedEntity);
            //try
            //{
            //    SetModifiedState(modifiedEntity, entry);
            //}
            //catch (InvalidOperationException)
            //{
            string propertyType;
            var keyValue = GetPrimaryKeyValue(entry, out propertyType);

            if (entry.State != EntityState.Detached)
                return;

            var existingEntity = DbSet.Find(keyValue);
            if (existingEntity != null)
            {
                var mergeEntry = Context.Entry(existingEntity);
                var createdBy = existingEntity.CreatedBy;
                var createdDate = existingEntity.CreatedDate;
                mergeEntry.CurrentValues.SetValues(modifiedEntity);
                var mergeEntity = ((TPersistenceModel)mergeEntry.Entity);
                mergeEntity.CreatedBy = createdBy;
                mergeEntity.CreatedDate = createdDate;
                ((IObjectState)mergeEntry.Entity).ObjectState = ObjectState.Modified;
            }
            else
            {
                Insert(modifiedEntity);
                //DbSet.Attach(modifiedEntity);
                //SetModifiedState(modifiedEntity, entry);
            }
            //}
        }
    }
}
