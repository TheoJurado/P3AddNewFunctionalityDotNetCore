using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes
{
    public class StockValidationAttribute : ValidationAttribute
    {
        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int stock))
            {
                return new ValidationResult(ErrorMessage); //if the stock is null or not a int, return ErrorMessage : "StockNotAInteger"
            }
            if (stock <= 0)
            {
                return new ValidationResult(GreaterThanZeroErrorMessage); // if the stock is a int but nor greater than zero, return GreaterThanZeroErrorMessage
            }
            return ValidationResult.Success;
        }
    }
}
