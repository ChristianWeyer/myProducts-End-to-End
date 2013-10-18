using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyProducts.Web.Api.Validation
{
    public static class FluentValidationExtensions
    {
        public static IEnumerable<ValidationResult> ToValidationResult(
            this FluentValidation.Results.ValidationResult validationResult)
        {
            var results = validationResult.Errors.Select(
                item => new ValidationResult(item.ErrorMessage, new List<string> { item.PropertyName }));

            return results;
        }
    }
}
