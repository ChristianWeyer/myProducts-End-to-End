
namespace MyProducts.DataAccess
{
    public class Article : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
