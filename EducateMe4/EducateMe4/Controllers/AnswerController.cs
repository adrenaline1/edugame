using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Entities;

namespace EducateMe4.Controllers
{
    public class AnswerController : Controller
    {
        private EducateMeEntities db = new EducateMeEntities();

        // GET: /AnswersIndex
        public ActionResult AnswersIndex(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var closedanswers = db.ClosedAnswers.Include(c => c.Question).Where(c => c.questionID == questionId.Value);
            return View(closedanswers.ToList());
        }

        // GET: /Answer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedAnswer closedanswer = db.ClosedAnswers.Find(id);
            if (closedanswer == null)
            {
                return HttpNotFound();
            }
            return View(closedanswer);
        }

        // GET: /Answer/Create
        public ActionResult Create()
        {
            ViewBag.questionID = new SelectList(db.Questions, "ID", "infoText");
            return View();
        }

        // POST: /Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,questionID,text,image,isCorrect")] ClosedAnswer closedanswer)
        {
            if (ModelState.IsValid)
            {
                db.ClosedAnswers.Add(closedanswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.questionID = new SelectList(db.Questions, "ID", "infoText", closedanswer.questionID);
            return View(closedanswer);
        }

        // GET: /Answer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedAnswer closedanswer = db.ClosedAnswers.Find(id);
            if (closedanswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.questionID = new SelectList(db.Questions, "ID", "infoText", closedanswer.questionID);
            return View(closedanswer);
        }

        // POST: /Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,questionID,text,image,isCorrect")] ClosedAnswer closedanswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(closedanswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.questionID = new SelectList(db.Questions, "ID", "infoText", closedanswer.questionID);
            return View(closedanswer);
        }

        // GET: /Answer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedAnswer closedanswer = db.ClosedAnswers.Find(id);
            if (closedanswer == null)
            {
                return HttpNotFound();
            }
            return View(closedanswer);
        }

        // POST: /Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClosedAnswer closedanswer = db.ClosedAnswers.Find(id);
            db.ClosedAnswers.Remove(closedanswer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
