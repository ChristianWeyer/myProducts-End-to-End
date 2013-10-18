using System;
using MyProducts.Web.Api.Validation;

namespace MyProducts.Web.Api.DTOs
{
    public class ArticleDetailUpdateDto : ValidatableObject<ArticleDetailUpdateDtoValidator>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
