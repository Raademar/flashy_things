using System;
using System.ComponentModel.DataAnnotations;

namespace flashy_things.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public string GuestName { get; set; }
    }
}