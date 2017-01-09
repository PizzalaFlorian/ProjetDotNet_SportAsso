using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAsso;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Permissions;
using System.Security;

namespace SportAsso.Controllers
{
    public class documentsController : Controller
    {
        private SportAssoEntities db = new SportAssoEntities();

        // GET: documents
        public ActionResult Index()
        {
            var document = db.document.Include(d => d.utilisateur);
            return View(document.ToList());
        }

        public static long GetIdByLogin(string login)
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
        public ActionResult MesDocuments()
        {
            long id = GetIdByLogin(User.Identity.Name);
            var document = from b in db.document.Include(d => d.utilisateur)
                           where b.utilisateur_id == id
                           select b;
            return View(document.ToList());
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase filee)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    long id = GetIdByLogin(User.Identity.Name);
                    var fileName = Path.GetFileName(file.FileName);
                    string upload_path = "D:\\workspace\\DotNET\\Projet\\ProjetDotNet_SportAsso\\Site\\SportAsso\\SportAsso\\Upload\\";
                    if (!Directory.Exists("~/Upload/" + id + "/"))
                    {
                        FileIOPermission f2 = new FileIOPermission(FileIOPermissionAccess.Read, upload_path);
                        f2.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, upload_path);
                        try
                        {
                            f2.Demand();
                        }
                        catch (SecurityException s)
                        {
                            Console.WriteLine(s.Message);
                        }
                        /*Directory.CreateDirectory(upload_path + id + "\\");
                        FileIOPermission f3 = new FileIOPermission(FileIOPermissionAccess.Read, upload_path + id + "\\");
                        f3.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, upload_path + id + "\\");
                        try
                        {
                            f3.Demand();
                        }
                        catch (SecurityException s)
                        {
                            Console.WriteLine(s.Message);
                        }*/
                    }
                    //var path = Path.Combine(Server.MapPath("~/Upload/"), fileName);
                    var path = upload_path + id + "\\" + fileName;
                    file.SaveAs(path);
                    return RedirectToAction("Create",path);
                }
            }

            return View();
        }

        // GET: documents/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            document document = db.document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: documents/Create
        public ActionResult Create(string document_path)
        {
            ViewBag.path_document = document_path;
            ViewBag.utilisateur_id = GetIdByLogin(User.Identity.Name);
            ViewBag.type_document = "certificat médical";
            ViewBag.valide = 2;
            return View();
        }

        // POST: documents/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "document_id,utilisateur_id,type_document,path_document,valide")] document document)
        {
            if (ModelState.IsValid)
            {
                db.document.Add(document);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", document.utilisateur_id);
            return View(document);
        }

        // GET: documents/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            document document = db.document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", document.utilisateur_id);
            return View(document);
        }

        // POST: documents/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "document_id,utilisateur_id,type_document,path_document,valide")] document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.utilisateur_id = new SelectList(db.utilisateur, "utilisateur_id", "login", document.utilisateur_id);
            return View(document);
        }

        // GET: documents/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            document document = db.document.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            document document = db.document.Find(id);
            db.document.Remove(document);
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
