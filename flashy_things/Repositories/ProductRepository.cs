using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using flashy_things.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace flashy_things.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string ConnectionString;
        private readonly ProductRepository productRepository;


        public ProductRepository(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public List<Product> Get()
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                return connection.Query<Product>("SELECT * FROM product;").ToList(); 
            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM product WHERE Id = @id;", new { id });
            }
        }

        public bool Add(Product product)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                if(product == null)
                {
                    if (product.@Title == "" ||
                        product.@Description == "" ||
                        product.@Image == "" ||
                        product.@Category == 0)
                    return false;
                }
                
                connection.Execute("INSERT INTO product (Title, Description, Image, Category, Stock, DateAdded) Values (@Title, @Description, @Image, @Category, @Stock, @DateAdded)", product);
                return true;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var result = connection.Execute("DELETE FROM product WHERE Id = @id", new { id });
                
                if (result == 0)
                {
                    return false;
                }

                return true;
            }
        }
        
        public bool AddToCart(CartItem cartItem, int ProductId)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var doesCartExist = connection
                    .QuerySingleOrDefault("SELECT * FROM cart WHERE CartId = @CartId", new {cartItem.CartId});

                if (doesCartExist == null)
                {
                    connection.Execute("INSERT INTO cart (CartId) VALUES (@CartId)", new { cartItem.CartId });
                }
                var response = connection.Execute("INSERT INTO cartitem (CartId, ProductId) VALUES (@CartId, @ProductId)", new { cartItem.CartId, ProductId });
                
                if (response == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}