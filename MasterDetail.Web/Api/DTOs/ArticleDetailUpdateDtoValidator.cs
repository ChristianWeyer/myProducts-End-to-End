using FluentValidation;

namespace MasterDetail.Web.Api
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
