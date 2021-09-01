using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using K_amazon.Models;

namespace K_amazon.Controllers
{
    public class DataController : Controller
    {
        AppDbContext _ctx;
        public DataController(AppDbContext context)
        {
            _ctx = context;
        }
        public async Task<IActionResult> Index()
        {
            UtilityModel util = new UtilityModel(_ctx);
            string msg = "";
            var json = await getProductJsonFromWebAsync();
            try
            {
                msg = (util.loadLaptopTables(json)) ? "tables loaded" : "problem loading tables";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            ViewBag.LoadedMsg = msg;
            return View("Index");
        }

        private async Task<String> getProductJsonFromWebAsync()
        {
            string url = "https://raw.githubusercontent.com/YoungminCh/json/master/casestudy.json";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
