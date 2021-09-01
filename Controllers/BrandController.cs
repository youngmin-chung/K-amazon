using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using K_amazon.Models;
using K_amazon.Utils;


namespace K_amazon.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;
        public BrandController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BrandViewModel vm)
        {
            // only build the catalogue once
            if (HttpContext.Session.Get<List<Brand>>(SessionVariables.Brands) == null)
            {
                // no session information so let's go to the database
                try
                {
                    BrandModel catModel = new BrandModel(_db);
                    // now load the brands
                    List<Brand> brands = catModel.GetAll();
                    HttpContext.Session.Set<List<Brand>>(SessionVariables.Brands, brands);
                    vm.SetBrands(brands);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                // no need to go back to the database as information is already in the session
                vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVariables.Brands));
            }
            return View(vm);
        }

        public IActionResult SelectBrand(BrandViewModel vm)
        {
            BrandModel branModel = new BrandModel(_db);
            ProductModel productModel = new ProductModel(_db);
            List<Product> products = productModel.GetAllByBrand(vm.BrandId);
            List<ProductViewModel> vms = new List<ProductViewModel>();
            if (products.Count > 0)
            {
                foreach (Product product in products)
                {
                    ProductViewModel pvm = new ProductViewModel();
                    pvm.Qty = 0;
                    pvm.BrandId = product.BrandId;
                    pvm.BrandName = branModel.GetName(product.BrandId);
                    pvm.Description = product.Description;
                    pvm.Id = product.Id;
                    pvm.CPU = product.CPU;
                    pvm.GPU = product.GPU;
                    pvm.RAM = product.RAM;
                    pvm.SSD = product.SSD;
                    pvm.PRICE = Convert.ToDecimal(product.CostPrice);
                    pvm.MSRP = Convert.ToDecimal(product.MSRP);
                    pvm.QTYONHAND = product.QtyOnHand;
                    pvm.QTYBACKORDER = product.QtyOnBackOrder;
                    pvm.GNAME = product.GraphicName;
                    pvm.PNAME = product.ProductName;
                    pvm.BRAND = product.Id;
                    vms.Add(pvm);
                }
                ProductViewModel[] myProduct = vms.ToArray();
                HttpContext.Session.Set<ProductViewModel[]>(SessionVariables.Product, myProduct);
            }
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVariables.Brands));
            return View("Index", vm); // need the original Index View here
        }

        public ActionResult SelectProduct(BrandViewModel vm)
        {
            Dictionary<string, object> cart;
            if (HttpContext.Session.Get<Dictionary<string, Object>>(SessionVariables.Cart) == null)
            {
                cart = new Dictionary<string, object>();
            }
            else
            {
                cart = HttpContext.Session.Get<Dictionary<string, object>>(SessionVariables.Cart);
            }
            ProductViewModel[] product = HttpContext.Session.Get<ProductViewModel[]>(SessionVariables.Product);
            String retMsg = "";
            foreach (ProductViewModel item in product)
            {
                if (item.Id == vm.Id)
                {
                    if (vm.Qty > 0) // update only selected product
                    {
                        item.Qty = vm.Qty;
                        retMsg = vm.Qty + " - product(s) Added!";
                        cart[item.Id] = item;
                    }
                    else
                    {
                        item.Qty = 0;
                        cart.Remove(item.Id);
                        retMsg = "product(s) Removed!";
                    }
                    vm.BrandId = item.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.Set<Dictionary<string, Object>>(SessionVariables.Cart, cart);
            vm.SetBrands(HttpContext.Session.Get<List<Brand>>(SessionVariables.Brands));
            return View("Index", vm);
        }
    }
}
