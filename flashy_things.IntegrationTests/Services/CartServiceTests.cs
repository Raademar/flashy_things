using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using flashy_things.Models;
using flashy_things.Repositories;
using flashy_things.Services;
using NUnit.Framework;

namespace Tests.Services
{
    public class CartServiceTests
    {
        private CartService cartService;
        [SetUp]
        public void SetUp()
        {
            this.cartService = new CartService(new CartRepository(
                "Server=localhost;Port=8889;Database=flashy_things;Uid=admin;Pwd=admin;"));
        }

        [Test]
        public void Get_GivenId_ReturnsResultsFromDatabase()
        {
            var response = cartService.Get(6);
            Assert.That(response.CartId, Is.EqualTo(6));
            Assert.That(response.Products.Count(), Is.EqualTo(1));
            Assert.That(response.CartCompleted, !Is.Null);
        }

        [Test]
        public void Add_GivenValidCart_SavesToOrder()
        {
            const int ExpectedCartId = 99;
            const string ExpectedName = "Hasse-Klasse";
            const string ExpectedStreet = "Muppgatan 2";
            const string ExpectedCity = "Klassetown";
            const string ExpectedZipCode = "123 45";
            const string ExpectedTelephone = "07070707087";
            const string ExpectedEmail = "hasseklasseboy@klasses.se";
            var order = new Order
            {
                CartId = ExpectedCartId,
                Name = ExpectedName,
                Street = ExpectedStreet,
                City = ExpectedCity,
                ZipCode = ExpectedZipCode,
                Telephone = ExpectedTelephone,
                Email = ExpectedEmail
            };
            Cart orderAdded;
            using (new TransactionScope())
            {
                this.cartService.SubmitOrder(order);
                orderAdded = this.cartService.Get(99);
            }
            
            Assert.That(orderAdded.CartId, Is.EqualTo(ExpectedCartId));
        }
    }
}