using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class ProductInStock
    {
        [Key]
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
