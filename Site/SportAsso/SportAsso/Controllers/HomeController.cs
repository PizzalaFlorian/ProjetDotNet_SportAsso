using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportAsso.Controllers
{
    public class HomeController : Controller      
    {
        private SportAssoEntities db = new SportAssoEntities();
        public ActionResult Index()
        {
            var discipline = db.discipline;
            return View(discipline.ToList());
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