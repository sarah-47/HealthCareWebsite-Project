using HealthCareWebsite.Data;
using HealthCareWebsite.Models;
using HealthCareWebsite.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareWebsite.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext adb;
        private readonly DatabaseContext db;

        private readonly IHostingEnvironment hostingEnvironment;



        public AdminController(ApplicationDbContext _adb, DatabaseContext _db, IHostingEnvironment hostingEnvironment)
        {
            adb = _adb;
            db = _db;

            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {

            return View();
        }

 
            public  IActionResult ViewUsers()
            {
            IEnumerable<UsersInfo> users = (from user in adb.Users
                                  select new UsersInfo
                                  {
                                      Id = user.Id,
                                      UserName = user.UserName,
                                      Email = user.Email
                                  });
                return View(users);
            }
        public IActionResult ViewMeds()
        {
            IEnumerable<MedicinesModel> mlist = (from m in db.Medicines
                                                  from c in db.Categories
                                                  where m.CategoryId == c.CId
                                                  select new MedicinesModel
                                                  {
                                                      MId = m.MId,
                                                      MName = m.MName,
                                                      MPrice = m.MPrice,
                                                      MImage = m.MImage,
                                                      IsAvailable = m.IsAvailable,
                                                      CName = c.CName
                                                  }
           ).ToList();
            return View(mlist);
        }


        public IActionResult AddMedicine()
        {
            ViewBag.Categories = db.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddMedicine(MedicinesVM m)
        {


            string ffileName = null;
            //string ffileName = Guid.NewGuid().ToString() + "_" + m.MImage.FileName;;
            if (m.MImage != null)
            {
                string uplodefile = Path.Combine(hostingEnvironment.WebRootPath, "imgs");
                ffileName = m.MImage.FileName;
                string filePath = Path.Combine(uplodefile, ffileName);
                m.MImage.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            MedicinesModel mm = new MedicinesModel();


            mm.MName = m.MName;
            mm.MPrice = m.MPrice;
            mm.MImage = ffileName;
            mm.CategoryId = m.CId;
            mm.IsAvailable = m.IsAvailable;

            db.Medicines.Add(mm);
            db.SaveChanges();


            return RedirectToAction("ViewMeds");

        }

        public IActionResult UpdateMed(int id)
        {
            var data = this.db.Medicines.Where(x => x.MId == id).FirstOrDefault();
            MedicinesModel mv = new MedicinesModel();
            mv.MId = data.MId;
            mv.MName = data.MName;
            mv.MPrice = data.MPrice;
            mv.IsAvailable = data.IsAvailable;
            mv.CategoryId = data.CategoryId;

            ViewBag.Categories = db.Categories.ToList();

            return View(mv);
        }

        [HttpPost]
        public IActionResult UpdateMed(MedicinesModel m)
        {

            var mm = db.Medicines.Where(x => x.MId == m.MId).FirstOrDefault();


            mm.MName = m.MName;
            mm.MPrice = m.MPrice;
            mm.CategoryId = m.CategoryId;
            mm.IsAvailable = m.IsAvailable;



            db.Update(mm);
            db.SaveChanges();


            return RedirectToAction("ViewMeds");

        }

        public IActionResult DeleteMed(int id)
        {
            var mm = db.Medicines.Where(x => x.MId == id).FirstOrDefault();
            db.Remove(mm);
            db.SaveChanges();
            return RedirectToAction("ViewMeds");
        }


        public IActionResult CategoriesList()
        {


            return View(db.Categories.ToList());
        }

        public IActionResult AddCate()
        {


            return View();
        }
        [HttpPost]
        public IActionResult AddCate(CategoriesModel cm)
        {
            db.Add(cm);
            db.SaveChanges();

            return RedirectToAction("CategoriesList");
        }

        public IActionResult UpdateCate(int id)
        {

            var data = db.Categories.Where(x => x.CId == id).FirstOrDefault();

            return View("AddCate", data);
        }

        [HttpPost]
        public IActionResult UpdateCate(CategoriesModel cm)
        {

            var data = db.Categories.Where(x => x.CId == cm.CId).FirstOrDefault();
            data.CName = cm.CName;
            db.Update(data);
            db.SaveChanges();
            return RedirectToAction("CategoriesList");
        }
        public IActionResult DeleteCate(int id)
        {

            var cm = db.Categories.Where(x => x.CId == id).FirstOrDefault();
            db.Remove(cm);
            db.SaveChanges();
            return RedirectToAction("CategoriesList");
        }



    }

}
