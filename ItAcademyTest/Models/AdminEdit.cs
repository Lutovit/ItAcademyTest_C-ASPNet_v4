using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ItAcademyTest.Models
{
    public class AdminEdit
    {
        [DisplayName("Имя")]
        public string UserName { set; get; }

        [DisplayName("Email")]
        public string UserEmail { set; get; }

        [DisplayName("Роли пользователя")]
        public IList<string> UserRoleList { set; get; }

        [DisplayName("Доступные роли")]
        public IList<string> RoleList { set; get; }

        public AdminEdit()
        {
            UserRoleList = new List<string>();
            RoleList = new List<string>();
        }
    }
}