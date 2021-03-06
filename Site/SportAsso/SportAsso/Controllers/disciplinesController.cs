﻿using System;
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

        public static utilisateur FindUserByLogin(string login)
        {
            SportAssoEntities db = new SportAssoEntities();
            foreach(utilisateur u in db.utilisateur)
            {
                if(u.login == login)
                {
                    return u;
                }
            }
            return new utilisateur();
        }

        public static string GetUserFullNameByLogin(string login)
        {
            utilisateur u = FindUserByLogin(login);
            return u.prenom + ' ' + u.nom;
        }

        // GET: disciplines
        [Authorize(Roles ="encadrant , admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("encadrant"))
            {
                return Redirect("/Home/Encadrant");
            }
            var discipline = db.discipline.Include(d => d.utilisateur);
            return View(discipline.ToList());
        }

        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Redirect()
        {
            if (User.IsInRole("encadrant"))
            {
                return Redirect("/Home/Encadrant");
            }
            return Redirect("/disciplines/Index");
        }

        // GET: disciplines/Details/5
        [Authorize(Roles = "encadrant , admin")]
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
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            //ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                }
            }
            ViewBag.responsable_discipline_id = responsables;
            return View();
        }

        // POST: disciplines/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "discipline_id,responsable_discipline_id,label,description")] discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.discipline.Add(discipline);
                db.SaveChanges();
                return RedirectToAction("Redirect");
            }

            //ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                }
            }
            ViewBag.responsable_discipline_id = responsables;
            return View(discipline);
        }

        // GET: disciplines/Edit/5
        [Authorize(Roles = "encadrant, admin")]
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
            //ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    if (u.utilisateur_id == discipline.responsable_discipline_id)
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = true });
                    }
                    else
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                    }

                }
            }
            ViewBag.responsable_discipline_id = responsables;
            return View(discipline);
        }

        // POST: disciplines/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "encadrant, admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discipline_id,responsable_discipline_id,label,description")] discipline discipline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discipline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Redirect");
            }
            //ViewBag.responsable_discipline_id = new SelectList(db.utilisateur, "utilisateur_id", "login", discipline.responsable_discipline_id);
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    if (u.utilisateur_id == discipline.responsable_discipline_id)
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = true });
                    }
                    else
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                    }

                }
            }
            ViewBag.responsable_discipline_id = responsables;
            return View(discipline);
        }

        // GET: disciplines/Delete/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            discipline discipline = db.discipline.Find(id);
            db.discipline.Remove(discipline);
            db.SaveChanges();
            return RedirectToAction("Redirect");
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
