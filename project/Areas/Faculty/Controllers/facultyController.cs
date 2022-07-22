using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project.EDM;

namespace project.Areas.Faculty.Controllers
{
    public class facultyController : Controller
    {
        technotaskEntities dc = new technotaskEntities();
        // GET: Faculty/faculty

        
        public ActionResult Index()
        {
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

    }
}