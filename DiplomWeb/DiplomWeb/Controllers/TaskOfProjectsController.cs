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

namespace DiplomWeb.Controllers
{
    public class TaskOfProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Tasks(int id)
        {
            var userId = User.Identity.GetUserId();

            List<TaskOfProject> tasks = db.Projects.Find(id).TasksOfProject.Where(p => p.ForWhomId == userId || p.FromWhomId == userId).ToList();
            return PartialView(tasks);
        }





        // GET: TaskOfProjects
        public ActionResult Index()
        {
            var tasksOfProject = db.TasksOfProject.Include(t => t.ForWhom).Include(t => t.FromWhom);
            return View(tasksOfProject.ToList());
        }

        // GET: TaskOfProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOfProject taskOfProject = db.TasksOfProject.Find(id);
            if (taskOfProject == null)
            {
                return HttpNotFound();
            }
            return View(taskOfProject);
        }

        // GET: TaskOfProjects/Create
        public ActionResult Create(int id)
        {
            ViewBag.IdProject= id;
            ViewBag.ForWhomId = new SelectList(db.Users, "Id", "SecondName");
            List<ApplicationUser> users = db.Projects.Find(id).Groups.SelectMany(t => t.ApplicationUsers).ToList();
            ViewBag.Watchers = users;
            return PartialView();
        }

        // POST: TaskOfProjects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "id,FromWhomId,ForWhomId,Name,Description,ProjectId,TaskStatus")] TaskOfProject taskOfProject,List<string> users, IEnumerable<HttpPostedFileBase> files = null)
        {
            if (ModelState.IsValid)
            {
                List<ApplicationUser> watchers= db.Users.Where(u => users.Contains(u.Id)).ToList();
                taskOfProject.Watchers.AddRange(watchers);
                taskOfProject.FromWhomId = User.Identity.GetUserId();
                db.TasksOfProject.Add(taskOfProject);
                SaveFiles(files, taskOfProject);
                db.SaveChanges();
                return Json(new { message = "Задача добавлена" }, JsonRequestBehavior.DenyGet);
            }
          return Json(new {message="Ошибка, задача не создана"},JsonRequestBehavior.DenyGet);
        }



        public void SaveFiles(IEnumerable<HttpPostedFileBase> files, TaskOfProject task)
        {

            foreach (HttpPostedFileBase item in files)
            {
                if (item != null)
                {
                    string prefix = Guid.NewGuid().ToString() + item.FileName;
                    FilesCRM file = new FilesCRM() { Name = prefix, FileName = item.FileName };
                    task.FilesCRMs.Add(file);
                    item.SaveAs(Server.MapPath("~/Files/" + prefix));
                }
            }

        }




        // GET: TaskOfProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOfProject taskOfProject = db.TasksOfProject.Find(id);
            if (taskOfProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ForWhomId = new SelectList(db.Users, "Id", "FirstName", taskOfProject.ForWhomId);
            ViewBag.FromWhomId = new SelectList(db.Users, "Id", "FirstName", taskOfProject.FromWhomId);
            return View(taskOfProject);
        }

        // POST: TaskOfProjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FromWhomId,ForWhomId,Name,Description")] TaskOfProject taskOfProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskOfProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ForWhomId = new SelectList(db.Users, "Id", "FirstName", taskOfProject.ForWhomId);
            ViewBag.FromWhomId = new SelectList(db.Users, "Id", "FirstName", taskOfProject.FromWhomId);
            return View(taskOfProject);
        }

        // GET: TaskOfProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskOfProject taskOfProject = db.TasksOfProject.Find(id);
            if (taskOfProject == null)
            {
                return HttpNotFound();
            }
            return View(taskOfProject);
        }

        // POST: TaskOfProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskOfProject taskOfProject = db.TasksOfProject.Find(id);
            db.TasksOfProject.Remove(taskOfProject);
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
