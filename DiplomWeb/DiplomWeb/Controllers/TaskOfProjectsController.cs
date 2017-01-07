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
using System.IO;
using Microsoft.AspNet.Identity.Owin;

namespace DiplomWeb.Controllers
{
    public class TaskOfProjectsController : Controller
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


        public ActionResult Tasks(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            List<TaskOfProject> tasks = db.Projects.Find(id).TasksOfProject.Where(p => p.ForWhomId == userId || p.FromWhomId == userId || p.Watchers.Contains(user)).ToList();
            return PartialView(tasks);
        }

        public ActionResult Index(int id)
        {
            TaskOfProject task = db.TasksOfProject.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Admin = false;
            if (UserManager.IsInRole(user.Id, "Admin") || task.ForWhom.UserName.Equals(user.UserName)||task.FromWhom.UserName.Equals(user.UserName))
            {

                ViewBag.Admin = true;
            }

            return View(task);
        }


        public ActionResult Overview(int id)
        {
            TaskOfProject task = db.TasksOfProject.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return PartialView(task);
        }


        // GET: TaskOfProjects
        public ActionResult List()
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
            ViewBag.ForWhomId = new SelectList(db.Users.OrderBy(s=>s.SecondName), "Id", "SecondName");
            List<ApplicationUser> users = db.Projects.Find(id).Groups.SelectMany(t => t.ApplicationUsers).ToList();
            ViewBag.Watchers = users.GroupBy(x => x.Id).Select(g => g.First()).ToList(); 
            return PartialView();
        }

        // POST: TaskOfProjects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "FromWhomId,ForWhomId,Name,Description,DateStart,DataFinal,Priority,ProjectId,TaskStatus")] TaskOfProject taskOfProject,List<string> users)
        {
            if (ModelState.IsValid)
            {
                List<ApplicationUser> watchers= db.Users.Where(u => users.Contains(u.Id)).ToList();
                taskOfProject.Watchers.AddRange(watchers);
                taskOfProject.FromWhomId = User.Identity.GetUserId();
                db.TasksOfProject.Add(taskOfProject);
                if (Request.Files != null)
                {
                    SaveFiles(Request.Files, taskOfProject);
                }
                    
                db.SaveChanges();
                return Json(new { message = "Задача добавлена" }, JsonRequestBehavior.DenyGet);
            }
          return Json(new {message="Ошибка, задача не создана"},JsonRequestBehavior.DenyGet);
        }

        public void SaveFiles(HttpFileCollectionBase files, TaskOfProject task)
        {
            foreach (string file in files)
            {
                HttpPostedFileBase item = Request.Files[file];
                if (item != null && item.ContentLength > 0)
                {
                    string prefix = Guid.NewGuid().ToString() + item.FileName;
                    FilesCRM fileSave = new FilesCRM() { Name = prefix, FileName = item.FileName };
                    task.FilesCRMs.Add(fileSave);
                    item.SaveAs(Server.MapPath("~/Files/" + prefix));
                }
            }


        

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
            ViewBag.IdProject = taskOfProject.ProjectId;
            ViewBag.ForWhomId = new SelectList(db.Users.OrderBy(s => s.SecondName), "Id", "SecondName");
            List<ApplicationUser> users = taskOfProject.Project.Groups.SelectMany(t => t.ApplicationUsers).ToList();
            
            ViewBag.Watchers = users.GroupBy(x => x.Id).Select(g => g.First()).ToList();
            return PartialView(taskOfProject);
        }

        // POST: TaskOfProjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ForWhomId,Name,Description,DateStart,DataFinal,Priority,TaskStatus")] TaskOfProject taskOfProject, List<string> users)
        {
            if (ModelState.IsValid)
            {

                TaskOfProject task = db.TasksOfProject.Find(taskOfProject.Id);
                task.Name = taskOfProject.Name;
                task.Description = taskOfProject.Description;
                task.ForWhomId = taskOfProject.ForWhomId;
                task.TaskStatus = taskOfProject.TaskStatus;
                task.Priority = taskOfProject.Priority;
                task.DateStart = taskOfProject.DateStart;
                task.DataFinal = taskOfProject.DataFinal;
                task.Watchers.Clear();
                task.Watchers.AddRange(db.Users.Where(u=>users.Contains(u.Id)));
                if (Request.Files != null)
                {
                    SaveFiles(Request.Files, task);
                }
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { message = "Задача изменена" }, JsonRequestBehavior.DenyGet);
            }
            return Json(new { message = "Ошибка, попробуйте снова" }, JsonRequestBehavior.DenyGet);
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
