using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAsso.Controllers
{
    public class HomeController : Controller      
    {
        private SportAssoEntities db = new SportAssoEntities();
        
        public ActionResult Index(int? id)
        {
            if (NeedToSwithByRole())
            {
                return SwitchHomeByModelRole();
            }
           
            var discipline = db.discipline;
            ViewData["titreSection"] = "test";
            ViewData["descriptionSection"] = "test";
            if (id.HasValue == false || id == 0)
            {
                ViewBag.id = 0;
                ViewBag.detail = false;
                ViewData["titreSection"] = "Découvrez les plaisirs du sport chez Sports Asso !";
                ViewData["descriptionSection"] = "Des dizaines de disciplines exaltantes dispnnibles. Encadré par des proffessionels du sport, venez découvrir les nombreuses activité propossé par nontre association !";
            }
            else
            {
                ViewBag.id = id;
                ViewBag.detail = true;
                discipline d = discipline.Find(id);
                ViewData["titreSection"] = d.label;
                ViewData["descriptionSection"] = d.description;
            }
            IQueryable<SportAsso.section> q = from d in db.section where d.discipline_id == id select d;
            ViewBag.list = q.ToList<section>();

            return View(discipline.ToList());
        }

        public static string getCssById(long id,long index)
        {
            if(id == index)
            {
                return "collection-item active";
            }
            return "collection-item";

        }

        private bool NeedToSwithByRole()
        {
            if (User.IsInRole("encadrant"))
            {
                return true;
            }
            if (User.IsInRole("admin"))
            {
                return true;
            }
            return false;
        }

        private ActionResult SwitchHomeByModelRole()
        {
            if (User.IsInRole("encadrant"))
            {
                return Redirect("/Home/Encadrant");
            }
            if (User.IsInRole("admin"))
            {
                return Redirect("/utilisateurs/index");
            }
            return Redirect("/Home/Index");
        }

        public static long GetIdByUserName(string login)
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

        [Authorize(Roles ="encadrant")]
        public ActionResult Encadrant()
        {
            ViewBag.Message = "Acceuil Encadrant";
            long id = GetIdByUserName(User.Identity.Name);

            /*discipline*/
            IQueryable<SportAsso.discipline> discipline = from di in db.discipline where di.responsable_discipline_id == id select di;
            ViewBag.discipline = discipline.ToList<discipline>();
            if (!discipline.Any())
            {
                ViewBag.hasDiscipline = "true";
            }
            else
            {
                ViewBag.hasDiscipline = "false";
            }

            /*sections*/
            IQueryable<SportAsso.section> section = from di in db.section where di.responsable_id == id select di;
            ViewBag.section = section.ToList<section>();
            if (!section.Any())
            {
                ViewBag.hasSection = "true";
            }
            else
            {
                ViewBag.hasSection = "false";
            }

            /*seance*/
            IQueryable<SportAsso.seance> seance = from di in db.seance where di.encadrant_id == id select di;
            ViewBag.seance = seance.ToList<seance>();
            if (!seance.Any())
            {
                ViewBag.hasSeance = "true";
            }
            else
            {
                ViewBag.hasSeance = "false";
            }

            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Accueil Administrateur";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*
        public ActionResult disciplineInfo( int id)
        {
            String res = null;
            foreach(discipline d in db.discipline)
            {
                if(d.responsable_discipline_id == id)
                {
                    res = d.description;
                }
            }
            return View(res);
        }*/
    }
}