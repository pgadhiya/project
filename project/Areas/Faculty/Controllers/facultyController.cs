using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.EDM;
using System.IO;

namespace project.Areas.Faculty.Controllers
{
    public class facultyController : Controller
    {
        technotaskEntities dc = new technotaskEntities();
        // GET: Faculty/faculty

        
        public ActionResult Index()
        {
            //return View(dc.tblfaculties.ToList());
            return View(dc.tblfaculties.ToList());
        }
        public ActionResult Facultyhome()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["email"];
            string pass = fc["password"];
            
            var userobj = dc.tblfaculties.Where(u => u.E_mail == email && u.Password == pass).FirstOrDefault();

            if (userobj != null)
            {
                //cookies
                Session["UserId"] = userobj.F_ID;
                Session["UserName"] = userobj.F_Name;
                Session["profimg"] = userobj.F_Image;
                return RedirectToAction("Facultyhome");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or Password.";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult FacultyDetails(int id)
        {
            return View(dc.tblfaculties.Find(id));
        }
        public ActionResult FacultyEdit(int id)
        {
            var data = (from n in dc.tblfaculties
                        where n.F_ID == id
                        select n).FirstOrDefault();
            ViewBag.facultyobj = data;
            return View();
        }

        [HttpPost]
        public ActionResult FacultyEdit(FormCollection fc, HttpPostedFileBase file)
        {
            tblfaculty obj = new tblfaculty();
            var  F_ID = Convert.ToInt32(fc["F_ID"]);
            obj = dc.tblfaculties.Find(F_ID);
            if (obj==null)
            {
                obj= new tblfaculty();
            }
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["L_Name"];
            obj.E_mail = fc["E_mail"];
            obj.Password = fc["Password"];
            
            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.F_Image = filename;

            }
            

            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult FacultyDelete(int id)
        {
            var data = (from n in dc.tblfaculties
                        where n.F_ID == id
                        select n).FirstOrDefault();
            ViewBag.facultyobj = data;
            return View();
        }

        [HttpPost]
        [ActionName("FacultyDelete")]
        public ActionResult FacultyDeleteRec(int id)
        {
            dc.tblfaculties.Remove(dc.tblfaculties.Find(id));
            //dc.tblbatches.Remove(dc.tblbatches.Find(id));
            dc.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}