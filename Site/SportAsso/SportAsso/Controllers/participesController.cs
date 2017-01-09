using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAsso;
using Microsoft.AspNet.Identity;


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

        public static string HourFormator(string time)
        {
            string[] res = time.Split(':');

            return res[0]+"h"+res[1];
        }

        public static string GetSeancesInfo(long id)
        {
            string res = "";
            SportAssoEntities db = new SportAssoEntities();

            seance s = db.seance.Find(id);
            section sec = db.section.Find(s.section_id);
            discipline d = db.discipline.Find(sec.discipline_id);

            res = d.label + " - " + sec.label + " : " + s.jour_de_la_semaine + " de " + HourFormator(""+s.heure_debut) + " à " + HourFormator(""+s.heure_fin);

            return res;
        }

        public static string StatutPaiment(bool paiment)
        {
            if (paiment)
            {
                return "Payer";
            }
            return "En attente de paiment";
        }

        [Authorize]
        public ActionResult MesInscriptions()
        {
            long id = GetUserIdByLogin(User.Identity.GetUserName());
            var participe = from par in db.participe.Include(p => p.seance).Include(p => p.utilisateur)
                            where par.utilisateur_id == id
                            select par;
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
        public static long GetUserIdByLogin(string login)
        {
            SportAssoEntities db = new SportAssoEntities();
            foreach (utilisateur u in db.utilisateur)
            {
                if(u.login == login)
                {
                    return u.utilisateur_id;
                }
            }   
            return 0;
        }

        [Authorize]
        public ActionResult Inscription(long id)
        {
            SportAssoEntities db = new SportAssoEntities();
            ViewBag.seance_id = id;
            seance s = db.seance.Find(id);
            section sec = db.section.Find(s.section_id);
            discipline d = db.discipline.Find(sec.discipline_id);
            ViewBag.utilisateur_id = GetUserIdByLogin(User.Identity.GetUserName());
            ViewBag.a_payer = false;
            ViewData["titre"] = "Inscription à la section " + sec.label + " de la discipline " + d.label;
            ViewData["texte"] = "Comfirmez votre inscription à la séance du "+ s.jour_de_la_semaine + " de "+ s.heure_debut + " à "+ s.heure_fin;
            return View();
        }

        // POST: participes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscription([Bind(Include = "utilisateur_id,seance_id,a_payer")] participe participe)
        {
            if (ModelState.IsValid)
            {
                db.participe.Add(participe);
                db.SaveChanges();
                return RedirectToAction("MesInscriptions");
            }
            ViewBag.message = "Erreur";
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
