
using System.ComponentModel.DataAnnotations;
namespace MyProducts.Services.DTOs
{
    public class ArticleDetailUpdateDto : DtoBase
    {
        [MinLength(5)]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public CategoryDto Category { get; set; }
    }
}
