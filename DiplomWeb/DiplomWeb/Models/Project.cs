using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomWeb.Models
{
    public class Project
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        [AllowHtml]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата начала")]
        public DateTime? DateStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата выполнения")]
        public DateTime? DataFinal { get; set; }
        [Display(Name = "Приоритет")]
        public Priority Priority { get; set; }
        [Display(Name = "Создал")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Display(Name = "Удален")]
        [DefaultValue("false")]
        public bool Delted { get; set; }

        public Project()
        {
            this.Groups = new HashSet<Group>();
            this.FilesCRMs = new HashSet<FilesCRM>();
            this.TasksOfProject = new HashSet<TaskOfProject>();
        }
        public virtual ICollection<Group> Groups { get; set; } 
        public virtual ICollection<TaskOfProject> TasksOfProject { get; set; }
        [Display(Name = "Файлы")]
        public virtual ICollection<FilesCRM> FilesCRMs { get; set; }
    }


    public class ProjectInfo
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
       
    }
}