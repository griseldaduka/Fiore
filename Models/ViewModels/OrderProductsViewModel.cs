namespace Fiore.Models.ViewModels
{
    public class OrderProductsViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  
        public decimal SubtotalPrice  { get; set; }
    }
}
