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
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestInvalidName_ShouldReturnMissingName(string invalidName)
        {
            // Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = invalidName, // using the invalid name from InlineData
                Description = "description",
                Stock = "10",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingName" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingName");
        }
        #endregion

        #region stock
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void TestStockEmpty_ShouldReturnMissingStock(string invalidStock)
        {
            //Arrange
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

            // Assert: Check if validation failed and contains the "MissingStock" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingStock");
        }
        [Theory]
        [InlineData("ten")]
        [InlineData("10.5")]
        public void TestStockStringCharacterAndDoubleValue_ShouldReturnStockNotAnInteger(string invalidStock)
        {
            //Arrange
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
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotAnInteger");
        }
        [Theory]
        [InlineData("-10")]
        [InlineData("0")]
        public void TestStockNegativeAndZero_ShouldReturnStockNotGreaterThanZero(string invalidStock)
        {
            //Arrange
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
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotGreaterThanZero");
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
        public void TestPriceStringCharacter()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = "twenty"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "PriceNotANumber" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "PriceNotANumber");
        }
        [Theory]
        [InlineData("-20")]
        [InlineData("0")]
        public void TestPriceNegativeAndZero_ShouldReturnPriceNotGreaterThanZero(string invalidPrice)
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

            // Assert: Check if validation failed and contains the "PriceNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "PriceNotGreaterThanZero");
        }
        #endregion

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

        [Fact]
        public void RemoveProduct_ShouldCallDeleteProductFromRepository()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockCart = new Mock<ICart>();

            var productService = new ProductService(mockCart.Object, mockProductRepository.Object);
            /*
            //Product to add and remove from repository
            var productToRemove = new Product
            {
                Id = 850,
                Name = "Test Product",
                Description = "description",
                Quantity = 10,
                Price = 20
            };
            mockProductRepository.Setup(repo => repo.GetProduct(850)).ReturnsAsync(productToRemove);/**/

            // Act
            productService.DeleteProduct(850);

            // Assert
            mockProductRepository.Verify(repo => repo.DeleteProduct(850), Times.Once);
        }

        //Tester ce qu'il se passe si un produit est supprimé de la DB pendant qu'il est dans un panier d'un client



        [Fact]
        public void ExampleMethod()
        {
            // Arrange

            // Act


            // Assert
            Assert.Equal(1, 1);
        }
    }
}