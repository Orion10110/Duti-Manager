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

   

    public class ProjectsController : Controller
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

        // GET: Projects
        public ActionResult AllProjects()
        {
         
            return View(db.Projects.Include(a =>a.ApplicationUser).Where(v=>v.Delted==false).ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        public ActionResult Index()
        {

            var user= db.Users.Find(User.Identity.GetUserId());
            List<ProjectInfo> projects =user.Groups.Select(t => new ProjectInfo
              {Id =t.Project.Id,Name = t.Project.Name}).ToList();
            if (UserManager.IsInRole(user.Id, "Admin"))
            {
                projects = db.Projects.Select(t=>new ProjectInfo {
                    Id =t.Id,Name=t.Name+" "}).ToList();
            }
            return View(projects);
        }


        public ActionResult Overview(int id)
        {
            Project project = db.Projects.Find(id);
            if(project == null)
            {
                return HttpNotFound();
            }
            return PartialView(project);
        }

        public ActionResult Project(int id)
        {
            
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(User.Identity.GetUserId());
            ViewBag.Admin = false;
            if (UserManager.IsInRole(user.Id, "Admin") || project.ApplicationUser.UserName.Equals(user.UserName))
            {
              
                ViewBag.Admin = true;
            }

            return View("Project",project);
        }

        public ActionResult Manager()
        {
            return View(db.Projects.ToList());
        }

        public ActionResult Members(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
          
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = db.Users.ToList();
            ViewBag.ProjectId = id;
            return View(project.Groups);
        }
        [HttpPost, ActionName("Members")]
        public ActionResult Members(IEnumerable<string> users, IEnumerable<string> groups, int id)
        {
            Project project = db.Projects.Find(id);

            List<ApplicationUser> us = db.Users.Where(j => users.Contains(j.Id)).ToList();
            if (us.Count > 0)
            {
                project.Groups.Where(p => groups.Contains(p.Id.ToString())).ToList().ForEach(u => u.ApplicationUsers.AddRange(us));
            }
            return View();
        }
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,DateStart,DataFinal,Priority,Group")] Project project,List<string> groups, IEnumerable<HttpPostedFileBase> files=null)
        {
             project.ApplicationUser=db.Users.Find( User.Identity.GetUserId());

            if (ModelState.IsValid)
            {

                foreach (var item in groups)
                {
                    if (!String.IsNullOrEmpty(item.ToString())) {
                        project.Groups.Add(new Group { Name = item });
                    }
                }
                project.Groups.Add(new Group()
                {
                    Name = "Управляющий",
                    ApplicationUsers = new List<ApplicationUser>() { db.Users.Find(User.Identity.GetUserId()) }
                });    
              
                SaveFiles(files,project);
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }


        public void SaveFiles(IEnumerable<HttpPostedFileBase> files,Project project)
        {
           
                foreach (HttpPostedFileBase item in files)
                {
                    if (item != null){
                        string prefix = Guid.NewGuid().ToString() + item.FileName;
                        FilesCRM file = new FilesCRM() { Name = prefix, FileName = item.FileName };
                        project.FilesCRMs.Add(file);
                        item.SaveAs(Server.MapPath("~/Files/" + prefix));
                    }
                }
            
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,DateStart,DataFinal,Priority")] Project project)
        {
            project.ApplicationUser = db.Users.Find(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            project.Delted = true;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult MembersProject(int id)
        {
            Project pr=  db.Projects.Find(id);
            return PartialView(pr.Groups.ToList());
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
