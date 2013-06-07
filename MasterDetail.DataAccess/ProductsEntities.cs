using System.Data.Entity;

namespace MasterDetail.DataAccess
{
    public class ProductsEntities : DbContext
    {
        public ProductsEntities()
            : base("name=ProductsEntities")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Article> Articles { get; set; }
    }
}
