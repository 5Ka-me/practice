using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Models
{
    public class Storage
    {
        [Key]
        public int ProductId { get; set; }
        public int Count { get; set; }

        public Product Product { get; set; }
    }
}
