
using System.Collections.Generic;
namespace MyProducts.Services.DTOs
{
    public class ArticleDetailUpdateDto : DtoBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
