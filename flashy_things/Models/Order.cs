using System;
using System.ComponentModel.DataAnnotations;

namespace flashy_things.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}