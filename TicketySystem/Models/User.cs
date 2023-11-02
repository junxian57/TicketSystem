using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TicketySystem.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }

        public enum Role
        {
            [Display(Name = "QA")]
            QA=1,
            [Display(Name = "RD")]
            RD=2,
            [Display(Name = "PM")]
            PM=3,
            [Display(Name = "Administrator")]
            Administrator=4
        }
    }
}