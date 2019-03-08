using System.ComponentModel.DataAnnotations;

namespace flashy_things.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public int Stock { get; set; }
        public string DateAdded { get; set; }
    }
}