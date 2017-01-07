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
    public class participesController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        // GET: participes
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var participe = db.participe.Include(p => p.seance).Include(p => p.utilisateur);
            return View(participe.ToList());
        }

        [Authorize]
        public ActionResult MesInscritions()
        {
            var participe = db.participe.Include(p => p.seance).Include(p => p.utilisateur);
            return View(participe.ToList());
        }

        // GET: participes/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participe participe = db.participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // GET: participes/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id");
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            return View();
        }

        // POST: participes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "utilisateur_id,seance_id,a_payer")] participe participe)
        {
            if (ModelState.IsValid)
            {
                db.participe.Add(participe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", participe.seance_id);
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", participe.utilisateur_id);
            return View(participe);
        }

        [Authorize]
        public ActionResult Inscription(long id)
        {
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id");
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            return View();
        }

        // POST: participes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscritpion([Bind(Include = "utilisateur_id,seance_id,a_payer")] participe participe)
        {
            if (ModelState.IsValid)
            {
                db.participe.Add(participe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", participe.seance_id);
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", participe.utilisateur_id);
            return View(participe);
        }

        // GET: participes/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participe participe = db.participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", participe.seance_id);
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", participe.utilisateur_id);
            return View(participe);
        }

        // POST: participes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "utilisateur_id,seance_id,a_payer")] participe participe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.seance_id = new SelectList(db.seance, "seance_id", "seance_id", participe.seance_id);
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", participe.utilisateur_id);
            return View(participe);
        }

        // GET: participes/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            participe participe = db.participe.Find(id);
            if (participe == null)
            {
                return HttpNotFound();
            }
            return View(participe);
        }

        // POST: participes/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            participe participe = db.participe.Find(id);
            db.participe.Remove(participe);
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
