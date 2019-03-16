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
                return connection.Query<CartItem>("SELECT CartItem.CartItemId, CartItem.CartId, CartItem.ProductId, Products.title, Products.image " +
                                                  "FROM CartItem " +
                                                  "LEFT JOIN Products ON Products.id = CartItem.ProductId" +
                                                  "WHERE CartItem.CartId = 1;").ToList(); 
            }
        }
    }
}