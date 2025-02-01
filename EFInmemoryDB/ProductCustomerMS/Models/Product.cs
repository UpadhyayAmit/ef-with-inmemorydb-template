namespace ProductCustomerMS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CustomerId { get; set; } // Foreign key
        public Customer Customer { get; set; } // Navigation property
    }
}
