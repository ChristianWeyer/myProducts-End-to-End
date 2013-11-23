using System.Collections.Generic;

namespace MyProducts.Model
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Article> Articles { get; set; }
    }
}