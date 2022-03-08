using DataBaseFirstApproachEF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataBaseFirstApproachEF.Controllers
{
    public class HomeController : Controller
    {
        DataBaseFirstEFEntities db = new DataBaseFirstEFEntities();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {if(ModelState.IsValid == true)
            {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["InsertMessage"] = "<script>alert('Inserted Data')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["InsertMessage"] = "<script>alert('Inserted not Data')</script>";
                }
            }
            
            return View();
        }
        public ActionResult Edit(int id)
        {
            var row = db.Students.Where(model => model.id == id).FirstOrDefault();
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if(ModelState.IsValid == true)
            {
                db.Entry(s).State = EntityState.Modified;
                int a = db.SaveChanges();
                if(a > 0)
                {
                    TempData["UpdateMessage"] = "<script>alert('Updated Data')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["UpdateMessage"] = "<script>alert('Data is Not Updated')</script>";
                }
               
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var DeleteRow = db.Students.Where(model => model.id == id).FirstOrDefault();
            return View(DeleteRow);
        }
        [HttpPost]
        public ActionResult Delete(Student s)
        {
            db.Entry(s).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert('Delete Data')</script>";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["DeleteMessage"] = "<script>alert('Data not Delete')</script>";
            }
            return View();
        }
    }
}