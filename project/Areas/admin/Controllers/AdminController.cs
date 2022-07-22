using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.EDM;
using System.IO;

namespace project.Areas.admin.Controllers
{
    public class AdminController : Controller
    {
        //private const string mob = "mobile";
        technotaskEntities dc = new technotaskEntities();
        // GET: admin/Admin
        public ActionResult Index()
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
            var userobj = dc.tbladmins.Where(u => u.E_mail == email && u.Password == pass).FirstOrDefault();

            if (userobj != null)
            {
                //cookies
                Session["UserId"] = userobj.AD_ID;
                Session["UserName"] = userobj.F_Name;
                return RedirectToAction("Index");
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

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            tbladmin obj = new tbladmin();
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["lname"];
            obj.E_mail = fc["email"];
            obj.Password = fc["password"];

            dc.tbladmins.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("Login");
        }
        public ActionResult Registerfaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registerfaculty(FormCollection fc)
        {
            tblfaculty obj = new tblfaculty();
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["lname"];
            obj.E_mail = fc["email"];
            obj.Password = fc["password"];

            dc.tblfaculties.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("Login");
        }
        public ActionResult Registerstudent()
        {
            FillStates();
            return View();
        }
        
        [HttpPost]
        public ActionResult Registerstudent(FormCollection fc, HttpPostedFileBase file)
        {

            
            tblstudent obj = new tblstudent();

            obj.F_Name = fc["fname"];
            obj.L_Name = fc["lname"];
            obj.E_mail = fc["email"];
            obj.Password = fc["password"];
            string mob = fc["mobile"];
            // obj.Mobile = fc["mobile"];
            if (!string.IsNullOrEmpty(mob))
            {
                obj.Mobile = Convert.ToInt32(mob);
            } 
            obj.Address = fc["address"];
            string st = fc["state_id"];
            if (!string.IsNullOrEmpty(st))
            {
                obj.ST_ID = Convert.ToInt32(st);
            }
            string ct = fc["city_id"];
            if (!string.IsNullOrEmpty(st))
            {
                obj.CT_ID = Convert.ToInt32(ct);
            }
            obj.Status = fc["stdstatus"];
            //string fileimg = fc["file"];
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.S_img = filename;
            }
            dc.tblstudents.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("../../Student/student/Login");
        }
        //public ActionResult Studentprofile(int id)
        //{
        //    tblstudent obj = new tblstudent();
        //    var stdimg = dc.tblstudents.Where(x => x.ST_ID == id).FirstOrDefault();
        //    return  File(stdimg.S_img, "image/jpeg");
        //}
        void FillStates()
        {
           
            var stateslist = from s in dc.tblstates
            
                             select new SelectListItem
                             {
                                 Value = s.ST_ID.ToString(),
                                 Text = s.Satename
                             };
            
            ViewBag.states = stateslist.ToList();

        }

        public JsonResult GetCitiesByStateId(int id)
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var cities = dc.tblcities.Where(c => c.ST_ID == id).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getfaculty()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            return Json(dc.tblfaculties.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getbatch()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            return Json(dc.tblbatches.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getstudent()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            return Json(dc.tblstudents.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}