using FluentValidation;

namespace MasterDetail.Web.Api.DTOs
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
