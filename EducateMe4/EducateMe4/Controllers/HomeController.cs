using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Models;

namespace EducateMe4.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            IndexViewModel model = new IndexViewModel();

            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}