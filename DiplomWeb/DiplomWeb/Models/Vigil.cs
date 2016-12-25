using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class Vigil
    {
        public Vigil()
        {
            this.RecordVigils = new HashSet<RecordVigil>();
            this.ApplicationUsers = new List<ApplicationUser>();
            this.Days = "1,2,3,4,5,6,7";
            //this.VigilGroups = new HashSet<VigilGroups>();
        }
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Дни дежурства")]
        public string Days { get; set; }

        

        [Display(Name = "Название")]
        public string Name { get; set; }

        //[Display(Name = "Группа")]

        //public virtual ICollection<VigilGroups> VigilGroups { get; set; }
        [Display(Name="Дежурные")]
        public virtual List<ApplicationUser> ApplicationUsers { get; set; }

        [Display(Name = "Записи")]
        public virtual ICollection<RecordVigil> RecordVigils { get; set; }
    }
}