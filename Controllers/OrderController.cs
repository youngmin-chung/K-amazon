using Microsoft.AspNetCore.Mvc;
using K_amazon.Utils;
using K_amazon.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace K_amazon.Controllers
{
    public class OrderController : Controller
    {
        AppDbContext _db;
        public OrderController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ClearCart() // clear out current cart
        {
            HttpContext.Session.Remove(SessionVariables.Cart);
            HttpContext.Session.SetString(SessionVariables.Message, "Cart Cleared");
            return Redirect("/Home");
        }

        [Route("[action]")]
        public IActionResult GetOrders()
        {
            OrderModel model = new OrderModel(_db);
            ApplicationUser user = HttpContext.Session.Get<ApplicationUser>(SessionVariables.User);
            return Ok(model.GetAll(user.Id));
        }

        [Route("[action]/{tid:int}")]
        public IActionResult GetOrderDetails(int tid)
        {
            OrderModel model = new OrderModel(_db);
            ApplicationUser user = HttpContext.Session.Get<ApplicationUser>(SessionVariables.User);
            return Ok(model.GetOrderDetails(tid, user.Id));
        }


        // Add the cart, pass the session variable info to the db
        public ActionResult AddOrder()
        {
            OrderModel model = new OrderModel(_db);
            int retVal = -1;
            string retMessage = "";
            try
            {
                Dictionary<string, string> orderMsg = new Dictionary<string, string>
                {
                    { SessionVariables.OrderMessage, null }
                };

                Dictionary<string, object> cartItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVariables.Cart);
                retVal = model.AddOrder(cartItems, HttpContext.Session.Get<ApplicationUser>(SessionVariables.User), orderMsg);
                if (retVal > 0 || orderMsg[SessionVariables.Message] == null) // Cart Added
                {
                    retMessage = "Order " + retVal + " Created!" + orderMsg[SessionVariables.OrderMessage];
                }
                else // problem
                {
                    retMessage = "Order not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Order was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVariables.Cart); // clear out current cart once persisted
            HttpContext.Session.SetString(SessionVariables.Message, retMessage);
            return Redirect("/Home");
        }

        public IActionResult List()
        {
            // they can't list Trays if they're not logged on
            if (HttpContext.Session.Get<ApplicationUser>(SessionVariables.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }
    }
}
