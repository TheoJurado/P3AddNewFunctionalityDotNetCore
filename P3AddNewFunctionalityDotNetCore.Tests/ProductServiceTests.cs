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
        [Fact]
        public void TestNameEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "", //empty name to trigger validation error
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

        [Fact]
        public void TestSpaceNameEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = " ", //name only a space to trigger validation error
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

        [Fact]
        public void TestNullName()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = null, //null name to trigger validation error
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
        [Fact]
        public void TestStockEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingStock" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingStock");
        }
        [Fact]
        public void TestStockSapceEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = " ",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingStock" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingStock");
        }
        [Fact]
        public void TestStockStringCharacter()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "ten",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotAnInteger" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotAnInteger");
        }
        [Fact]
        public void TestStockDoubleAndNotInt()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10.5",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotAnInteger" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotAnInteger");
        }
        [Fact]
        public void TestStockNegative()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "-10",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotGreaterThanZero");
        }
        [Fact]
        public void TestStockEqualZero()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "0",
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "StockNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "StockNotGreaterThanZero");
        }
        [Fact]
        public void TestStockNull()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = null,
                Price = "20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingStock" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingStock");
        }
        #endregion

        #region price
        [Fact]
        public void TestPriceEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = ""
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingPrice" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingPrice");
        }
        [Fact]
        public void TestPriceSpaceEmpty()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = " "
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
        [Fact]
        public void TestPriceNegative()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = "-20"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "PriceNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "PriceNotGreaterThanZero");
        }
        [Fact]
        public void TestPriceZero()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = "0"
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "PriceNotGreaterThanZero" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "PriceNotGreaterThanZero");
        }
        [Fact]
        public void TestPriceNull()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel
            {
                Id = 850,
                Name = "testName",
                Description = "description",
                Stock = "10",
                Price = null
            };

            // Act: Manually validate the model
            var validationContext = new ValidationContext(productViewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(productViewModel, validationContext, validationResults, true);

            // Assert: Check if validation failed and contains the "MissingPrice" error
            Assert.Contains(validationResults, vr => vr.ErrorMessage == "MissingPrice");
        }
        #endregion

        private DbContextOptions<P3Referential> CreateInMemoryDatabaseOptions()
        {
            return new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }
        
        [Fact]
        public async Task AddProduct_ShouldAddProductToDatabase()
        {
            // Arrange
            var options = CreateInMemoryDatabaseOptions();
            using var context = new P3Referential(options);
            var productRepository = new ProductRepository(context);
            //Mok for ProductService
            var mockCart = new Mock<ICart>();
            var productService = new ProductService(mockCart.Object, productRepository);

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
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(1, await context.Product.CountAsync());
            Assert.Equal("Test Product", (await context.Product.FirstAsync()).Name);
        }

        //Tester si la suppression de produit a bien un impact sur base de donnée
        [Fact]
        public async Task DeleteProduct_ShouldRemoveProductFromDatabase()
        {
            // Arrange
            var options = CreateInMemoryDatabaseOptions();
            using var context = new P3Referential(options);
            var productRepository = new ProductRepository(context);
            // Mok for ProductService
            var mockCart = new Mock<ICart>();
            var productService = new ProductService(mockCart.Object, productRepository);

            // Add a product to be able to delete it
            var productToAdd = new Product
            {
                Id = 851,
                Name = "Product To Delete",
                Description = "description",
                Quantity = 10,
                Price = 20
            };
            context.Product.Add(productToAdd);
            await context.SaveChangesAsync();

            // Act
            productService.DeleteProduct(productToAdd.Id);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(0, await context.Product.CountAsync()); // Make sure there are no more products
            Assert.False(await context.Product.AnyAsync(p => p.Id == productToAdd.Id)); // Check that the product is deleted correctly
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