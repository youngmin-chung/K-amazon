using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal MSRP { get; set; }
        public int QtyOrdered { get; set; }
        public int QtySold { get; set; }
        public int QtyBackOrdered { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
