using Domain.Models;
using FluentAssertions;
using Xunit;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.Mappings;

namespace Application.Services.Tests
{
  public class ClientRegistrationServiceTests : BaseTests
  {
    protected IClientAnalyticsService CreateProductService( ConnectionType connectionType = ConnectionType.EntityFramework )
    {
      IRepository<Product>? productRepository = new InMemoryRepository<Product>();
      IRepository<Category>? categoryRepository = new InMemoryRepository<Category>();

      // Update the mapper initialization to use the correct constructor for MapperConfiguration
      IMapper mapper = new MapperConfiguration( cfg =>
      {
        cfg.AddProfile( new MappingProfile() ); // Use an instance of MappingProfile
      }, new LoggerFactory() ).CreateMapper(); // Create a mapper instance


      if(connectionType == ConnectionType.EntityFramework)
      {
        var options = new DbContextOptionsBuilder<ClientRegisterDBContext>()
      .UseInMemoryDatabase( databaseName )
      .Options;

        productRepository = new EfRepository<Product>( options );
        categoryRepository = new EfRepository<Category>( options );
      }

      return new ClientRegistrationService( productRepository, categoryRepository, mapper, null, null );
    }

    protected IClientCaptureService CreateCategoryService( ConnectionType connectionType = ConnectionType.EntityFramework )
    {
      IRepository<Category>? categoryRepository = new InMemoryRepository<Category>();

      // Update the mapper initialization to use the correct constructor for MapperConfiguration
      IMapper mapper = new MapperConfiguration( cfg =>
      {
        cfg.AddProfile( new MappingProfile() ); // Use an instance of MappingProfile
      }, new LoggerFactory() ).CreateMapper(); // Create a mapper instance


      if(connectionType == ConnectionType.EntityFramework)
      {
        var options = new DbContextOptionsBuilder<ClientRegisterDBContext>()
      .UseInMemoryDatabase( databaseName )
      .Options;

        categoryRepository = new EfRepository<Category>( options );
      }

      return new ProductCategoryService( categoryRepository, mapper );
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    [InlineData( ConnectionType.InMemoryDb )]
    public async Task InsertProduct_ShouldSaveToDatabase( ConnectionType connectionType )
    {
      using var productService = CreateProductService( connectionType );

      // Arrange
      var product = new ProductDto
      (
        Id: "1",
        Name: "Gaming Mouse",
        SKU: "MOU-99",
        Price: 50.00m
      );

      // Act
      await productService.CreateProductAsync( product );

      // Assert
      var savedProduct = await productService.GetProductByIdAsync( "1" );
      savedProduct.Should().NotBeNull();
      savedProduct.Name.Should().Be( "Gaming Mouse" );
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    [InlineData( ConnectionType.InMemoryDb )]
    public async Task UpdateProductField_ShouldUpdateOnlyPrice_WhenPriceIsChanged( ConnectionType connectionType )
    {
      // Arrange
      using var productService = CreateProductService( connectionType );

      var product = new ProductDto
      (
        Id: "1",
        Name: "Original Laptop",
        SKU: "LAP-100",
        Price: 1000m,
        CategoryId: "1"
      );

      await productService.CreateProductAsync( product );

      // Act
      const decimal newPrice = 1200m;

      var productRePriced = new ProductDto
        (
           Id: "1",
           Name: "Original Laptop",
           SKU: "LAP-100",
           Price: newPrice,
           CategoryId: "1"
        );

      await productService.UpdateProductAsync( productRePriced );

      // Assert
      var updatedProduct = await productService.GetProductByIdAsync( "1" );

      updatedProduct.Price.Should().Be( newPrice ); // Price should change
      updatedProduct.Name.Should().Be( "Original Laptop" ); // Name must stay the same
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    [InlineData( ConnectionType.InMemoryDb )]
    public async Task DeleteProduct_ShouldRemoveProductFromDatabase( ConnectionType connectionType )
    {
      // --- ARRANGE ---
      using var productService = CreateProductService( connectionType );

      // Create a product to delete
      var product = new ProductDto
      (
        Id: "1",
        Name: "Test Laptop",
        Price: 999.99m
      );

      await productService.CreateProductAsync( product );

      var beforeDeleteProduct = await productService.GetProductByIdAsync( "1" );

      // --- ACT ---
      await productService.DeleteProductAsync( product );

      // --- ASSERT ---
      // Try to retrieve the product to see if it still exists
      var deletedProduct = await productService.GetProductByIdAsync( "1" );

      Assert.True( beforeDeleteProduct != null && deletedProduct == null, "The product should have been deleted from the database." );
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    public async Task GetProductsByCategory_ShouldReturnDirectMatches( ConnectionType connectionType )
    {
      // Act: Filter by "Laptops" (ID: 2)
      using var productService = CreateProductService( connectionType );
      using var categoryService = CreateCategoryService( connectionType );
      await CreateCategoryFilterTestData( productService, categoryService );

      var result = await productService.GetProductsByCategory( "2" );

      // Assert
      result.Should().HaveCount( 2 );
      result.Should().Contain( p => p.Name == "MacBook" );
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    public async Task GetProductsByCategory_ShouldReturnChildProducts_WhenParentIdSelected( ConnectionType connectionType )
    {
      // Act: Filter by "Electronics" (ID: 1) which has no direct products, 
      // but has "Laptops" as a child.
      using var productService = CreateProductService( connectionType );
      using var categoryService = CreateCategoryService( connectionType );
      await CreateCategoryFilterTestData( productService, categoryService );

      var result = await productService.GetProductsByCategory( "1" );

      // Assert
      result.Should().HaveCount( 2 ); // Should find the 2 laptops
      result.Should().AllSatisfy( p => p.CategoryId.Should().Be( "2" ) );
    }

    [Theory]
    [InlineData( ConnectionType.EntityFramework )]
    public async Task GetProductsByCategory_ShouldReturnEmpty_WhenCategoryDoesNotExist( ConnectionType connectionType )
    {
      using var productService = CreateProductService( connectionType );
      using var categoryService = CreateCategoryService( connectionType );

      await CreateCategoryFilterTestData( productService, categoryService );
      // Act
      var result = await productService.GetProductsByCategory( "999" );

      // Assert
      result.Should().BeEmpty();
    }

    private async Task CreateCategoryFilterTestData( IClientAnalyticsService productService, IClientCaptureService categoryService )
    {
      var categories = new List<CategoryDto>
      {
        new( "1", "Electronics", "ELE123", true ),
        new( "2", "Laptops", "LAP123", true, "1" ),
        new( "3", "Groceries", "GRO123", true )
      };

      var products = new List<ProductDto>
      {
        new( "P1", "MacBook", 1200, "SKU001", "Laptop", "2" ),
        new( "P2", "Apple", 1, "SKU002", "Fruit", "3" ),
        new( "P3", "ThinkPad", 1000, "SKU003", "Laptop", "2" )
      };

      foreach(var product in products)
      {
        await productService.CreateProductAsync( product );
      }

      foreach(var category in categories)
      {
        await categoryService.CreateCategoryAsync( category );
      }
    }
  }
}
