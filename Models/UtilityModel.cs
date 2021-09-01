using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace K_amazon.Models
{
    public class UtilityModel
    {
        private AppDbContext _db;
        public UtilityModel(AppDbContext context) // will be sent by controller
        {
            _db = context;
        }

        public bool loadLaptopTables(string stringJson)
        {
            bool brandsLoaded = false;
            bool productsLoaded = false;
            try
            {
                // dynamic keyword
                // tell the compiler that a variable's type can change or that it is not known until runtime.
                dynamic objectJson = Newtonsoft.Json.JsonConvert.DeserializeObject(stringJson);
                brandsLoaded = loadBrands(objectJson);
                productsLoaded = loadProducts(objectJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return brandsLoaded && productsLoaded;
        }

        private bool loadBrands(dynamic objectJson)
        {
            bool loadedBrands = false;
            try
            {
                // clear out the old rows
                _db.Brands.RemoveRange(_db.Brands);
                _db.SaveChanges();
                List<String> allBrands = new List<String>();
                foreach (var node in objectJson)
                {
                    allBrands.Add(Convert.ToString(node["BRAND"]));
                }
                // distinct will remove duplicates before we insert them into the db
                IEnumerable<String> brands = allBrands.Distinct<String>();
                foreach (string branname in brands)
                {
                    Brand bran = new Brand();
                    bran.Name = branname;
                    _db.Brands.Add(bran);
                    _db.SaveChanges();
                }
                loadedBrands = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedBrands;
        }

        private bool loadProducts(dynamic objectJson)
        {
            bool loadedProducts = false;
            try
            {
                List<Brand> brands = _db.Brands.ToList();
                // clear out the old
                _db.Products.RemoveRange(_db.Products);
                _db.SaveChanges();
                foreach (var node in objectJson)
                {
                    Product item = new Product();
                    item.Id = Convert.ToString(node["ProductId"]);
                    item.ProductName = Convert.ToString(node["PNAME"]);
                    item.GraphicName = Convert.ToString(node["GNAME"]);
                    item.CPU = Convert.ToString(node["CPU"]);
                    item.GPU = Convert.ToString(node["GRAPHIC"]);
                    item.RAM = Convert.ToString(node["RAM"]);
                    item.SSD = Convert.ToString(node["SSD"]);
                    item.CostPrice = Convert.ToDecimal(node["PRICE"].Value);
                    item.MSRP = Convert.ToDecimal(node["MSRP"].Value);
                    item.QtyOnHand = Convert.ToInt32(node["QTYONHAND"].Value);
                    item.QtyOnBackOrder = Convert.ToInt32(node["QTYBACKORDER"].Value);
                    item.Description = Convert.ToString(node["DESCRIPTION"]);
                    string brandName = Convert.ToString(node["BRAND"].Value);
                    // add the FK here
                    foreach (Brand brand in brands)
                    {
                        if (brand.Name == brandName)
                            item.Brand = brand;
                    }
                    _db.Products.Add(item);
                    _db.SaveChanges();
                }
                loadedProducts = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error - " + ex.Message);
            }
            return loadedProducts;
        }

    }
}
