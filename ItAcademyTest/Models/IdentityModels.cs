using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ItAcademyTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ItAcademyTest.Models
{
    public class ApplicationUser : IdentityUser
    {        
        public ApplicationUser()
        {
        }
    }

    public class ApplicationContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationContext() : base("IdentityDb_v4") { }

        public static ApplicationContext Create() 
        {
            return new ApplicationContext();      
        
        }   
    
    }


    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store)
        {
        }
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }

    /*
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {      

            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            // создаем две роли
            var role1 = new ApplicationRole { Name = "admin", Description="God of this App=)" };
            var role2 = new ApplicationRole { Name = "user", Description="ordinary user without any rights=)" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            base.Seed(context);
        }
        
    } 
    */


}