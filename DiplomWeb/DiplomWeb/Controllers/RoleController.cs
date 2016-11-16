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
using Microsoft.AspNet.Identity.EntityFramework;

namespace DiplomWeb.Controllers
{
    public class Vigils2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vigils2
        public ActionResult Index()
        {
            var vigils = db.ApplicationRole.Include(r => r.Vigils);
           // var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
           // var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
           // var rM = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
           // // создаем две роли
           // var role1 = new IdentityRole { Name = "admiwn" };
           // var role2 = new IdentityRole { Name = "uswer" };
           //ApplicationRole role3 = new ApplicationRole("advert1");
           // rM.Create(role3);
           // // добавляем роли в бд
           // roleManager.Create(role1);
           // roleManager.Create(role2);

           // User.Identity.GetUserId();

           
            return View(vigils.ToList());
        }

        // GET: Vigils2/Details/5
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

        // GET: Vigils2/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationRoleID = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Vigils2/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ApplicationRoleID")] Vigil vigil)
        {
            if (ModelState.IsValid)
            {
                db.Vigils.Add(vigil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationRoleID = new SelectList(db.Roles, "Id", "Name", vigil.ApplicationRoleID);
            return View(vigil);
        }

        // GET: Vigils2/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ApplicationRoleID = new SelectList(db.Roles, "Id", "Name", vigil.ApplicationRoleID);
            return View(vigil);
        }

        // POST: Vigils2/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ApplicationRoleID")] Vigil vigil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vigil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationRoleID = new SelectList(db.Roles, "Id", "Name", vigil.ApplicationRoleID);
            return View(vigil);
        }

        // GET: Vigils2/Delete/5
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

        // POST: Vigils2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vigil vigil = db.Vigils.Find(id);
            db.Vigils.Remove(vigil);
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
