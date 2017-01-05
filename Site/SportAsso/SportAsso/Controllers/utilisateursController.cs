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
    public class utilisateursController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();


        // GET: utilisateurs
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.utilisateur.ToList());
        }


        // GET: utilisateurs/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // GET: utilisateurs/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "utilisateur_id,login,password,prenom,nom,type_user,adresse,telephone")] utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.utilisateur.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(utilisateur);
        }

        // GET: utilisateurs/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "utilisateur_id,login,password,prenom,nom,type_user,adresse,telephone")] utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: utilisateurs/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur utilisateur = db.utilisateur.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: utilisateurs/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            utilisateur utilisateur = db.utilisateur.Find(id);
            db.utilisateur.Remove(utilisateur);
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
