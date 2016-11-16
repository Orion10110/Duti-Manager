using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class RecordVigil
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }



        [Display(Name = "Название")]
        public string Name { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserID { get; set; }

        [Display(Name = "Название")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? VigilID { get; set; }

        [Display(Name = "Название")]
        public virtual Vigil Vigil { get; set; }

        [Display(Name = "Дата дежурства")]
        public DateTime DateVigil { get; set; }
        [Display(Name = "Примечание")]
        public string Note { get; set; }
    }
}