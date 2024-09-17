using Microsoft.EntityFrameworkCore;
using Moq;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Resources;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels.Attributes;
using System.Globalization;
using Xunit.Sdk;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>

        #region name
        [Theory]
        [InlineData("", "MissingName")]
        [InlineData(" ", "MissingName")]
        [InlineData(null, "MissingName")]
        public void TestInvalidName_ShouldReturnMissingName(string inputName, string expectedResult)
        {
            // Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = inputName, // using the invalid name from InlineData
                Description = "description",
                Stock = "10",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingName" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedResult);
        }
        #endregion

        #region stock
        [Theory]
        [InlineData("", "MissingStock")]
        [InlineData(" ", "MissingStock")]
        [InlineData(null, "MissingStock")]
        public void TestStockEmpty_ShouldReturnMissingStock(string inputStock, string expectedResult)
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = inputStock,
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingStock" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == expectedResult);
        }
        [Theory]
        [InlineData("ten")]
        [InlineData("10.5")]
        public void TestStockStringCharacterAndDoubleValue_ShouldReturnStockNotAnInteger(string invalidStock)
        {
            //Arrange localizer
            ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);
            //Arrange product
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = invalidStock,
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotAnInteger" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == ResourceManager.GetString("StockNotAnInteger", CultureInfo.CurrentUICulture));
        }
        [Theory]
        [InlineData("-10")]
        [InlineData("0")]
        public void TestStockNegativeAndZero_ShouldReturnStockNotGreaterThanZero(string invalidStock)
        {
            //Arrange localizer
            ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);
            //Arrange product
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = invalidStock,
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == ResourceManager.GetString("StockNotGreaterThanZero", CultureInfo.CurrentUICulture));
        }
        #endregion

        #region price
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestPriceEmpty_ShouldReturnMissingPrice(string invalidPrice)
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = invalidPrice
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingPrice" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingPrice");
        }
        [Fact]
        public void TestPriceStringCharacter_ShouldReturnPriceNotANmuber()
        {
            //Arrange localizer
            ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);
            //Arrange product
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = "twenty"//price not a number
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "PriceNotANumber" error
            Assert.Contains(validationResults,
                vr => vr.ErrorMessage == ResourceManager.GetString("PriceNotANumber", CultureInfo.CurrentUICulture));
        }
        [Theory]
        [InlineData("-20")]
        [InlineData("0")]
        public void TestPriceNegativeAndZero_ShouldReturnPriceNotGreaterThanZero(string invalidPrice)
        {
            //Arrange localizer
            ResourceManager ResourceManager =
            new ResourceManager("P3AddNewFunctionalityDotNetCore.Resources.Models.ViewModels.ProductViewModel",
                                typeof(StockValidationAttribute).Assembly);
            //Arrange product
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = invalidPrice
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "PriceNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == ResourceManager.GetString("PriceNotGreaterThanZero", CultureInfo.CurrentUICulture));
        }
        #endregion


        [Fact]
        public void TestValidProduct_ShouldReturnNoError()
        {
            // Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "Test Product",
                Description = "description",
                Stock = "10",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation success
            Assert.True(Validator.TryValidateObject(productViewModel, validationContext, validationResults, true));
        }

        [Fact]
        public void AddProduct_ShouldCallSaveProductFromRepository()
        {
            // Arrange
            //Mok for ProductService
            var mockCart = new Mock<ICart>();
            var mockProductRepository = new Mock<IProductRepository>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object);

            var newProduct = new ProductViewModel
            {
                Id = 850,
                Name = "Test Product",
                Description = "description",
                Stock = "10",
                Price = "20"
            };

            // Act
            productService.SaveProduct(newProduct);

            // Assert
            mockProductRepository.Verify(repo => repo.SaveProduct(It.IsAny<Product>()), Times.Once);
        }

        //Tester ce qu'il se passe si un produit est supprimé de la DB pendant qu'il est dans un panier d'un client
        [Fact]
        public void RemoveProductWhileInCart_ShouldCallDeleteProductFromRepositoryAndRemoveLineFromCart()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockCart = new Mock<ICart>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object);
            
            //Product to add and remove from repository
            var productToRemove = new Product
            {
                Id = 850,
                Name = "Test Product",
                Description = "description",
                Quantity = 10,
                Price = 20
            };
            IEnumerable<Product> theProducts = new List<Product> { productToRemove };
            mockProductRepository.Setup(repository => repository.GetAllProducts()).Returns(theProducts);

            // Act
            productService.DeleteProduct(850);

            // Assert
            mockCart.Verify(repo => repo.RemoveLine(productService.GetProductById(850)), Times.Once);
            mockProductRepository.Verify(repo => repo.DeleteProduct(850), Times.Once);
        }



        [Fact]
        public void RemoveInexistedProduct_ShouldNoCallDeleteProductFromRepository()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockCart = new Mock<ICart>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object);

            // Act
            productService.DeleteProduct(850);

            // Assert
            mockProductRepository.Verify(repo => repo.DeleteProduct(850), Times.Never);
        }

        /*
        [Fact]
        public void ExampleMethod()
        {
            // Arrange

            // Act


            // Assert
            Assert.Equal(1, 1);
        }/**/
    }
}