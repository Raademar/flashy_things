using System.Collections.Generic;
using System.Linq;
using Dapper;
using flashy_things.Models;
using MySql.Data.MySqlClient;

namespace flashy_things.Repositories
{
    public class ProductRepository
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
    }
}