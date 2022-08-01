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
        public ActionResult Defaulthome(FormCollection fc)
        {
            var roles = fc["roles"];
            if (roles == "Admin")
            {
                return RedirectToAction("Login");
            }
            else if (roles == "Faculty")
            {
                return RedirectToAction("../../Faculty/faculty/Login");
            }
            else if (roles == "Student")
            {
                return RedirectToAction("../../Student/student/Login");
            }
            else
            {
                return View();
            }

            //return RedirectToAction("Index");
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
                Session["profimg"] = userobj.A_Image;
                Session["userroll"] = userobj.Roll;
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
        public ActionResult Register(FormCollection fc, HttpPostedFileBase file)
        {
            tbladmin obj = new tbladmin();
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["lname"];
            obj.E_mail = fc["email"];
            obj.Password = fc["password"];
            obj.Roll = fc["roll"];
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.A_Image = filename;
            }

            dc.tbladmins.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("Login");
        }
        public ActionResult Registerfaculty()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registerfaculty(FormCollection fc, HttpPostedFileBase file)
        {
            tblfaculty obj = new tblfaculty();
            obj.F_Name = fc["fname"];
            obj.L_Name = fc["lname"];
            obj.E_mail = fc["email"];
            obj.Password = fc["password"];
            obj.Roll = fc["roll"];
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string fullpath = Path.Combine(Server.MapPath("~/Images"), filename);
                file.SaveAs(fullpath);
                obj.F_Image = filename;
            }

            dc.tblfaculties.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("../../Faculty/faculty/Index");
        }
        public ActionResult Registerstudent()
        {
            FillStates();
            Fillbatches();
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
            obj.Roll = fc["roll"];
            string batch = fc["B_ID"];
            if (!string.IsNullOrEmpty(batch))
            {
                obj.B_ID = Convert.ToInt32(batch);
            }

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
        public ActionResult RegisterBatch()
        {
            FillFaculty();
            FillTechnology();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterBatch(FormCollection fc)
        {
            tblbatch obj = new tblbatch();
            obj.B_Name = fc["B_Name"];
            string bcdate = fc["B_CR_DATE"];
            if (!string.IsNullOrEmpty(bcdate))
            {
                obj.B_CR_DATE = Convert.ToDateTime(bcdate);
            }
            //obj.B_CR_DATE = fc["B_CR_DATE"];
            string bsdate = fc["B_ST_DATE"];
            if (!string.IsNullOrEmpty(bsdate))
            {
                obj.B_ST_DATE = Convert.ToDateTime(bsdate);
            }
            //obj.B_ST_DATE = fc["B_ST_DATE"];
            string bfaculty = fc["F_ID"];
            if (!string.IsNullOrEmpty(bfaculty))
            {
                obj.F_ID = Convert.ToInt32(bfaculty);
            }
            string btechnology = fc["T_ID"];
            if (!string.IsNullOrEmpty(btechnology))
            {
                obj.T_ID = Convert.ToInt32(btechnology);
            }
            //obj.F_ID = fc["F_ID"];
            obj.Status = fc["Status"];

            dc.tblbatches.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("../../Batches/batch/Index");
        }
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
        void FillFaculty()
        {
            List<SelectListItem> li = new List<SelectListItem>();

            var facultylist = dc.tblfaculties.ToList();

            foreach (var item in facultylist)
            {
                li.Add(new SelectListItem { Value = item.F_ID.ToString(), Text = item.F_Name });
            }
            ViewBag.facultyname = li;
        }
        void FillTechnology()
        {
            List<SelectListItem> li = new List<SelectListItem>();

            var technologylist = dc.tbltechnologies.ToList();

            foreach (var item in technologylist)
            {
                li.Add(new SelectListItem { Value = item.T_ID.ToString(), Text = item.T_Name });
            }
            ViewBag.technologyname = li;
        }
        void Fillbatches()
        {
            List<SelectListItem> li = new List<SelectListItem>();

            var batchlist = dc.tblbatches.ToList();

            foreach (var item in batchlist)
            {
                li.Add(new SelectListItem { Value = item.B_ID.ToString(), Text = item.B_Name });
            }
            ViewBag.batchname = li;
        }
        public JsonResult Getfaculty()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            return Json(dc.tblfaculties.ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getbatch()
        {

            dc.Configuration.ProxyCreationEnabled = false;
            List<tblbatch> List = new List<tblbatch>();
           var List1 = dc.tblbatches.Include("tblfaculty").Include("tbltechnology").Select(x => new  { 
            B_CR_DATE = x.B_CR_DATE,
            B_ID = x.B_ID,
            B_Name = x.B_Name,
            B_ST_DATE =x.B_ST_DATE,
            F_ID = x.F_ID,
            Status = x.Status,
            F_Name = x.tblfaculty != null ? x.tblfaculty.F_Name : "",
            T_Name = x.tbltechnology != null ? x.tbltechnology.T_Name : ""
           }).ToList();

            //var query =
            //from tblbatches in dc.tblbatches
            //join tblfaculties in dc.tblfaculties on tblbatches.F_ID equals tblfaculties.F_ID
            //select new { tblbatches = tblbatches, tblfaculties = tblfaculties };

            //var Data = query.ToList();
            return Json(List1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Getstudent()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            List<tblstudent> List = new List<tblstudent>();
            var List2 = dc.tblstudents.Include("tblstate").Include("tblcity").Select(x => new {
                S_ID = x.ST_ID,
                F_Name = x.F_Name,
                L_Name = x.L_Name,
                E_mail = x.E_mail,
                Address = x.Address,
                ST_ID = x.ST_ID,
                CT_ID = x.CT_ID,
                Mobile = x.Mobile,
                Status = x.Status,
                S_img = x.S_img,
                Satename = x.tblstate != null ? x.tblstate.Satename : "",
                Cityname = x.tblcity != null ? x.tblcity.Cityname : "",
            }).ToList();
            
            return Json(List2, JsonRequestBehavior.AllowGet);
        }
    }
}