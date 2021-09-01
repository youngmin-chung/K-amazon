using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using K_amazon.Utils;
using Microsoft.AspNetCore.Identity;
using K_amazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace K_amazon.Controllers
{
    public class LoginController : Controller
    {
        SignInManager<ApplicationUser> _signInMgr;
        UserManager<ApplicationUser> _usrMgr;
        public LoginController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInMgr = signInManager;
            _usrMgr = userManager;
        }
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl = null)
        {
            if (HttpContext.Session.Get(SessionVariables.LoginStatus) == null)
            {
                HttpContext.Session.SetString(SessionVariables.LoginStatus, "not logged in");
            }
            if (Convert.ToString(HttpContext.Session.Get(SessionVariables.LoginStatus)) == "not logged in")
            {
                HttpContext.Session.SetString(SessionVariables.Message, "please login");
            }
            ViewBag.status = HttpContext.Session.GetString(SessionVariables.LoginStatus);
            ViewBag.message = HttpContext.Session.GetString(SessionVariables.Message);
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        //
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInMgr.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser currentUser = await _usrMgr.FindByNameAsync(model.Email);
                    if (currentUser != null)
                    {
                        HttpContext.Session.Set(SessionVariables.User, currentUser);
                        HttpContext.Session.SetString(SessionVariables.LoginStatus, currentUser.Lastname + " logged in");
                        HttpContext.Session.SetString(SessionVariables.Message, "Welcome " + currentUser.Firstname + " " + currentUser.Lastname);
                    }
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    HttpContext.Session.SetString(SessionVariables.Message, "login attempt failed");
                    ViewBag.Status = "not logged on";
                    ViewBag.Message = HttpContext.Session.GetString(SessionVariables.Message);
                    return View("Index");
                }
            }
            // If we got this far, something failed, redisplay form
            return RedirectToLocal(returnUrl);
        }

        public async Task<IActionResult> Logoff()
        {
            await _signInMgr.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Session.SetString(SessionVariables.LoginStatus, "not logged in");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
