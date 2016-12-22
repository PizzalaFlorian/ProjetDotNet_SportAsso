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
    public class creneauxController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        // GET: creneaux
        public ActionResult Index()
        {
            var creneau = db.creneau.Include(c => c.seance);
            return View(creneau.ToList());
        }

        // GET: creneaux/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creneau creneau = db.creneau.Find(id);
            if (creneau == null)
            {
                return HttpNotFound();
            }
            return View(creneau);
        }

        // GET: creneaux/Create
        public ActionResult Create()
        {
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id");
            return View();
        }

        // POST: creneaux/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "creneau_id,seance_id,heure_debut,heure_fin,jour_de_la_semaine")] creneau creneau)
        {
            if (ModelState.IsValid)
            {
                db.creneau.Add(creneau);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", creneau.seance_id);
            return View(creneau);
        }

        // GET: creneaux/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creneau creneau = db.creneau.Find(id);
            if (creneau == null)
            {
                return HttpNotFound();
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", creneau.seance_id);
            return View(creneau);
        }

        // POST: creneaux/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "creneau_id,seance_id,heure_debut,heure_fin,jour_de_la_semaine")] creneau creneau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creneau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", creneau.seance_id);
            return View(creneau);
        }

        // GET: creneaux/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creneau creneau = db.creneau.Find(id);
            if (creneau == null)
            {
                return HttpNotFound();
            }
            return View(creneau);
        }

        // POST: creneaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            creneau creneau = db.creneau.Find(id);
            db.creneau.Remove(creneau);
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
