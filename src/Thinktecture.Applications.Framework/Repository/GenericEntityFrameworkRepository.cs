using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Thinktecture.Applications.Framework.Entities;

namespace Thinktecture.Applications.Framework.Repository
{
    public abstract class GenericEntityFrameworkRepository<TDbContext, TEntity> :
        IGenericRepository<TEntity>
        where TEntity : class, IDataWithState
        where TDbContext : DbContext, new()
    {
        private TDbContext entities = new TDbContext();

        protected TDbContext Context
        {
            get { return entities; }
            set { entities = value; }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = entities.Set<TEntity>().AsNoTracking();

            return query;
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = entities.Set<TEntity>().AsNoTracking();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = entities.Set<TEntity>().AsNoTracking().Where(predicate);

            return query;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            entities.Set<TEntity>().Add(entity);
            Save();

            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            entities.Entry(entity).State = EntityState.Modified;
            Save();

            return entity;
        }

        public TEntity InsertOrUpdateGraph(TEntity entityGraph)
        {
            if (entityGraph.State == DataState.Added)
            {
                entities.Set<TEntity>().Add(entityGraph);
            }
            else
            {
                entities.Set<TEntity>().Add(entityGraph);
                entities.ApplyStateChanges();
            }

            Save();
            entities.ResetState();
            
            return entityGraph;
        }

        public virtual void Delete(TEntity entity)
        {
            entities.Entry<TEntity>(entity).State = EntityState.Deleted;
            Save();
        }

        #region Async - untested YET
       
        public async virtual Task<TEntity> InsertAsync(TEntity entity)
        {
            entities.Set<TEntity>().Add(entity);
            await SaveAsync();

            return entity;
        }
        public async virtual void DeleteAsync(TEntity entity)
        {
            entities.Entry<TEntity>(entity).State = EntityState.Deleted;
            await SaveAsync();
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            entities.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return entity;
        }

        private async Task SaveAsync()
        {
            await entities.SaveChangesAsync();
        }
        
        #endregion

        private void Save()
        {
            entities.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
