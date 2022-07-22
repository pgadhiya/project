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
            return View(dc.tblbatches.ToList());
        }
    }
}