using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class FilesCRM
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Имя файла")]
        public string FileName { get; set; }
        [Display(Name = "Проект")]
        public virtual Project Project {get;set;}
        [Display(Name = "Задача")]
        public virtual TaskOfProject TaskOfProject { get; set; }
    }
}