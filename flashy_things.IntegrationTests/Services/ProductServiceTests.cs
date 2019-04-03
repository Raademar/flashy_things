using flashy_things.Repositories;
using flashy_things.Services;
using flashy_things.Models;
using NUnit.Framework;

namespace Tests.Services
{
    public class ProductServiceTests
    {
        private ProductService productService;
        [SetUp]
        public void SetUp()
        {
            this.productService = new ProductService(new ProductRepository(
                "Server=localhost;Port=8889;Database=flashy_things;Uid=admin;Pwd=admin;"));
        }

        [Test]
        public void Get_GivenId_ReturnsResultFromDatabase()
        {
            var response = this.productService.Get(1);
            
            Assert.That(response.Id, Is.EqualTo(1));
            Assert.That(response.Title, Is.EqualTo("Nike Football Shoe Red"));
            Assert.That(response.Description, Is.EqualTo("Coola nikeskor"));
            Assert.That(response.Image, !Is.Null);
            Assert.That(response.Price, Is.GreaterThan(0));
        }
    }
}