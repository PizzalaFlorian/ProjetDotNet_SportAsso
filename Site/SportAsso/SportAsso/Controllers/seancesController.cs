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

        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Redirect()
        {
            if (User.IsInRole("encadrant"))
            {
                return Redirect("/Home/Encadrant");
            }
            return Redirect("/seance/Index");
        }

        // GET: seances
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Index(long? id)
        {
            var seance = db.seance.Include(s => s.lieu).Include(s => s.section).Include(s => s.utilisateur);
            ViewBag.titre_page = "Liste des Séances";
            ViewBag.ajoutLier = "false";
            ViewBag.section_id = 0;
               
            if(id.HasValue)
            {
               seance = from b in db.seance.Include(si => si.lieu).Include(si => si.section).Include(si => si.utilisateur)
                         where b.section_id == id
                         select b;
                section s = db.section.Find(id);
                discipline d = db.discipline.Find(s.discipline_id);
                ViewBag.titre_page = "Liste des séances de la section " + s.label + " de la discipline " + d.label;
                ViewBag.ajoutLier = "true";
                ViewBag.section_id = id;
            }
          
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

        public static string GetStringNumberOfParticipantByID(long seance_id)
        {
            SportAssoEntities db = new SportAssoEntities();
            var participations = from p in db.participe where p.seance_id == seance_id select p;
            int cpt = 0;
            foreach(participe p in participations)
            {
                cpt++;
            }
            return ""+cpt;
        }

        public static long GetNumberOfParticipantByID(long seance_id)
        {
            SportAssoEntities db = new SportAssoEntities();
            var participations = from p in db.participe where p.seance_id == seance_id select p;
            long cpt = 0;
            foreach (participe p in participations)
            {
                cpt++;
            }
            return cpt;
        }

        public static bool IsComplet(long seance_id)
        {
            long N_participant = GetNumberOfParticipantByID(seance_id);
            SportAssoEntities db = new SportAssoEntities();
            long max = db.seance.Find(seance_id).places_max;

            if(N_participant < max)
            {
                return false;
            }
            return true;
        }

        public ActionResult Inscriptions(long id)
        {
            ViewData["title"] = "Liste des séance de la section " + GetNameSectionByID(id) + " de la discipline " + GetNameDisciplineBySeanceID(id);
            var seance = from b in db.seance.Include(s => s.lieu).Include(s => s.section).Include(s => s.utilisateur)
                         where b.seance_id == id
                         select b;

            bool isEmpty = !seance.Any();
            if (isEmpty)
            {
                ViewBag.empty = "true";
                ViewData["empty"] = "Aucune séance de disponible pour cette discipline";
            }
            else
            {
                ViewBag.empty = "false";
                ViewData["empty"] = "";
            }

            ViewBag.section_id = id;
            return View(seance.ToList());
        }

        // GET: seances/Details/5
        [Authorize(Roles = "encadrant , admin")]
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
        [Authorize(Roles = "encadrant , admin")]
        public ActionResult Create(long? id)
        {
            ViewBag.lieu_id = new SelectList(db.lieu, "lieu_id", "label");
            if (id.HasValue)
            {
                ViewBag.section_id = new SelectList(db.section, "section_id", "description",id);
            }
            else
            {
                ViewBag.section_id = new SelectList(db.section, "section_id", "description");
            }
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id });
                }
            }
            ViewBag.encadrant_id = responsables;

            var jours = new List<SelectListItem>();
            jours.Add(new SelectListItem() { Text = "Lundi", Value = "Lundi" });
            jours.Add(new SelectListItem() { Text = "Mardi", Value = "Mardi" });
            jours.Add(new SelectListItem() { Text = "Mercredi", Value = "Mercredi" });
            jours.Add(new SelectListItem() { Text = "Jeudi", Value = "Jeudi" });
            jours.Add(new SelectListItem() { Text = "Vendredi", Value = "Vendredi" });
            jours.Add(new SelectListItem() { Text = "Samedi", Value = "Samedi" });
            jours.Add(new SelectListItem() { Text = "Dimanche", Value = "Dimanche" });

            ViewBag.jour_de_la_semaine = jours;
            //ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login");
            return View();
        }

        // POST: seances/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "encadrant , admin")]
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
            //ViewBag.encadrant_id = new SelectList(db.utilisateur, "utilisateur_id", "login", seance.encadrant_id);
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id });
                }
            }
            ViewBag.encadrant_id = responsables;
            var jours = new List<SelectListItem>();
            jours.Add(new SelectListItem() { Text = "Lundi", Value = "Lundi" });
            jours.Add(new SelectListItem() { Text = "Mardi", Value = "Mardi" });
            jours.Add(new SelectListItem() { Text = "Mercredi", Value = "Mercredi" });
            jours.Add(new SelectListItem() { Text = "Jeudi", Value = "Jeudi" });
            jours.Add(new SelectListItem() { Text = "Vendredi", Value = "Vendredi" });
            jours.Add(new SelectListItem() { Text = "Samedi", Value = "Samedi" });
            jours.Add(new SelectListItem() { Text = "Dimanche", Value = "Dimanche" });

            ViewBag.jour_de_la_semaine = jours;
            return View(seance);
        }

        public static bool isJourSelected(seance s,string jour)
        {
            if(s.jour_de_la_semaine == jour)
            {
                return true;
            }
            return false;
        }

        // GET: seances/Edit/5
        [Authorize(Roles = "encadrant , admin")]
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
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    if (u.utilisateur_id == seance.encadrant_id)
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = true });
                    }
                    else
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                    }

                }
            }
            ViewBag.encadrant_id = responsables;
            var jours = new List<SelectListItem>();
            jours.Add(new SelectListItem() { Text = "Lundi", Value = "Lundi",Selected = isJourSelected(seance,"Lundi") });
            jours.Add(new SelectListItem() { Text = "Mardi", Value = "Mardi", Selected = isJourSelected(seance, "Mardi") });
            jours.Add(new SelectListItem() { Text = "Mercredi", Value = "Mercredi", Selected = isJourSelected(seance, "Mercredi") });
            jours.Add(new SelectListItem() { Text = "Jeudi", Value = "Jeudi", Selected = isJourSelected(seance, "Jeudi") });
            jours.Add(new SelectListItem() { Text = "Vendredi", Value = "Vendredi", Selected = isJourSelected(seance, "Vendredi") });
            jours.Add(new SelectListItem() { Text = "Samedi", Value = "Samedi", Selected = isJourSelected(seance, "Samedi") });
            jours.Add(new SelectListItem() { Text = "Dimanche", Value = "Dimanche", Selected = isJourSelected(seance, "Dimanche") });

            ViewBag.jour_de_la_semaine = jours;
            return View(seance);
        }

        // POST: seances/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "encadrant , admin")]
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
            var responsables = new List<SelectListItem>();
            foreach (utilisateur u in db.utilisateur)
            {
                if (u.type_user == "encadrant")
                {
                    if (u.utilisateur_id == seance.encadrant_id)
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = true });
                    }
                    else
                    {
                        responsables.Add(new SelectListItem() { Text = u.prenom + " " + u.nom + " " + u.login, Value = "" + u.utilisateur_id, Selected = false });
                    }

                }
            }
            ViewBag.encadrant_id = responsables;
            ViewBag.encadrant_id = responsables;
            var jours = new List<SelectListItem>();
            jours.Add(new SelectListItem() { Text = "Lundi", Value = "Lundi", Selected = isJourSelected(seance, "Lundi") });
            jours.Add(new SelectListItem() { Text = "Mardi", Value = "Mardi", Selected = isJourSelected(seance, "Mardi") });
            jours.Add(new SelectListItem() { Text = "Mercredi", Value = "Mercredi", Selected = isJourSelected(seance, "Mercredi") });
            jours.Add(new SelectListItem() { Text = "Jeudi", Value = "Jeudi", Selected = isJourSelected(seance, "Jeudi") });
            jours.Add(new SelectListItem() { Text = "Vendredi", Value = "Vendredi", Selected = isJourSelected(seance, "Vendredi") });
            jours.Add(new SelectListItem() { Text = "Samedi", Value = "Samedi", Selected = isJourSelected(seance, "Samedi") });
            jours.Add(new SelectListItem() { Text = "Dimanche", Value = "Dimanche", Selected = isJourSelected(seance, "Dimanche") });

            ViewBag.jour_de_la_semaine = jours;
            return View(seance);
        }

        // GET: seances/Delete/5
        [Authorize(Roles = "encadrant , admin")]
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
        [Authorize(Roles = "encadrant , admin")]
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
