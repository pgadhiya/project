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
                return RedirectToAction("Home");
            }
            else
            {
                ViewBag.loginerror = "Invalid Email or Password.";
            }
            return View();
        }
        //public ActionResult GetImage(int UserId)
        //{
        //    var dir = Server.MapPath("~/Images");
        //    var path = Path.Combine(dir, UserId + ".jpg"); //validate the path for security or use other means to generate the path.
        //    return base.File(path, "image/jpeg");
        //}
        //public ActionResult Studentprofile(int id)
        //{
        //    var userImage = dc.tblstudents.Where(x => x.ST_ID == id).FirstOrDefault();

        //    return new FileContentResult(userImage.S_img);
        //}
        //public ActionResult Studentprofile(int id)
        //{
        //    tblstudent obj = new tblstudent();
        //    var stdimg = dc.tblstudents.Where(x => x.ST_ID == id).FirstOrDefault();
            
        //    if (stdimg != null)
        //    {
        //        //cookies               
        //        Session["profimg"] = stdimg.S_img;
        //    }
            
        //    return File(stdimg.S_img, "image/jpeg/png");
        //}
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}