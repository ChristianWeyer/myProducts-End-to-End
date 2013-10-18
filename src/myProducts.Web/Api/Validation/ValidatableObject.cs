using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace MasterDetail.Web.Api.Validation
{
    public class ValidatableObject<TObject> : IValidatableObject where TObject : IValidator, new()
    {
        private readonly IValidator validator;

        public ValidatableObject()
        {
            validator = new TObject();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return validator.Validate(this).ToValidationResult();
        }
    }
}