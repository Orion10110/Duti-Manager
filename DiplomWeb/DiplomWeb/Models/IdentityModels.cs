using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DiplomWeb.Models
{

    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }
        [Display(Name = "Оповещение по e-mail")]
        public bool EmailNotifications { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime DateBirth { get; set; }
        [JsonProperty("CountHolidayDays", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue("24")]
        [Display(Name = "Количество дней отпуска")]
        public int CountHolidayDays { get; set; }
        [Display(Name = "Фото")]
        public string ImageAvatar { get; set; }
        public ApplicationUser()
        {
            this.RecordVigils = new HashSet<RecordVigil>();
            this.Groups = new HashSet<Group>();
            this.Projects = new HashSet<Project>();
            this.WatcherOfProjects = new HashSet<TaskOfProject>();
            this.ForWhomTask = new HashSet<TaskOfProject>();
            this.FromWhomTask = new HashSet<TaskOfProject>();
            this.Holidays = new HashSet<Holiday>();
            this.Vigils = new HashSet<Vigil>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
        //public virtual ICollection<VigilGroups> VigilGroups { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public virtual ICollection<TaskOfProject> ForWhomTask { get; set; }

        public virtual ICollection<TaskOfProject> FromWhomTask { get; set; }

        public virtual ICollection<TaskOfProject> WatcherOfProjects { get; set; }
        public virtual ICollection<RecordVigil> RecordVigils { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Vigil> Vigils { get; set; }

    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() {
        }

        public ApplicationRole(string name) : base(name)
        {
        }

     

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Vigil> Vigils { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<TaskOfProject> TasksOfProject { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Holiday> Holidays { get; set; }
        
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


            modelBuilder.Entity<TaskOfProject>()
              .HasMany(c => c.Watchers)
             .WithMany(t => t.WatcherOfProjects);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}