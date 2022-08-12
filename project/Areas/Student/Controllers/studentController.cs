using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.EDM;
using System.IO;

namespace project.Areas.Student.Controllers
{
    public class studentController : Controller
    {
        technotaskEntities dc = new technotaskEntities();
        // GET: Student/student
        public ActionResult Index()
        {
            return View(dc.tblstudents.ToList());
        }
        public ActionResult Home()
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
            var userobj = dc.tblstudents.Where(u => u.E_mail == email && u.Password == pass ).FirstOrDefault();

            if (userobj != null)
            {
                //cookies
                Session["UserId"] = userobj.S_ID;
                Session["UserName"] = userobj.F_Name;
                Session["profimg"] = userobj.S_img;
                Session["userroll"] = userobj.Roll;
                Session["batch"] = userobj.B_ID;
                return RedirectToAction("StudentDetails/" + userobj.S_ID);
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
        public ActionResult StudentDetails(int id)
        {
            return View(dc.tblstudents.Find(id));
        }
        public ActionResult StudentEdit(int id)
        {
            var data = (from n in dc.tblstudents
                        where n.S_ID == id
                        select n).FirstOrDefault();
            ViewBag.studentobj = data;
            ViewBag.states = FillStates();
            return View();
        }

        [HttpPost]
        public ActionResult StudentEdit(FormCollection fc, HttpPostedFileBase file)
        {
            tblstudent obj = new tblstudent();
            //obj.S_ID = Convert.ToInt32(fc["F_ID"]);
            var S_ID = Convert.ToInt32(fc["S_ID"]);
            obj = dc.tblstudents.Find(S_ID);
            if (obj == null)
            {
                obj = new tblstudent();
            }
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["L_Name"];
            obj.E_mail = fc["E_mail"];
            obj.Password = fc["Password"];
            //obj.S_img = fc["F_Image"];
            obj.Mobile = Convert.ToInt32(fc["Mobile"]);
            obj.Address = fc["Address"]; 
            //obj.ST_ID = Convert.ToInt32(fc["ST_ID"]);
            string st = fc["state_id"];
            if (!string.IsNullOrEmpty(st))
            {
                obj.ST_ID = Convert.ToInt32(st);
            }
            //obj.CT_ID = Convert.ToInt32(fc["CT_ID"]);
            string ct = fc["city_id"];
            if (!string.IsNullOrEmpty(ct))
            {
                obj.CT_ID = Convert.ToInt32(ct);
            }
           
            obj.Status = fc["stdstatus"];

            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.S_img = filename;

            }


            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            //if(uRoll == "Admin")
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return RedirectToAction("Home");
            //}
            return RedirectToAction("Index");

        }

        public ActionResult StudentProfileEdit(int id)
        {
            var data = (from n in dc.tblstudents
                        where n.S_ID == id
                        select n).FirstOrDefault();
            ViewBag.studentobj = data;
            ViewBag.states = FillStates();
            return View();
        }

        [HttpPost]
        public ActionResult StudentProfileEdit(FormCollection fc, HttpPostedFileBase file)
        {
            tblstudent obj = new tblstudent();
            //obj.S_ID = Convert.ToInt32(fc["F_ID"]);
            var S_ID = Convert.ToInt32(fc["S_ID"]);
            obj = dc.tblstudents.Find(S_ID);
            if (obj == null)
            {
                obj = new tblstudent();
            }
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["L_Name"];
            obj.E_mail = fc["E_mail"];
            obj.Password = fc["Password"];
            //obj.S_img = fc["F_Image"];
            obj.Mobile = Convert.ToInt32(fc["Mobile"]);
            obj.Address = fc["Address"];
            //obj.ST_ID = Convert.ToInt32(fc["ST_ID"]);
            string st = fc["state_id"];
            if (!string.IsNullOrEmpty(st))
            {
                obj.ST_ID = Convert.ToInt32(st);
            }
            //obj.CT_ID = Convert.ToInt32(fc["CT_ID"]);
            string ct = fc["city_id"];
            if (!string.IsNullOrEmpty(ct))
            {
                obj.CT_ID = Convert.ToInt32(ct);
            }

            obj.Status = fc["stdstatus"];

            if (file != null && file.ContentLength > 0)
            {

                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.S_img = filename;

            }


            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            //if(uRoll == "Admin")
            //{
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return RedirectToAction("Home");
            //}
            return RedirectToAction("StudentDetails/"+ S_ID);

        }
        public ActionResult StudentDelete(int id)
        {
            var data = (from n in dc.tblstudents
                        where n.S_ID == id
                        select n).FirstOrDefault();
            ViewBag.studentobj = data;
            return View();
        }

        [HttpPost]
        [ActionName("StudentDelete")]
        public ActionResult StudentDeleteRec(int id)
        {
            dc.tblstudents.Remove(dc.tblstudents.Find(id));
            //dc.tblbatches.Remove(dc.tblbatches.Find(id));
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<SelectListItem> FillStates()
        {

            return  (from s in dc.tblstates

                             select new SelectListItem
                             {
                                 Value = s.ST_ID.ToString(),
                                 Text = s.Satename
                             }).ToList();

            

        }

        public JsonResult GetCitiesByStateId(int id)
        {
            dc.Configuration.ProxyCreationEnabled = false;
            var cities = dc.tblcities.Where(c => c.ST_ID == id).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
    }
}