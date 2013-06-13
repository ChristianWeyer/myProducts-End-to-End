using System.Data.Entity;

namespace MasterDetail.DataAccess
{
    public class ProductsContext : DbContext
    {
        public ProductsContext()
            : base("name=ProductsContext")
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public DbSet<Article> Articles { get; set; }
    }
}
