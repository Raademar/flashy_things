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
    public class ProductRepository : ControllerBase
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
                return connection.Query<Product>("SELECT * FROM Products;").ToList(); 
            }
        }

        public Product Get(int id)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE Id = @id;", new { id });
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
                
                connection.Execute("INSERT INTO Products (Title, Description, Image, Category, Stock, DateAdded) Values (@Title, @Description, @Image, @Category, @Stock, @DateAdded)", product);
                return true;
            }
        }

        public bool Delete(int id)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var result = connection.Execute("DELETE FROM Products WHERE Id = @id", new { id });
                
                if (result == 0)
                {
                    return false;
                }

                return true;
            }
        }
        
        public bool AddToCart(CartItem cartItem)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                var response = connection.Execute("INSERT INTO CartItem (CartId, ProductId) VALUES (@CartId, @ProductId)", cartItem );
                
                if (response == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}