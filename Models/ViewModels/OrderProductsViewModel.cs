namespace Fiore.Models.ViewModels
{
    public class OrderProductsViewModel
    {
        public string ImageName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  
        public decimal Subtotal { get; set; }
    }
}
