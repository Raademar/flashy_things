namespace flashy_things.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public string DateAdded { get; set; }

    }
}