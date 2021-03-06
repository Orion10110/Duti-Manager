﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiplomWeb.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Display(Name = "Группа")]
        public string Name { get; set; }        

        public Group()
        {
            this.ApplicationUsers = new List<ApplicationUser>();
            
        }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
        public virtual Project Project { get; set; }

    }
}