using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using K_amazon.Models;

namespace K_amazon.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;
        public ProductController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index(BrandViewModel brand)
        {
            ProductModel model = new ProductModel(_db);
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.Products = model.GetAllByBrand(brand.BrandId);
            viewModel.BrandName = model.GetBrand(brand.BrandId).Name;
            return View(viewModel);
        }
    }
}
