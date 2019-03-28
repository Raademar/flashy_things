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

        public Cart Get(int id)
        { 
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var cart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM cart WHERE CartId = @id", new { id });
                if (cart == null)
                {
                    connection.Execute("INSERT INTO cart (CartId) VALUES (@id)",new {id });
                    var newCart = connection.QuerySingleOrDefault<Cart>("SELECT * FROM cart WHERE CartId = @id", new { id });
                    return newCart;
                }
                cart.Products = connection
                    .Query<Product>(
                        "SELECT * FROM cartitem ci INNER JOIN product p ON ci.ProductId = p.Id WHERE ci.CartId = @id",
                        new {id}).ToList();
                cart.Price = cart.Products.Sum(item => item.Price);
                return cart;
            }
        }
        
        public bool SubmitOrder(Order order)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var result = connection.Execute(  
                "INSERT INTO submittedorders (CartId, Name, Street, City, ZipCode, Telephone, Email) VALUES (@CartId, @Name, @Street, @City, @ZipCode, @Telephone, @Email)", order);
                
                var resultOfUpdateCartCompletion = connection.Execute("UPDATE cart SET CartCompleted = 1 WHERE CartId = @CartId", new { order.CartId });
                
                if (result == 0 || resultOfUpdateCartCompletion == 0)
                {
                    return false;
                }
                
                return true;
            }
            
        }
    }
}