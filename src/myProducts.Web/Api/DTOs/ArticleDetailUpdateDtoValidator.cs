using FluentValidation;

namespace MyProducts.Web.Api.DTOs
{
    public class ArticleDetailUpdateDtoValidator : AbstractValidator<ArticleDetailUpdateDto>
    {
        public ArticleDetailUpdateDtoValidator()
        {
            RuleFor(item => 
                item.Name).NotEmpty();
        }
    }
}
