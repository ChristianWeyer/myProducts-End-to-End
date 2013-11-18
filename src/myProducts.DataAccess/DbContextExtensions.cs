using System;
using System.Data.Entity;
using System.Linq;

namespace MyProducts.DataAccess
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
    }
}