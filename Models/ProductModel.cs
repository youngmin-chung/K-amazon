using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class ProductModel
    {
        private AppDbContext _db;
        public ProductModel(AppDbContext context)
        {
            _db = context;
        }
        public List<Product> GetAll()
        {
            return _db.Products.ToList();
        }
        public List<Product> GetAllByBrand(int id)
        {
            //Brand brand = _db.Brands.First(b => b.Name == brandName);
            return _db.Products.Where(item => item.BrandId == id).ToList();
        }
        public Brand GetBrand(int id)
        {
            return _db.Brands.FirstOrDefault(bran => bran.Id == id);
        }
    }
}
