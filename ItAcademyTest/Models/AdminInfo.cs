using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ItAcademyTest.Models
{
    public class AdminInfo
    {
        [Display(Name = "Имя")]
        public string UserName { set; get; }


        [Display(Name = "Email")]
        public string UserEmail { set; get; }


        [Display(Name = "Роли пользователя")]
        public IList<string> UserRoleList { set; get; }


        public AdminInfo() 
        {
            UserRoleList = new List<string>();       
        }
    }
}