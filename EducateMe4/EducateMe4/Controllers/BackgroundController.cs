using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Entities;
using EducateMe4.Models;

namespace EducateMe4.Controllers {
    public class BackgroundController : Controller
    {
        public ActionResult Index()
        {
            EducateMeEntities Entities = new EducateMeEntities();
            BackgroundViewModel model = new BackgroundViewModel();

            model.Questionnaire = Entities.Questionnaires.First();

            return View(model);
        }

        public ActionResult ShowImage(int id)
        {
            EducateMeEntities Entities = new EducateMeEntities();
            byte[] imageData = Entities.Backgrounds.First().image;
            //if (imageData != null && imageData.Length > 0)
            //{
                return new FileStreamResult(new System.IO.MemoryStream(imageData), "image/jpeg");
            //}

        }
    }
}