using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
using System.Resources;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes
{
    public class PriceValidationAttribute : ValidationAttribute
    {

        //Setup for localize error message
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);

        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !Double.TryParse(value.ToString(), out double price))
            {
                var localizedErrorMessage = ResourceManager.GetString(ErrorMessage, CultureInfo.CurrentUICulture);
                return new ValidationResult(localizedErrorMessage ?? ErrorMessage);
                //if the price is null or not a double, return ErrorMessage : "PriceNotANumber"
            }
            if (price <= 0)
            {
                var localizedErrorMessage = ResourceManager.GetString(GreaterThanZeroErrorMessage, CultureInfo.CurrentUICulture);
                return new ValidationResult(localizedErrorMessage ?? GreaterThanZeroErrorMessage);
                // if the price is a double but nor greater than zero, return GreaterThanZeroErrorMessage
            }
            return ValidationResult.Success;
        }
    }
}
