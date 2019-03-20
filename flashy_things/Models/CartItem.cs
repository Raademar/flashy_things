
using System;
using System.ComponentModel.DataAnnotations;

namespace flashy_things.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}