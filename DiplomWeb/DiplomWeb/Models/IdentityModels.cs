using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace DiplomWeb.Models
{

    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {


        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public bool EmailNotifications { get; set; }

        public DateTime DateBirth { get; set; }

        public string ImageAvatar { get; set; }
        public ApplicationUser()
        {
            this.RecordVigils = new HashSet<RecordVigil>();
            this.Groups = new HashSet<Group>();
            this.Projects = new HashSet<Project>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<TaskOfProject> ForWhomTask { get; set; }

        public virtual ICollection<TaskOfProject> FromWhomTask { get; set; }
        public virtual ICollection<RecordVigil> RecordVigils { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
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

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskOfProject> TasksOfProject { get; set; }

        public DbSet<Group> Groups { get; set; }
        
        public DbSet<RecordVigil> RecordVigils { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskOfProject>()
               .HasRequired(c => c.ForWhom)
               .WithMany(t => t.ForWhomTask)
               .HasForeignKey(m => m.ForWhomId)
               .WillCascadeOnDelete(false);
            // Otherwise you might get a "cascade causes cycles" error

            modelBuilder.Entity<TaskOfProject>()
                .HasRequired(c => c.FromWhom)
               .WithMany(t => t.FromWhomTask)
               .HasForeignKey(m => m.FromWhomId)
               .WillCascadeOnDelete(false);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}