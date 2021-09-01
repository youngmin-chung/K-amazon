using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class BrandModel
    {
        private AppDbContext _db;
        public BrandModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Brand> GetAll()
        {
            return _db.Brands.ToList<Brand>();
        }

        public string GetName(int id)
        {
            Brand bran = _db.Brands.First(c => c.Id == id);
            return bran.Name;
        }
    }
}
