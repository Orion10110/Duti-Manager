using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class TaskOfProject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string FromWhomId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ForWhomId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int ProjectId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime? DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата выполнения")]
        public DateTime? DataFinal { get; set; }
        [Display(Name = "Приоритет")]
        public Priority Priority { get; set; }
        [Display(Name = "Статус")]
        public TaskStatus TaskStatus { get; set; }

        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }

        public TaskOfProject()
        {
            this.FilesCRMs = new HashSet<FilesCRM>();
            this.Watchers = new List<ApplicationUser>();
        }
        [Display(Name = "От кого")]
        public virtual ApplicationUser FromWhom { get; set; }
        [Display(Name = "Назначена")]
        public virtual ApplicationUser ForWhom { get; set; }
        [Display(Name = "Прокект")]
        public virtual Project Project { get; set; }
        [Display(Name = "Файлы")]
        public virtual ICollection<FilesCRM> FilesCRMs { get; set; }
        [Display(Name = "Наблюдатели")]
        public virtual List<ApplicationUser> Watchers { get; set; }

    }
}