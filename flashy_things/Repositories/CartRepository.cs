using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using flashy_things.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace flashy_things.Repositories
{
    public class CartRepository
    {
        private readonly string ConnectionString;
        private readonly CartRepository cartRepository;
        public List<CartItem> ShoppingCart;


        public CartRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public void AddToCart(CartItem product)
        {
            ShoppingCart.Add(product);
        }
    }
}