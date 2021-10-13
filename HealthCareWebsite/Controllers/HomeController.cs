using HealthCareWebsite.Data;
using HealthCareWebsite.Helper;
using HealthCareWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext db;
        private readonly UserManager<IdentityUser> userManager;
        public List<Cart> myCart;

        public HomeController(ILogger<HomeController> logger, DatabaseContext _db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            db = _db;

            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult MedicineList()
        {
            IEnumerable<MedicinesModel> mlist = (from m in db.Medicines
                                                  from c in db.Categories
                                                  where m.CategoryId == c.CId
                                                  select new MedicinesModel
                                                  {
                                                      MName = m.MName,
                                                      MPrice = m.MPrice,
                                                      MImage = m.MImage,
                                                      IsAvailable = m.IsAvailable,
                                                      CName = c.CName,
                                                      MId=m.MId
                                                  }
            ).ToList();


            return View(mlist);
        }

        public ActionResult Search()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Search(String Searching)
        {
            IEnumerable<MedicinesModel> SearchResult = (from m in db.Medicines 
                                                        from c in db.Categories
                                                        where m.CategoryId==c.CId 
                                                        where m.MName.Contains(Searching)
                                                           || c.CName.Contains(Searching)
                                                        select new MedicinesModel
                                                        {
                                                            MName = m.MName,
                                                            MPrice = m.MPrice,
                                                            MImage = m.MImage,
                                                            IsAvailable = m.IsAvailable,
                                                            CName = c.CName,
                                                            MId=m.MId
                                                            
                                                        }
            ).ToList(); 
            return View("MedicineList", SearchResult);
        }
        
        [HttpPost]
        public JsonResult AddToCart(int id)
        {
            var objItem = db.Medicines.Find(id);
            var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");


            if (cart == null)
            {
                cart = new List<Cart>
                {
                    new Cart()
                    {
                        medicine = objItem,
                        Quantity = 1
                    }
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                var index = Exists(cart, id);
                if (index == -1)
                {
                    cart.Add(new Cart()
                    {
                        medicine = objItem,
                        Quantity = 1
                    });
                }
                else
                {
                    var newQuantity = cart[index].Quantity + 1;
                    cart[index].Quantity = newQuantity;

                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            }


            return Json(cart);
       
        }
        private int Exists(List<Cart> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].medicine.MId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult UCart()
        {
           
            myCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            return View(myCart);  
        }


        public IActionResult Remove(int id)
        {
            myCart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            var index = Exists(myCart, id);
            myCart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", myCart);

            return RedirectToAction("UCart");
        }
    }
}
