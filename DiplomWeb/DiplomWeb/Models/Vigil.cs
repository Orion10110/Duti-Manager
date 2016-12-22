﻿using System;
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
            this.VigilGroups = new HashSet<VigilGroups>();
        }
        [Display(Name = "Дни дежурства")]
        public string Days { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Группа")]

        public virtual ICollection<VigilGroups> VigilGroups { get; set; }

        public virtual ICollection<RecordVigil> RecordVigils { get; set; }
    }
}