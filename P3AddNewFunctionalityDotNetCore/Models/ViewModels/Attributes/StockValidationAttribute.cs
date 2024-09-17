using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes
{
    public class StockValidationAttribute : ValidationAttribute
    {
        //Setup for localize error message
        private static readonly ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);

        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int stock))
            {
                var localizedErrorMessage = ResourceManager.GetString(ErrorMessage, CultureInfo.CurrentUICulture);
                return new ValidationResult(localizedErrorMessage ?? ErrorMessage);
                //if the stock is null or not a int, return ErrorMessage : "StockNotAInteger"
            }
            if (stock <= 0)
            {
                var localizedErrorMessage = ResourceManager.GetString(GreaterThanZeroErrorMessage, CultureInfo.CurrentUICulture);
                return new ValidationResult(localizedErrorMessage ?? GreaterThanZeroErrorMessage);
                // if the stock is a int but nor greater than zero, return GreaterThanZeroErrorMessage
            }
            return ValidationResult.Success;
        }
    }
}
