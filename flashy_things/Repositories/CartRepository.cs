using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using flashy_things.Models;
using MySql.Data.MySqlClient;

namespace flashy_things.Repositories
{
    public class CartRepository
    {
        private readonly string ConnectionString;
        private readonly CartRepository cartRepository;

        public CartRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public List<CartItem> Get()
        { 
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                return connection.Query<CartItem>("SELECT cartitem.CartItemId, cartitem.CartId, cartitem.ProductId, product.title, product.image, product.price FROM cartitem LEFT JOIN product ON product.Id = cartItem.ProductId WHERE cartitem.CartId = 1").ToList(); 
            }
        }
        
        public bool SubmitOrder(Cart cart)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var result = connection.Execute("INSERT INTO order (CartId, CustomerId) VALUES (@CartId, @CustomerId;)", cart);

                if (result == 0)
                {
                    return false;
                }

                return true;
            }
            
        }
    }
}