using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomWeb.Models;

namespace DiplomWeb.Controllers
{
    public class Vigils1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vigils1
        public ActionResult Index()
        {
            var vigils = db.Users.Include(v=>v.RecordVigils);
            return View(vigils.ToList());
        }

        // GET: Vigils1/Details/5
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

        // GET: Vigils1/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationRoleID = new SelectList(db.Roles, "Id", "Name");
            return View();
        }

        // POST: Vigils1/Create
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

        // GET: Vigils1/Edit/5
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

        // POST: Vigils1/Edit/5
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

        // GET: Vigils1/Delete/5
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

        // POST: Vigils1/Delete/5
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
