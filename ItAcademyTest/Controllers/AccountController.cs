using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ItAcademyTest.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AspNetIdentityApp.Controllers
{
    public class AccountController : Controller
    {
        
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        public ActionResult Register()
        {
            return View();
        }




        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    // если создание прошло успешно, то добавляем роль пользователя
                    await UserManager.AddToRoleAsync(user.Id, "user");                   

                    return RedirectToAction("Login", "Account");
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




        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                    AuthenticationManager.SignOut();

                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;

            return View(model);
        }



        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Login");
        }



        [Authorize]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("LogOff", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }



        [Authorize]
        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);

            if (user != null)
            {
                EditModel model = new EditModel { Email = user.Email, OldPassword = "", NewPassword = "" };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }



        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;

                    IdentityResult result = await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("LogOff", "Account");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Что-то пошло не так");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                }
            }           

            return View(model);
        }



        [Authorize]
        public async Task<ActionResult> ShowInfo()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);

            if (user != null)
            {
                InfoViewModel model = new InfoViewModel { Name = user.UserName  , Email = user.Email };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

    }
}