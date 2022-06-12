using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Receipt
    {
        [Key]
        public int ReceiptId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
