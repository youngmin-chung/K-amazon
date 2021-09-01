using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace K_amazon.Models
{
    public class BrandViewModel
    {
        private List<Brand> _brands;
        public BrandViewModel() { }
        public string BrandName { get; set; }
        public string Id { get; set; }
        public int BrandId { get; set; }
        public int Qty { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SelectListItem> GetBrands()
        {
            return _brands.Select(brand => new SelectListItem { Text = brand.Name, Value = brand.Id.ToString() });
        }
        public void SetBrands(List<Brand> brans)
        {
            _brands = brans;
        }

    }
}
