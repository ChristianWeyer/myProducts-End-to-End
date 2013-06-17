using System.Collections.Generic;
using System.Linq;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace MasterDetail.Web.Api
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
