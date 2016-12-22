using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class Holiday
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата оконччания")]
        public DateTime DataFinal { get; set; }
        [DefaultValue(0)]
        [Display(Name = "Статус")]
        public HolidayStatus Status { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ApplicationUserId { get; set; }

        [Display(Name = "Работник")]
        public virtual ApplicationUser ApplicationUser { get; set; }
      
    }
}