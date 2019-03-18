using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using flashy_things.Models;
using flashy_things.Repositories;
using flashy_things.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;

namespace flashy_things.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly string connectionString;
        private readonly CartService cartService;
        
        public CartController(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("ConnectionString");
            this.cartService = new CartService(new CartRepository(connectionString));
        }
        
        // GET api/cart/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var result = cartService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return this.Ok(result);
        }
        
        // POST api/cart/{id}
        [HttpPost("~/api/cart/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SubmitOrder([FromBody] Cart cart, int customerId)
        {
            var result = this.cartService.SubmitOrder(cart, customerId);
            if (!result)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}