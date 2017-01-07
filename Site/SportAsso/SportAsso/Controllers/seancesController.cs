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
    public class seancesController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        // GET: seances
        public ActionResult Index()
        {
            var seance = db.seance.Include(s => s.lieu).Include(s => s.section).Include(s => s.utilisateur);
            return View(seance.ToList());
        }

        public static string GetNameSectionByID(long id)
        {
            SportAssoEntities db = new SportAssoEntities();
            return db.section.Find(id).label;
        }

        public static string GetNameDisciplineBySeanceID(long id)
        {
            SportAssoEntities db = new SportAssoEntities();
            long dis_id = db.section.Find(id).discipline_id;
            return db.discipline.Find(dis_id).label;
        }
        public ActionResult Inscriptions(long id)
        {
            ViewData["title"] = "Liste des séance de la section " + GetNameSectionByID(id) + " de la discipline " + GetNameDisciplineBySeanceID(id);
            var seance = from b in db.seance.Include(s => s.lieu).Include(s => s.section).Include(s => s.utilisateur)
                         where b.seance_id == id
                         select b;
            return View(seance.ToList());
        }

        // GET: seances/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // GET: seances/Create
        public ActionResult Create()
        {
            ViewBag.lieu_id = new SelectList(db.lieu, "lieu_id", "label");
            ViewBag.section_id = new SelectList(db.section, "section_id", "description");
            ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            return View();
        }

        // POST: seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "seance_id,encadrant_id,lieu_id,section_id,places_max,heure_debut,heure_fin,jour_de_la_semaine")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.seance.Add(seance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lieu_id = new SelectList(db.lieu, "lieu_id", "label", seance.lieu_id);
            ViewBag.section_id = new SelectList(db.section, "section_id", "description", seance.section_id);
            ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login", seance.encadrant_id);
            return View(seance);
        }

        // GET: seances/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            ViewBag.lieu_id = new SelectList(db.lieu, "lieu_id", "label", seance.lieu_id);
            ViewBag.section_id = new SelectList(db.section, "section_id", "description", seance.section_id);
            ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login", seance.encadrant_id);
            return View(seance);
        }

        // POST: seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "seance_id,encadrant_id,lieu_id,section_id,places_max,heure_debut,heure_fin,jour_de_la_semaine")] seance seance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lieu_id = new SelectList(db.lieu, "lieu_id", "label", seance.lieu_id);
            ViewBag.section_id = new SelectList(db.section, "section_id", "description", seance.section_id);
            ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login", seance.encadrant_id);
            return View(seance);
        }

        // GET: seances/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            seance seance = db.seance.Find(id);
            if (seance == null)
            {
                return HttpNotFound();
            }
            return View(seance);
        }

        // POST: seances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            seance seance = db.seance.Find(id);
            db.seance.Remove(seance);
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
