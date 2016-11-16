using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DiplomWeb.Models
{

    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }

        public ApplicationUser()
        {
            this.RecordVigils = new HashSet<RecordVigil>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        public virtual ICollection<RecordVigil> RecordVigils { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() {
            this.Vigils = new HashSet<Vigil>();
        }

        public ApplicationRole(string name) : base(name)
        {
            this.Vigils = new HashSet<Vigil>();
        }

        public virtual ICollection<Vigil> Vigils { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Vigil> Vigils { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<RecordVigil> RecordVigils { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}