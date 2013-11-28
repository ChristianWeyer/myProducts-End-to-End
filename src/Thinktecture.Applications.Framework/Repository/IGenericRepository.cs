using System;
using System.Linq;
using System.Linq.Expressions;

namespace Thinktecture.Applications.Framework.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);        
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity InsertOrUpdateGraph(TEntity entityGraph);
        void Delete(TEntity entity);
    }
}
