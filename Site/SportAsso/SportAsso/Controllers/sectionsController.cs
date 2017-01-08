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
    public class sectionsController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        public static string FindDisciplineNameById(long? id)
        {
            if (id.HasValue)
            {
                SportAssoEntities db = new SportAssoEntities();
                return db.discipline.Find(id).label;
            }
            return "empty";
        }

        public static string FindFullTitleById(long section_id)
        {
            SportAssoEntities db = new SportAssoEntities();
            section s = db.section.Find(section_id);
            discipline d = db.discipline.Find(s.discipline_id);

            return d.label + " - " + s.label;
        }

        public static string FindUserFullNameById(long? id)
        {
            if (id.HasValue)
            {
                SportAssoEntities db = new SportAssoEntities();
                utilisateur u = db.utilisateur.Find(id);
                return u.prenom + ' ' + u.nom;
            }
            return "non affecté";
        }

        // GET: sections
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Index(long? id)
        {
            var section = db.section.Include(s => s.discipline);
            ViewData["title"] = "Liste des sections";
            if (id.HasValue)
            {
                ViewData["title"] = "Liste des sections de la discipline " + FindDisciplineNameById(id);
                section = from b in db.section.Include(s => s.discipline)
                where b.discipline_id == id
                select b;
            }
            return View(section.ToList());
        }


        // GET: sections/Details/5
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.section.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: sections/Create
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Create()
        {
            ViewBag.discipline_id = new SelectList(db.discipline, "discipline_id", "label");
            return View();
        }

        // POST: sections/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "encadrant , admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "section_id,description,discipline_id,label,prix")] section section)
        {
            if (ModelState.IsValid)
            {
                db.section.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.discipline_id = new SelectList(db.discipline, "discipline_id", "label", section.discipline_id);
            return View(section);
        }

        // GET: sections/Edit/5
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.section.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.discipline_id = new SelectList(db.discipline, "discipline_id", "label", section.discipline_id);
            ViewBag.responsable_id = new SelectList(db.utilisateur, "utilisateur_id", "login", section.responsable_id);
            return View(section);
        }

        // POST: sections/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "encadrant , admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "section_id,description,discipline_id,label,prix")] section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.discipline_id = new SelectList(db.discipline, "discipline_id", "label", section.discipline_id);
            return View(section);
        }

        // GET: sections/Delete/5
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            section section = db.section.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: sections/Delete/5
        [Authorize(Roles = "encadrant , admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            section section = db.section.Find(id);
            db.section.Remove(section);
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
