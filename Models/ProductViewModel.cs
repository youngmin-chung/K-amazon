using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class ProductViewModel
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string RAM { get; set; }
        public string SSD { get; set; }
        public decimal PRICE { get; set; }
        public decimal MSRP { get; set; }
        public int QTYONHAND { get; set; }
        public int QTYBACKORDER { get; set; }
        public string BRAND { get; set; }
        public string GNAME { get; set; }
        public string PNAME { get; set; }

    }
}
