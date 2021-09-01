using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class OrderLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        [StringLength(15)]
        public int QtyOrdered { get; set; }
        public int QtySold { get; set; }
        public int QtyBackOrdered { get; set; }
        [Column(TypeName = "money")]
        public decimal SellingPrice { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        public string ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
