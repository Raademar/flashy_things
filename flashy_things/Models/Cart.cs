using System.Collections.Generic;

namespace flashy_things.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public List<Product> Products { get; set; }
        public int CartCompleted { get; set; }
    }
}