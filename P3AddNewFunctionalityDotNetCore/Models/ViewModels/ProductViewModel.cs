using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "MissingStock")]
        [StockValidation(ErrorMessage = "StockNotAnInteger", GreaterThanZeroErrorMessage = "StockNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [PriceValidation(ErrorMessage = "PriceNotANumber", GreaterThanZeroErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }

    public class PriceValidationAttribute : ValidationAttribute
    {
        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !Double.TryParse(value.ToString(), out double price))
            {
                return new ValidationResult(ErrorMessage);
            }
            if (price <= 0)
            {
                return new ValidationResult(GreaterThanZeroErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class StockValidationAttribute : ValidationAttribute
    {
        public string GreaterThanZeroErrorMessage { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !int.TryParse(value.ToString(), out int stock))
            {
                return new ValidationResult(ErrorMessage);
            }
            if (stock <= 0)
            {
                return new ValidationResult(GreaterThanZeroErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
