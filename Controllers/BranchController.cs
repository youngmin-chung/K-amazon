using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using K_amazon.Models;
using K_amazon.Utils;


namespace K_amazon.Controllers
{
    public class BranchController : Controller
    {
        AppDbContext _db;
        public BranchController(AppDbContext context)
        {
            _db = context;
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVariables.Message) != null)
            {
                ViewBag.Message = HttpContext.Session.GetString(SessionVariables.Message);
            }
            return View();
        }
        [Route("[action]/{lat:double}/{lng:double}")]
        public IActionResult GetBranches(float lat, float lng)
        {
            BranchModel model = new BranchModel(_db);
            return Ok(model.GetThreeClosestBranches(lat, lng));
        }
    }
}