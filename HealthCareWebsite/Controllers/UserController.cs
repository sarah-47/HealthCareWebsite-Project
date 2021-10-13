using HealthCareWebsite.Data;
using HealthCareWebsite.Helper;
using HealthCareWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly DatabaseContext db;
        public List<Cart> myCart;

        public UserController(UserManager<IdentityUser> userManager, DatabaseContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public IActionResult Index()
        {

            return View();
        }

        [Authorize]
        public IActionResult ConfirmOrder()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            myCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");

            foreach (var item in myCart)
            {

                OrderMedsModel order = new OrderMedsModel();
                order.MId = item.medicine.MId;
                order.Quantity = item.Quantity;
                order.UniPrice = item.medicine.MPrice * item.Quantity;
                order.UserID = userId;
                db.OrderMeds.Add(order);
                db.SaveChanges();
            }

            return RedirectToAction("Confirmed");
        }


        public IActionResult Confirmed()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            IEnumerable<Corder> order = (from o in db.OrderMeds
                                         join
                                             m in db.Medicines
                                             on o.MId equals m.MId
                                         where o.UserID==userId
                                         select new Corder()
                                         {
                                             Image = m.MImage,
                                             Name = m.MName,
                                             Quantity = o.Quantity,
                                             UniPrice = o.UniPrice,
                                         }

                                 ).ToList();
            return View(order);
        }
    }


}

