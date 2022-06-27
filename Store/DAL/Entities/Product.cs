using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOnSale { get; set; } = false;

        public Category Category { get; set; }
    }
}
