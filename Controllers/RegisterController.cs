using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using K_amazon.Models;
using K_amazon.Utils;

namespace K_amazon.Controllers
{
    public class RegisterController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInMgr;
        public RegisterController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = userManager;
            _signInMgr = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        // POST:/Register/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Firstname = model.Firstname,
                    Address1 = model.Address1,
                    City = model.City,
                    Lastname = model.Lastname,
                    Age = model.Age,
                    CreditcardType = model.CreditcardType.Substring(0, 1),
                    Country = model.Country,
                    Region = model.Region,
                    Mailcode = model.Mailcode,

                };
                var addUserResult = await _usrMgr.CreateAsync(appUser, model.Password);
                if (addUserResult.Succeeded)
                {
                    await _signInMgr.SignInAsync(appUser, isPersistent: false);
                    HttpContext.Session.SetString(SessionVariables.LoginStatus, "logged on as " + model.Email);
                    HttpContext.Session.SetString(SessionVariables.Message, "Registered, logged on as " + model.Email);
                }
                else
                {
                    ViewBag.message = "registration failed - " + addUserResult.Errors.First().Description;
                    return View("Index");
                }
            }
            return Redirect("/Home");
        }
    }
}
