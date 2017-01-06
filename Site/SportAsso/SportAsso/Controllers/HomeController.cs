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
       
        public static string titreSection { set; get; }
        public static string descriptionSection { set; get; }

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
                ViewData["titreSection"] = "Découvrez les plaisirs du sport chez Sports Asso !";
                ViewData["descriptionSection"] = "Des dizaines de disciplines exaltantes dispnnibles. Encadré par des proffessionels du sport, venez découvrir les nombreuses activité propossé par nontre association !";
                
                ViewBag.sections = from b in db.section where b.discipline_id == id select b;
            }
            else
            {
                discipline d = discipline.Find(id);
                ViewData["titreSection"] = d.label;
                ViewData["descriptionSection"] = d.description;
            }
            
            return View(discipline.ToList());
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

        [Authorize(Roles ="encadrant")]
        public ActionResult Encadrant()
        {
            ViewBag.Message = "Acceuil Encadrant";
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

        public ActionResult disciplineInfo( int id)
        {
            String res = null;
            foreach(discipline d in db.discipline)
            {
                if(d.discipline_id == id)
                {
                    res = d.description;
                }
            }
            return View(res);
        }
    }
}