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

        public string DateTime { get; private set; }

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
                Session["userroll"] = userobj.Roll;
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
        public JsonResult Getbatch()
        {

            dc.Configuration.ProxyCreationEnabled = false;
            List<tblbatch> List = new List<tblbatch>();
            var List1 = dc.tblbatches.Include("tblfaculty").Include("tbltechnology").Select(x => new {
                B_CR_DATE = x.B_CR_DATE,
                B_ID = x.B_ID,
                B_Name = x.B_Name,
                B_ST_DATE = x.B_ST_DATE,
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
        //public ActionResult Mybatches()
        //{


        //    return View();

        //}
        //public JsonResult Mybatches()
        //{
        //    dc.Configuration.ProxyCreationEnabled = false;
        //    List<tblbatch> List = new List<tblbatch>();
        //    var mybatch = dc.tblbatches.GroupBy(x=>x.F_ID).Select(x => new
        //    (
        //        F_ID = x.F_ID,
        //        B_ID = x.B_ID,
        //        B_Name = x.B_Name,
        //        facname = x.tblfaculty != null ? x.tblfaculty.F_Name : "",

        //    )).ToList();
        //    //return View();
        //    return Json(mybatch, JsonRequestBehavior.AllowGet);

        //}
        public ActionResult Tasklist()
        {
           
            return View();
        }
        public ActionResult ManageTask()
        {

            return View();
        }
        public JsonResult Gettasks()
        {

            dc.Configuration.ProxyCreationEnabled = false;
            List<tbltask> List = new List<tbltask>();
            var List1 = dc.tbltasks.Include("tbladmin").Include("tblfaculty").Select(x => new {
                Task_ID = x.Task_ID,
                Task_Desc = x.Task_Desc,
                AD_ID = x.AD_ID,
                F_ID = x.F_ID,
                Cr_Date = x.Cr_Date,
                Act_Date = x.Act_Date,
                Status = x.Status,
                adminname = x.tbladmin != null ? x.tbladmin.F_Name : "",
                facultyname = x.tblfaculty != null ? x.tblfaculty.F_Name : ""
            }).ToList();

            //var query =
            //from tblbatches in dc.tblbatches
            //join tblfaculties in dc.tblfaculties on tblbatches.F_ID equals tblfaculties.F_ID
            //select new { tblbatches = tblbatches, tblfaculties = tblfaculties };

            //var Data = query.ToList();
            return Json(List1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TaskEdit(int id)
        {
            var data = (from n in dc.tbltasks
                        where n.Task_ID == id
                        select n).FirstOrDefault();
            ViewBag.taskobj = data;
            return View();
        }

        [HttpPost]
        public ActionResult TaskEdit(FormCollection fc)
        {
            tbltask obj = new tbltask();
            var Task_ID = Convert.ToInt32(fc["Task_ID"]);
            obj = dc.tbltasks.Find(Task_ID);
            if (obj == null)
            {
                obj = new tbltask();
            }
            
            obj.Task_Desc = fc["Task_Desc"];
            obj.Status = fc["Status"];

            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("ManageTask");
        }
        public ActionResult TaskDelete(int id)
        {
            var data = (from n in dc.tbltasks
                        where n.Task_ID == id
                        select n).FirstOrDefault();
            ViewBag.taskobj = data;
            return View();
        }

        [HttpPost]
        [ActionName("TaskDelete")]
        public ActionResult TaskDeleteRec(int id)
        {
            dc.tbltasks.Remove(dc.tbltasks.Find(id));
            //dc.tblbatches.Remove(dc.tblbatches.Find(id));
            dc.SaveChanges();
            return RedirectToAction("ManageTask");
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
        void FillAdmin()
        {
            List<SelectListItem> li = new List<SelectListItem>();

            var adminlist = dc.tbladmins.ToList();

            foreach (var item in adminlist)
            {
                li.Add(new SelectListItem { Value = item.AD_ID.ToString(), Text = item.F_Name });
            }
            ViewBag.adminname = li;
        }
        public ActionResult Createtask()
        {
            FillAdmin();
            FillFaculty();
            return View();
        }
        [HttpPost]
        public ActionResult Createtask(FormCollection fc)
        {
            tbltask obj = new tbltask();
            obj.Task_Desc = fc["Task_Desc"];
            //obj.AD_ID = fc["AD_ID"];
            string adminid = fc["AD_ID"];
            if (!string.IsNullOrEmpty(adminid))
            {
                obj.AD_ID = Convert.ToInt32(adminid);
            }
            //obj.F_ID = fc["F_ID"];
            string facultyid = fc["F_ID"];
            if (!string.IsNullOrEmpty(facultyid))
            {
                obj.F_ID = Convert.ToInt32(facultyid);
            }

            string crdate = fc["Cr_Date"];
            if (!string.IsNullOrEmpty(crdate))
            {
                obj.Cr_Date = Convert.ToDateTime(crdate);
            }
            //obj.Cr_Date = fc["Cr_Date"];
           // obj.Act_Date = fc["Act_Date"];
            string actdate = fc["Act_Date"];
            if (!string.IsNullOrEmpty(actdate))
            {
                obj.Act_Date = Convert.ToDateTime(actdate);
            }
            obj.Status = fc["Status"];


            dc.tbltasks.Add(obj);
            dc.SaveChanges();

            return RedirectToAction("Tasklist");
        }

        private void UtcNow()
        {
            throw new NotImplementedException();
        }
    }
}