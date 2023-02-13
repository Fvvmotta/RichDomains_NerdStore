using NerdStore.Catalog.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Validate_ValidationMustReturnExceptions()
        {

            // Arrange & Act & Assert

            var ex = Assert.Throws<DomainException>(() =>
                new Product(string.Empty, "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Name of the product can not be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", string.Empty, false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Description of the product can not be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 0, Guid.NewGuid(), DateTime.Now, "Omage", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Value of the product can not be less than or equals to 0", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.Empty, DateTime.Now, "Image", new Dimensions(1, 1, 1))
            );

            Assert.Equal("Category Id of the product can not be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, string.Empty, new Dimensions(1, 1, 1))
            );

            Assert.Equal("Image field of the product can not be empty", ex.Message);

            ex = Assert.Throws<DomainException>(() =>
                new Product("Name", "Description", false, 100, Guid.NewGuid(), DateTime.Now, "Image", new Dimensions(0, 1, 1))
            );

            Assert.Equal("Height field of the product can not be less than or equals to 1", ex.Message);
        }
    }
}
