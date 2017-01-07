using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace DiplomWeb.Controllers
{
    public class HolidaysController : Controller
    {
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

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RecordVigils
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser appUser = db.Users.Find(id);
            appUser.RecordVigils.Where(r => r.StartAt >= DateTime.Now);
            appUser.Holidays.ToList();

            //db.Vigils.Include(v => v.ApplicationRole);
            //List<ApplicationRole> rl = db.ApplicationRole.Where(p => list.Contains(p.Name)).ToList();
            //List<Vigil> vigil = new List<Vigil>();
            //foreach (ApplicationRole ap in rl)
            //{
            //    vigil.AddRange(ap.Vigils.ToList());
            //}
            //List<RecordVigil> records = new List<RecordVigil>();
            //foreach (Vigil vg in vigil)
            //{

            //    records.AddRange(vg.RecordVigils);
            //}
            //ViewBag.VigilId = new SelectList(vigil, "Id", "Name");

            //    original.Any(p => otherPeople.Contains(p));
            return View();
        }

        public ActionResult Vigil()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser appUser = db.Users.Find(id);
            List<Holiday> holidays = appUser.Holidays.ToList();
            List<Object> record = new List<object>();
            foreach (var item in holidays)
            {
                bool editable = false;
                if (item.Status == HolidayStatus.новый)
                {
                    editable = true;
                }
                record.Add(new
                {
                    title = item.Status.ToString(),
                    id = item.Id,
                    start = item.DateStart.AddDays(1).ToUniversalTime(),
                    end = item.DataFinal.AddDays(1).ToUniversalTime(),
                    allDay = true,
                    editable = editable
                });
            }

            ViewBag.CountHolidays = appUser.CountHolidayDays;
            return View();
        }


        //public ActionResult Sheduler(int id)
        //   {
        //       //string id = User.Identity.GetUserId();
        //       //ApplicationUser applicationUser = db.Users.Find(id);
        //       //var role = db.Roles.Where(d=>d.)
        //       //        ////IEnumerable<Vigil> listVigil = db.Vigils.Include(v => v.ApplicationRole).
        //       //        dp.events.list = [
        //       //{
        //       //        start: "2013-03-25T00:00:00",
        //       //  end: "2013-03-27T00:00:00",
        //       //  id: "1",
        //       //  resource: "A",
        //       //  text: "Event 1"
        //       //},
        //       //{
        //       //        start: "2013-03-26T12:00:00",
        //       //  end: "2013-03-27T00:00:00",
        //       //  id: "2",
        //       //  resource: "B",
        //       //  text: "Event 2"
        //       //}
        //       //];
        //       Vigil vigil = db.Vigils.Find(id);
        //       List<RecordVigil> rv = vigil.RecordVigils.ToList();
        //       List<object> events = new List<object>();
        //       List<object> listUser = new List<object>();
        //       List<ApplicationUser> users = db.Users.Where(u => u.Roles.Any(r => r.RoleId == vigil.ApplicationRoleID)).ToList();
        //       foreach (ApplicationUser item in users)
        //       {
        //           listUser.Add(new { name = item.SecondName + " " + item.FirstName, id = item.Id });
        //       }
        //       foreach (RecordVigil item in rv)
        //       {


        //           events.Add(new
        //           {
        //               start = item.StartAt.AddDays(1).ToUniversalTime(),
        //               end = item.EndAt.ToUniversalTime(),
        //               resource = item.ApplicationUserID,
        //               text = "",
        //               moveDisabled = true,
        //               resizeDisabled = true
        //           });
        //       }
        //       ViewBag.Events = events;
        //       ViewBag.Users = listUser;
        //       ViewBag.Name = vigil.Name;
        //       //    original.Any(p => otherPeople.Contains(p));
        //       return View();
        //   }


        [HttpPost]
        public JsonResult SaveDate([Bind(Include = "DateStart,DataFinal")] Holiday holiday)
        //(DateTime startAt, DateTime endAt, string title, string color, int vigilId)
        {
            string userId = User.Identity.GetUserId();
            //  RecordVigil recordVigil = new RecordVigil() { ApplicationUserID = userId, EndAt = endAt, StartAt = startAt, Title = title, VigilID = vigilId, Color = color, Description = description };
            holiday.ApplicationUserId = userId;
            db.Holidays.Add(holiday);
            db.SaveChanges();
            return new JsonResult { Data = new { Id = holiday.Id, start = holiday.DateStart, end = holiday.DataFinal, title = holiday.Status.ToString()}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        public JsonResult UpdateDate(int id, DateTime startAt, DateTime endAt)
         {
            Holiday holid = db.Holidays.Find(id);
            holid.DateStart = startAt;
            holid.DataFinal= endAt;
            db.Entry(holid).State = EntityState.Modified;
            db.SaveChanges();
            return new JsonResult { Data = new { start = startAt, end = endAt }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        //[HttpPost]
        //public JsonResult ChangeEvent(int id, string title, string color, string description)
        //{
        //    RecordVigil recordVigil = db.RecordVigils.Find(id);
        //    recordVigil.Title = title;
        //    recordVigil.Description = description;
        //    recordVigil.Color = color;
        //    db.Entry(recordVigil).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return new JsonResult { Data = new { title = title, color = color, id = id, description = description }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}



        [HttpPost]
        public JsonResult DeleteEvent(int id)
        {
            Holiday holid= db.Holidays.Find(id);
            db.Holidays.Remove(holid);
            db.SaveChanges();
            return new JsonResult { Data = new { id = id }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // UserVigils/GetEvents
        public JsonResult GetEvents()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser appUser = db.Users.Find(id);
            List<Holiday> holidays = appUser.Holidays.ToList();
            List<Object> record = new List<object>();
            foreach (var item in holidays)
            {
                bool editable = false;
                if (item.Status == HolidayStatus.новый)
                {
                    editable = true;
                }
                record.Add(new
                {
                    title = item.Status.ToString(),
                    id = item.Id,
                    start = item.DateStart,
                    end = item.DataFinal,
                    allDay = true,
                    editable = editable
                });
            }

            return new JsonResult { Data = record, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
        // GET: Holidays
        public ActionResult List()
        {
            return View(db.Holidays.ToList());
        }

        // GET: Holidays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // GET: Holidays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Holidays/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateStart,DataFinal,Status")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.Holidays.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(holiday);
        }

        // GET: Holidays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: Holidays/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateStart,DataFinal,Status,ApplicationUserId")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holiday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(holiday);
        }

        // GET: Holidays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Holiday holiday = db.Holidays.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            return View(holiday);
        }

        // POST: Holidays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Holiday holiday = db.Holidays.Find(id);
            db.Holidays.Remove(holiday);
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
