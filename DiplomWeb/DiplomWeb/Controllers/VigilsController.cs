using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DiplomWeb.Controllers
{
    public class VigilsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {

            ApplicationUser userApp = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Record= userApp.RecordVigils.Where(r => r.StartAt >= DateTime.Now).ToList();
            List<Vigil> vig = userApp.Vigils.ToList();
            ViewBag.Admin = false;
            if (UserManager.IsInRole(userApp.Id, "Admin"))
            {
                ViewBag.Admin = true;
                vig = db.Vigils.ToList();
            }
            return View(vig);
        }



        // GET: Vigils
        public ActionResult List()
        {
            return View(db.Vigils.ToList());
        }

        // GET: Vigils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vigil vigil = db.Vigils.Find(id);
            if (vigil == null)
            {
                return HttpNotFound();
            }
            return View(vigil);
        }

        // GET: Vigils/Create
        public ActionResult Create()
        {
            ViewBag.Users = db.Users.ToList();
            return View();
        }

        // POST: Vigils/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Vigil vigil, IEnumerable<string> days, IEnumerable<string> users)
        {
            if (ModelState.IsValid)
            {
                string d = String.Join(",", days.Where(s=>s!="false"));
                vigil.Days = d;
                List<ApplicationUser> us = db.Users.Where(j => users.Contains(j.Id)).ToList();
                if (us.Count > 0)
                {
                    vigil.ApplicationUsers.AddRange(us);
                }
                db.Vigils.Add(vigil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vigil);
        }

        // GET: Vigils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vigil vigil = db.Vigils.Find(id);
            List<ApplicationUser> aplic = vigil.ApplicationUsers.ToList();
            ViewBag.SetUser = aplic;
            List<ApplicationUser> users = db.Users.OrderBy(s => s.SecondName).ToList();
            ViewBag.Users = users.OrderBy(s => !aplic.Contains(s)).ToList();
            
            if (vigil == null)
            {
                return HttpNotFound();
            }
            return PartialView(vigil);
        }

        // POST: Vigils/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Vigil vigil, IEnumerable<string> days, IEnumerable<string> users)
        {
            if (ModelState.IsValid)
            {
                Vigil vig = db.Vigils.Find(vigil.Id);
                vig.ApplicationUsers.Clear();
                List<ApplicationUser> us = db.Users.Where(j => users.Contains(j.Id)).ToList();
                if (us.Count > 0)
                {
                    vig.ApplicationUsers.AddRange(us);
                }
                string d = String.Join(",", days.Where(s => s != "false"));
                vig.Days = d;
                vig.Name = vigil.Name;
                db.Entry(vig).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { message = "Дежурство изменено" }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { message = "Ошибка, попробуйте снова" }, JsonRequestBehavior.DenyGet);


    }

    // GET: Vigils/Delete/5
    public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vigil vigil = db.Vigils.Find(id);
            if (vigil == null)
            {
                return HttpNotFound();
            }
            return View(vigil);
        }

        // POST: Vigils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vigil vigil = db.Vigils.Find(id);
            db.Vigils.Remove(vigil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Members(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vigil vigil= db.Vigils.Find(id);

            if (vigil == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = db.Users.ToList();
            ViewBag.VigilId = id;
            return View();
        }
        [HttpPost, ActionName("Members")]
        public ActionResult Members(IEnumerable<string> users, int id)
        {
            Vigil vigil = db.Vigils.Find(id);

            List<ApplicationUser> us = db.Users.Where(j => users.Contains(j.Id)).ToList();
            if (us.Count > 0)
            {
                vigil.ApplicationUsers.AddRange(us);
                db.Entry(vigil).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        public ActionResult VigilMembers(int id)
        {
            Vigil vg = db.Vigils.Find(id);
            return PartialView(vg.ApplicationUsers.ToList());
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
