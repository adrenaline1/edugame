using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Entities;
using EducateMe4.Models;

namespace EducateMe4.Controllers {
    public class BackgroundAdminController : Controller
    {
        public ActionResult Index()
        {
            return View("BackgroundAdmin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    EducateMeEntities entities = new EducateMeEntities();
                    Background background = entities.Backgrounds.Create<Background>();
                    background.name = "mapppp";
                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        background.image = reader.ReadBytes(upload.ContentLength);
                    }
                    background.imageType = upload.FileName.Substring(upload.FileName.Length-4);
                    //background.ID = 5;

                    entities.Backgrounds.Add(background);
                    entities.SaveChanges();

                    return View("Administration", background);
                }
            }

            return View("Administration");
        }
    }
}