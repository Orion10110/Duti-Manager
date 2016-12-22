using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiplomWeb.Models
{
    public class VigilGroups
    {

        public int Id { get; set; }

        public string Name { get; set; }


        public VigilGroups()
        {
            this.ApplicationUsers = new List<ApplicationUser>();
            this.Vigils = new List<Vigil>();
        }

        public virtual List<ApplicationUser> ApplicationUsers { get; set; }
        public virtual List<Vigil> Vigils { get; set; }
    }
}