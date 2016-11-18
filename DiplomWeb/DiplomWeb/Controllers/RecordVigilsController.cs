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
    public class RecordVigilsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecordVigils
        public ActionResult Index()
        {
            var recordVigils = db.RecordVigils.Include(r => r.ApplicationUser).Include(r => r.Vigil);
            return View(recordVigils.ToList());
        }

        // GET: RecordVigils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            if (recordVigil == null)
            {
                return HttpNotFound();
            }
            return View(recordVigil);
        }

        public ActionResult ViewCalendar()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveDate(DateTime startAt, DateTime endAt, string title)
        {
            
            return new JsonResult { Data = new {start= startAt, end = endAt, title = title }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult UpdateDate(DateTime startAt, DateTime endAt, string title)
        {

            return new JsonResult { Data = new { start = startAt, end = endAt, title = title}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetEvents()
        {
            //var v = db.RecordVigils.OrderBy(a => a.StartAt).ToList();
            List<RecordVigil> v = new List<RecordVigil>();
            v.Add(new RecordVigil { Title = "title", Description = "asdfas", StartAt = new DateTime(2016, 11, 16),EndAt = new DateTime(2016, 11, 18), IsFullDay = false });
            v.Add(new RecordVigil { Title = "title", Description = "asdfas", StartAt = new DateTime(2016, 11, 17), EndAt = new DateTime(2016, 11, 17), IsFullDay = true });

            return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        // GET: RecordVigils/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email");
            ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name");
            return View();
        }

        // POST: RecordVigils/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ApplicationUserID,VigilID,DateVigil,Note")] RecordVigil recordVigil)
        {
            if (ModelState.IsValid)
            {
                db.RecordVigils.Add(recordVigil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", recordVigil.ApplicationUserID);
            ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name", recordVigil.VigilID);
            return View(recordVigil);
        }

        // GET: RecordVigils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            if (recordVigil == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", recordVigil.ApplicationUserID);
            ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name", recordVigil.VigilID);
            return View(recordVigil);
        }

        // POST: RecordVigils/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ApplicationUserID,VigilID,DateVigil,Note")] RecordVigil recordVigil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recordVigil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserID = new SelectList(db.Users, "Id", "Email", recordVigil.ApplicationUserID);
            ViewBag.VigilID = new SelectList(db.Vigils, "Id", "Name", recordVigil.VigilID);
            return View(recordVigil);
        }

        // GET: RecordVigils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            if (recordVigil == null)
            {
                return HttpNotFound();
            }
            return View(recordVigil);
        }

        // POST: RecordVigils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RecordVigil recordVigil = db.RecordVigils.Find(id);
            db.RecordVigils.Remove(recordVigil);
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
