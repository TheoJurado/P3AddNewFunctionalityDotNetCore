using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes;

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

        [Required(ErrorMessage = "MissingStock")]//"MissingStock"
        [StockValidation(ErrorMessage = "StockNotAnInteger", GreaterThanZeroErrorMessage = "StockNotGreaterThanZero")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [PriceValidation(ErrorMessage = "PriceNotANumber", GreaterThanZeroErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }
    }
}
