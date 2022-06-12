using System.Collections.Generic;

namespace Store.Models
{
    public static class Storage
    {
        public static List<(Product product, int count)> Products { get; set; } = new List<(Product product, int count)>();
    }
}
