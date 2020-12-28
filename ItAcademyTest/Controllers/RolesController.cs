using ItAcademyTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ItAcademyTest.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }


        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }


        [Authorize]
        public async Task<ActionResult> UsersRoles()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);

            if (user != null)
            {
               IList<string> _roleslist = await UserManager.GetRolesAsync(user.Id);

                return View(_roleslist);
            }
            return RedirectToAction("Login", "Account");
            
        }


        [Authorize]
        public async Task<ActionResult> BecomeAdmin() 
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            

            if (user != null && user.Email == "pilot_mig@bk.ru" )
            {
                await UserManager.AddToRoleAsync(user.Id, "admin");

                return RedirectToAction("UsersRoles", "Roles");
            }
            else 
            {
                return RedirectToAction("WrongEmailToBecomeAdmin", "Roles");
            }
        }


        [Authorize]
        public ActionResult WrongEmailToBecomeAdmin() 
        {
            return View();
        }




        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name,
                    Description = model.Description
                });

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }

            }
            return View(model);
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);

            if (role != null)
            {
                return View(new EditRoleModel { Id = role.Id, Name = role.Name, Description = role.Description });
            }

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    role.Description = model.Description;
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
            }
            return View(model);
        }



        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }


 

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }





        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddUserToRole(string Email, string rolename) 
        {
            
            ApplicationUser user = await UserManager.FindByEmailAsync(Email);

            if (user != null)
            {
                await UserManager.AddToRoleAsync(user.Id, rolename);
                return RedirectToAction("Info", "Admin");
            }
            else 
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index", "Admin");

        }



    }
}