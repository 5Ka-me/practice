using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsOnSale { get; set; } 
    }
}
