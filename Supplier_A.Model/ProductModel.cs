namespace Supplier_A.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; } // Make ImageUrl nullable
    }

}
