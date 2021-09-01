using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K_amazon.Models
{
    public class Product
    {
        [StringLength(50)]
        public string Id { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; } // generates FK
        [Required]
        public int BrandId { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(20)]
        public string GraphicName { get; set; }
        [Required]
        [StringLength(80)]
        public string CPU { get; set; }
        [Required]
        [StringLength(80)]
        public string GPU { get; set; }
        [Required]
        [StringLength(10)]
        public string RAM { get; set; }
        [Required]
        [StringLength(50)]
        public string SSD { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "money")]
        [Required]
        public decimal MSRP { get; set; }
        [Required]
        public int QtyOnHand { get; set; }
        [Required]
        public int QtyOnBackOrder { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
    }
}
