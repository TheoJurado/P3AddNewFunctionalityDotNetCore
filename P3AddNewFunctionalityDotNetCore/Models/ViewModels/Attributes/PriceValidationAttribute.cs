using System.ComponentModel.DataAnnotations;
using System;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes
{
    public class PriceValidationAttribute : ValidationAttribute
    {
        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !Double.TryParse(value.ToString(), out double price))
            {
                return new ValidationResult(ErrorMessage);//if the price is null or not a double, return ErrorMessage : "PriceNotANumber"
            }
            if (price <= 0)
            {
                return new ValidationResult(GreaterThanZeroErrorMessage); // if the price is a double but nor greater than zero, return GreaterThanZeroErrorMessage
            }
            return ValidationResult.Success;
        }
    }
}
