using System.Collections.Generic;
using Thinktecture.Applications.Framework.Entities;

namespace MyProducts.Model
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Article> Articles { get; set; }
    }
}