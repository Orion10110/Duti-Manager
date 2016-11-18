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
        public string Title { get; set; }

        [Display(Name = "Описпние")]
        public string Description { get; set; }

        [Display(Name ="Начало")]
        public DateTime StartAt { get; set; }

        [Display(Name = "Конец")]
        public DateTime EndAt { get; set; }

        [Display(Name ="Целый день?")]
        public bool IsFullDay { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserID { get; set; }

        [Display(Name = "Пользователь")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? VigilID { get; set; }

        [Display(Name = "Дежурство")]
        public virtual Vigil Vigil { get; set; }

    }
}