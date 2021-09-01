using Microsoft.AspNetCore.Mvc;
using K_amazon.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace K_amazon.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVariables.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVariables.LoginStatus, "not logged in");
            }
            if (HttpContext.Session.GetString(SessionVariables.LoginStatus) == "not logged in")
            {
                if (HttpContext.Session.GetString(SessionVariables.Message) == null)
                {
                    HttpContext.Session.SetString(SessionVariables.Message, "please login!");
                }
            }
            ViewBag.Status = HttpContext.Session.GetString(SessionVariables.LoginStatus);
            ViewBag.Message = HttpContext.Session.GetString(SessionVariables.Message);
            return View();
        }
    }
}
