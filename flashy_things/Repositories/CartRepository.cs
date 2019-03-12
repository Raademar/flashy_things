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

        public List<Product> Get(int id)
        {
            using (var connection = new MySqlConnection(this.ConnectionString))
            {
                return connection.Query<Product>("SELECT * FROM CartItem WHERE CartId = @id;", new { id }).ToList(); 
            }
        }
    }
}