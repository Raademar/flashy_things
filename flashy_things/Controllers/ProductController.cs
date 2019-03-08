using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using flashy_things.Models;
using flashy_things.Repositories;
using flashy_things.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace flashy_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string connectionString;
        private readonly ProductService productService;
        
        public ProductController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.productService = new ProductService(new ProductRepository(connectionString));
        }
        
        // GET api/product
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var result = this.Ok(productService.Get());

            if (result == null)
            {
                return NotFound();
            }

            return this.Ok(result);
        }

    }
}