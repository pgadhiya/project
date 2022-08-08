using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.EDM;

namespace project.Areas.Batches.Controllers
{
    public class batchController : Controller
    {
        technotaskEntities dc = new technotaskEntities();
        // GET: Batches/batch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BatchDetails(int id)
        {

            return View(dc.tblbatches.Find(id));
        }
        public ActionResult BatchDetails1()
        {

            dc.Configuration.ProxyCreationEnabled = false;
            List<tblbatch> List = new List<tblbatch>();
            var List1 = dc.tblbatchdetails.Include("tblbatch").Include("tblstudent").Select(x => new
            {
               
                //id = x.B_ID,
                B_ID = x.B_ID,
                S_ID = x.S_ID,
                B_Name = x.tblbatch != null ? x.tblbatch.B_Name : "",
                S_Name = x.tblstudent != null ? x.tblstudent.F_Name : "",
                
            }).ToList();

            return Json(List1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBatch()
        {
            dc.Configuration.ProxyCreationEnabled = false;
            List<tblbatch> List = new List<tblbatch>();
            var List1 = dc.tblbatches.Include("tblfaculty").Include("tbltechnology").Select(x => new
            {
                B_CR_DATE = x.B_CR_DATE,
                B_ID = x.B_ID,
                B_Name = x.B_Name,
                B_ST_DATE = x.B_ST_DATE,
                F_ID = x.F_ID,
                Status = x.Status,
                F_Name = x.tblfaculty != null ? x.tblfaculty.F_Name : "",
                T_Name = x.tbltechnology != null ? x.tbltechnology.T_Name : ""
            }).ToList();

            return Json(List1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BatchEdit(int id)
        {
            var data = (from n in dc.tblbatches
                        where n.B_ID == id
                        select n).FirstOrDefault();
            ViewBag.batchobj = data;
            ViewBag.technologies = FillTechnology();
            ViewBag.faculties = Fillfaculty();
            return View();
        }

        [HttpPost]
        public ActionResult BatchEdit(FormCollection fc)
        {
            tblbatch obj = new tblbatch();
            //obj.S_ID = Convert.ToInt32(fc["F_ID"]);
            var B_ID = Convert.ToInt32(fc["B_ID"]);
            obj = dc.tblbatches.Find(B_ID);
            if (obj == null)
            {
                obj = new tblbatch();
            }
            obj.B_Name = fc["B_Name"];
            //obj.ST_ID = Convert.ToInt32(fc["ST_ID"]);
            string tech = fc["T_ID"];
            if (!string.IsNullOrEmpty(tech))
            {
                obj.T_ID = Convert.ToInt32(tech);
            }
            //obj.CT_ID = Convert.ToInt32(fc["CT_ID"]);
            string fac = fc["F_ID"];
            if (!string.IsNullOrEmpty(fac))
            {
                obj.F_ID = Convert.ToInt32(fac);
            }
            string bstdate = fc["B_ST_DATE"];
            if (!string.IsNullOrEmpty(bstdate))
            {
                obj.B_ST_DATE = Convert.ToDateTime(bstdate);
            }
            string bcrdate = fc["B_CR_DATE"];
            if (!string.IsNullOrEmpty(bcrdate))
            {
                obj.B_CR_DATE = Convert.ToDateTime(bcrdate);
            }

            obj.Status = fc["Status"];

            dc.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<SelectListItem> FillTechnology()
        {

            return (from s in dc.tbltechnologies

                    select new SelectListItem
                    {
                        Value = s.T_ID.ToString(),
                        Text = s.T_Name
                    }).ToList();
        }
        public List<SelectListItem> Fillfaculty()
        {

            return (from s in dc.tblfaculties

                    select new SelectListItem
                    {
                        Value = s.F_ID.ToString(),
                        Text = s.F_Name
                    }).ToList();
        }
        public ActionResult BatchDelete(int id)
        {
            var data = (from n in dc.tblbatches
                        where n.B_ID == id
                        select n).FirstOrDefault();
            ViewBag.batchobj = data;
            return View();
        }

        [HttpPost]
        [ActionName("BatchDelete")]
        public ActionResult BatchDeleteRec(int id)
        {
            dc.tblbatches.Remove(dc.tblbatches.Find(id));
            //dc.tblbatches.Remove(dc.tblbatches.Find(id));
            dc.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
