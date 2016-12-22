using DiplomWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Controllers
{
    public class HomeController : Controller
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
            if (User.Identity.IsAuthenticated)
            {
                string id = User.Identity.GetUserId();
                ApplicationUser user =db.Users.Find(id);
                List<Vigil> vig = user.VigilGroups.SelectMany(s => s.Vigils).ToList();
                //IEnumerable<String> list = UserManager.GetRoles(id);
                //List<Vigil> vig = db.ApplicationRole.Where(p => list.Contains(p.Name)).SelectMany(s => s.Vigils).ToList();
               
                //var user = db.Users.Find(id);
                List<ProjectInfo> projects = user.Groups.Select(t => new ProjectInfo
                { Id = t.Project.Id, Name = t.Project.Name }).ToList();
                

                if (UserManager.IsInRole(user.Id, "Admin"))
                {
                    projects = db.Projects.Select(t => new ProjectInfo
                    {
                        Id = t.Id,
                        Name = t.Name + " "
                    }).ToList();

                    vig = db.Vigils.ToList();
                }
               
                ViewBag.Vigil = vig;
                ViewBag.Projects = projects;
            }
          
            return View();
        }


        public ActionResult Java()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}