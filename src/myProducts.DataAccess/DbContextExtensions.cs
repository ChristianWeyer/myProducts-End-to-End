using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace MyProducts.Model
{
    public static class DbContextExtensions
    {
        public static void SetDeleted<TEntity>(this DbContext db, Guid id) where TEntity : EntityBase, new()
        {
            db.Entry(new TEntity { Id = id }).State = EntityState.Deleted;
        }

        public static void SetEntityState<TEntity>(this DbContext db, TEntity entity, params string[] notToBeModified) where TEntity : EntityBase
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                db.Entry(entity).State = EntityState.Added;
            }
            else
            {
                db.Entry(entity).State = EntityState.Modified;
                notToBeModified.ToList().ForEach(e => db.Entry(entity).Property(e).IsModified = false);
            }
        }

        public static void AttachByIdValue<TEntity>(this DbContext context, TEntity rootEntity, HashSet<Type> childTypes)
            where TEntity : EntityBase
        {
            context.Set<TEntity>().Add(rootEntity);

            if (rootEntity.Id != Guid.Empty)
            {
                context.Entry(rootEntity).State = EntityState.Modified;
            }

            foreach (var entry in context.ChangeTracker.Entries<EntityBase>())
            {
                if (entry.State == EntityState.Added && entry.Entity != rootEntity)
                {
                    if (childTypes == null || childTypes.Count == 0)
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    else
                    {
                        var entityType = ObjectContext.GetObjectType(entry.Entity.GetType());

                        if (!childTypes.Contains(entityType))
                        {
                            entry.State = EntityState.Unchanged;
                        }
                        else if (entry.Entity.Id != Guid.Empty)
                        {
                            entry.State = EntityState.Modified;
                        }
                    }
                }
            }
        }
    }
}