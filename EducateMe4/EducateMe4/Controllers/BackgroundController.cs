using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Models;

namespace EducateMe4.Controllers {
    public class BackgroundController : Controller
    {
        public ActionResult Index()
        {
            BackgroundViewModel model = new BackgroundViewModel();

            return View(model);
        }
    }
}