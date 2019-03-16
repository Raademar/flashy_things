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
using MySqlX.XDevAPI;


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
        
        // GET api/product/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var result = this.Ok(productService.Get(id));

            if (result == null)
            {
                return NotFound();
            }
            return this.Ok(result);
        }
        
        // POST api/product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] Product product)
        {
            var result = this.productService.Add(product);
            if (!result)
            {
                return this.BadRequest();
            }
            return this.Ok();
        }
        
        // POST (delete) /api/product/id
        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var result = this.productService.Delete(id);

            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
        // POST /api/product/id/addtocart
        //[Route(")]
        [HttpPost("~/api/product/{id}/addtocart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddToCart([FromBody] CartItem cartItem)
        {
            var result = this.productService.AddToCart(cartItem);
            
            if (!result)
            {
                return this.BadRequest();
            }

            return this.Ok();
            
        }
    }
}