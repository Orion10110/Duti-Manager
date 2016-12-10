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
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace DiplomWeb.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
            private ApplicationRoleManager _roleManager;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        //ApplicationRoleManager


        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
           IEnumerable<String> list = UserManager.GetRoles(id);
            ViewBag.Roles = list;
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {

                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Number,DateBirth = model.DateBirth};
                var result = await UserManager.CreateAsync(user, model.Password);


                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: ApplicationUsers/ChooseRole/5
        public ActionResult AddRole(string userName)
        {

            if (userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.UserId = userName;
           
            return View(db.ApplicationRole.ToList());
        }
        public ActionResult DeleteRolle(string role, string userName)
        {
            if (String.IsNullOrEmpty(role) || userName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           ApplicationUser user= UserManager.FindByName(userName);
            UserManager.RemoveFromRole(user.Id, role);
            //ApplicationRole role= RoleManager.FindById(roleId);

            return RedirectToAction("Details", new { id = user.Id });
        }

        
        // GET: ApplicationUsers/AddRole/5
        public ActionResult ChoosedRole(string role, string userId)
        {
            if (String.IsNullOrEmpty(role)|| userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.FindByName(userId);
            UserManager.AddToRole(user.Id,role);
           //ApplicationRole role= RoleManager.FindById(roleId);

            return RedirectToAction("Details", new { id = user.Id });
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            var model = new ChangedViewModel {Id=applicationUser.Id, UserName = applicationUser.UserName, Email = applicationUser.Email, Number = applicationUser.PhoneNumber};
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: ApplicationUsers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChangedViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = db.Users.Find(model.Id);
                appUser.UserName = model.UserName;
                if (!String.IsNullOrEmpty(model.Password))
                {
                    appUser.PasswordHash = UserManager.getHasherPassword(model.Password);
                }      
                appUser.PhoneNumber = model.Number;
                appUser.Email = model.Email;
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
