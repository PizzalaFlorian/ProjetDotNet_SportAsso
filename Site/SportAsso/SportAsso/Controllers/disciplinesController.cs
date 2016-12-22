using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAsso;

namespace SportAsso.Controllers
{
    public class disciplinesController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        // GET: disciplines
        public ActionResult Index()
        {
            var discipline = db.discipline.Include(d => d.utilisateur);
            return View(discipline.ToList());
        }

        // GET: disciplines/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

        // GET: disciplines/Create
        public ActionResult Create()
        {
            ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            return View();
        }

        // POST: disciplines/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "discipline_id,responsable_discipline_id,label,description")] discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.discipline.Add(discipline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            return View(discipline);
        }

        // GET: disciplines/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            return View(discipline);
        }

        // POST: disciplines/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discipline_id,responsable_discipline_id,label,description")] discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discipline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            return View(discipline);
        }

        // GET: disciplines/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discipline discipline = db.discipline.Find(id);
            if (discipline == null)
            {
                return HttpNotFound();
            }
            return View(discipline);
        }

        // POST: disciplines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            discipline discipline = db.discipline.Find(id);
            db.discipline.Remove(discipline);
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
