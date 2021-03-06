﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EducateMe4.Entities;

namespace EducateMe4.Controllers
{
    public class QuestionController : Controller
    {
        private EducateMeEntities db = new EducateMeEntities();

        // GET: /Question/
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.QuestionType);
            return View(questions.ToList());
        }

        // GET: /Question/AddAnswer/5
        public ActionResult AddAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClosedAnswer answer = new ClosedAnswer();
            answer.questionID = id.Value;

            db.ClosedAnswers.Add(answer);
            db.SaveChanges();

            return RedirectToAction("Answers", new {id = id});
        }

        // GET: /Question/Create
        public ActionResult Create()
        {
            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name");
            return View();
        }

        // POST: /Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,typeID,points,infoText,infoYoutubeURL,text")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name", question.typeID);
            return View(question);
        }

        // GET: /Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name", question.typeID);
            return View(question);
        }

        // POST: /Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,typeID,points,infoText,infoYoutubeURL,text")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name", question.typeID);
            return View(question);
        }

        // GET: /Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: /Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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

        // GET: /Question/Answers
        public ActionResult Answers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }


        // GET: /Question/Answers
        public ActionResult AnswersIndex(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ClosedAnswer> answers = db.ClosedAnswers.Where(c => c.questionID == questionId).ToList();

            return View(answers);
        }

        // POST: /Question/Delete/5
        [HttpGet, ActionName("DeleteAnswer")]
        public ActionResult Delete(int id)
        {
            ClosedAnswer answer = db.ClosedAnswers.Find(id);
            db.ClosedAnswers.Remove(answer);
            db.SaveChanges();

            return RedirectToAction("Answers", new {id = answer.questionID});
        }


        [HttpPost]
        public ActionResult SaveAnswers(List<ClosedAnswer> answers)
        {
            foreach (var answer in answers)
            {
                db.ClosedAnswers.AddOrUpdate(answer);
            }

            db.SaveChanges();

            return RedirectToAction("Answers", new { id = answers[0].questionID });
        }
    }

}

