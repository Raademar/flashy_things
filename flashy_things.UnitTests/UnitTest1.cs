using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using flashy_things.Repositories;
using flashy_things.Models;
using flashy_things.Services;
using FakeItEasy;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private ICartRepository cartRepository;
        private CartService cartService;
        private IProductRepository productRepository;
        private ProductService productService;

        [SetUp]
        public void SetUp()
        {
            this.cartRepository = A.Fake<ICartRepository>();
            this.cartService = new CartService(this.cartRepository);
            this.productRepository = A.Fake<IProductRepository>();
            this.productService = new ProductService(this.productRepository);
        }
        
        [Test]
        public void Get_GivenId_ReturnsResultFromRepository()
        {
            // Arrange
            var cart = new Cart
            {
                CartId = 99,
            };

            A.CallTo(() => this.cartRepository.Get(cart.CartId)).Returns(cart);

            // Act
            var result = this.cartService.Get(cart.CartId);
            
            // Assert
            Assert.That(result, Is.EqualTo(cart));
        }
        
    }
}