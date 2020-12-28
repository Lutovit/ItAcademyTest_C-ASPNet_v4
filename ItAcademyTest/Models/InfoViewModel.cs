using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItAcademyTest.Models
{
    public class InfoViewModel
    {
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}