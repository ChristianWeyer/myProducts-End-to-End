using System;

namespace MyProducts.Model
{
    public class Article : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
