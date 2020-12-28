using ItAcademyTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ItAcademyTest.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }


        [Authorize(Roles = "admin")]
        // GET: Admin
        public ActionResult Index()
        {

            return View(UserManager.Users);
        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Info(string Id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                IList<string> _rolelist = await UserManager.GetRolesAsync(user.Id);

                AdminInfo model = new AdminInfo { UserName = user.UserName, UserEmail = user.Email, UserRoleList = _rolelist };

                ViewBag.id = user.Id;

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string Id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                IList<string> _userrolelist = await UserManager.GetRolesAsync(user.Id);


                List<ApplicationRole> _appprolelist = RoleManager.Roles.ToList();

                IList<string> _availableroles = new List<string>();

                foreach (ApplicationRole r in _appprolelist)
                {
                    _availableroles.Add(r.Name);
                }


                AdminEdit model = new AdminEdit { UserName = user.UserName, UserEmail = user.Email, UserRoleList = _userrolelist, RoleList = _availableroles };

                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }




        [Authorize(Roles = "admin")]
        public async Task<ActionResult> AddRoleToUser(string Email, string Rolename)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(Email);

            if (user != null)
            {
                await UserManager.AddToRoleAsync(user.Id, Rolename);
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteRoleFromUser(string Email, string Rolename)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(Email);

            if (user != null)
            {
                await UserManager.RemoveFromRoleAsync(user.Id, Rolename);
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else
            {
                ModelState.AddModelError("", "Что-то пошло не так");
            }

            return RedirectToAction("Index");

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteUser() 
        {
            return View();        
        }


        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string Id) 
        {
            ApplicationUser user = await UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("CantDeleteUser", new { name=user.Email});

        }


        [Authorize(Roles = "admin")]
        public string CantDeleteUser(string name) 
        {
            return "Не могу удалить пользователя:  " + name + "  Что-то пошло не так!";
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create() 
        {
            return View();       
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create(RegisterModel model) 
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    // если создание прошло успешно, то добавляем роль пользователя
                    await UserManager.AddToRoleAsync(user.Id, "user");

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }



        [Authorize(Roles = "admin")]
        public async Task<ActionResult>  FindByEmail(string email) 
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return RedirectToAction("Edit", "Admin", new { Id = user.Id });
            }
            else 
            { 
                return RedirectToAction("NoUserEmail", "Admin");
            }
        }


        [Authorize(Roles = "admin")]
        public ActionResult NoUserEmail()
        {
            return View();        
        }
    }
}