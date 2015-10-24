using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EducateMe4.Entities;

namespace EducateMe4.Controllers
{
    public class AdministrationController : Controller
    {
        private EducateMeEntities db = new EducateMeEntities();

        // GET: /Question/
        public ActionResult Index() {
            var questions = db.Questions.Include(q => q.QuestionType);
            return View(questions.ToList());
        }

        // GET: Administration
        public ActionResult Administration()
        {
            var questions = db.Questions.Include(q => q.QuestionType);
            return View(questions.ToList());
        }

        // GET: /Question/AddAnswer/5
        public ActionResult AddAnswer(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if(question == null) {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: /Question/Create
        public ActionResult Create() {
            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name");
            return View();
        }

        // POST: /Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,typeID,points,infoText,infoYoutubeURL,text")] Question question) {
            if(ModelState.IsValid) {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name", question.typeID);
            return View(question);
        }

        // GET: /Question/Edit/5
        public ActionResult Edit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if(question == null) {
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
        public ActionResult Edit([Bind(Include = "ID,typeID,points,infoText,infoYoutubeURL,text")] Question question) {
            if(ModelState.IsValid) {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.typeID = new SelectList(db.QuestionTypes, "ID", "name", question.typeID);
            return View(question);
        }

        // GET: /Question/Delete/5
        public ActionResult Delete(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if(question == null) {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: /Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult SelectedQuestions(List<ExpandoObject> values) {

            foreach(dynamic value in values) {

                int questionnaireID=value.step;
                int questionID = int.Parse(value.id);
                long x = value.x;
                long y = value.y;

                if (db.Pins.FirstOrDefault(f => f.questionID == questionID)!=null) {
                    db.Pins.Add(new Pin {
                        questionID = questionID,
                        y = y,
                        x = x,
                        questionnaireID = questionnaireID,
                        thumbnailID = 1
                    });
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}