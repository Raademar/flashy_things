using System;
using System.ComponentModel.DataAnnotations;

namespace flashy_things.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; }
        public string CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
        public int ProductId { get; set; }
        public virtual Product product { get; set; }
    }
}