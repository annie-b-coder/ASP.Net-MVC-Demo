﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Models.Identity;
using System.Models.Repository;
using System.Web.Security;
using System.Models.DTO;
using System.Collections.Generic;
using System.Net;
using System.Models.Work;

namespace System.Controllers
{
    public class AccountController : Controller
    {
        public UserRepository repo;

        public AccountController()
        {
            repo = new UserRepository();
        }


        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginUser());
        }

      
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Roles()
        {
            var roles = repo.GetRoles().Select(s => new ViewRole { Id = s.Id, Name = s.Name });
            return View(roles);
        }
        [HttpPost]
      
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Role_Create(string roleName)
        {
            if (roleName != "")
            {
                DTORole n = repo.GetRole(roleName);
                if (n == null)
                {
                    repo.CreateRole(roleName);
                }
            }
            return RedirectToAction("Roles");
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Models.Identity.LoginUser model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = repo.PasswordSignIn(model.UserName, model.Password);
            if (result)
            {
                DTOUser user = repo.GetUser(model.UserName);

                FormsAuthentication.SetAuthCookie(model.UserName, false);

                var appUrl = HttpRuntime.AppDomainAppVirtualPath;
                return RedirectToLocal(appUrl + "Manager/Index");
            }
            else
            {
                ModelState.AddModelError("", "Wrong password or login");
                return View(model);
            }

        }

        // POST: /Account/LogOff
        [HttpPost]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }
        
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult User_Create()
        {
            List<DTORole> roles = repo.GetRoles();

            ViewBag.Roles = roles.Select(x => new SelectListItem()
            {
                Selected = false,
                Text = x.Name,
                Value = x.Name
            }).ToList();
            return View(new CreateUser());
        }


        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult User_Create(CreateUser user)
        {
            DTOUser result = new DTOUser();

            List<DTORole> roles = new List<DTORole>();
            foreach (var role in user.Roles)
            {
                roles.Add(repo.GetRole(role));
            }

            if (ModelState.IsValid)
            {

                DTOUser nUser = repo.GetUser(user.UserName);
                if (nUser != null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                try
                {
                    result = repo.CreateUser(user, roles);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }

            }
            return RedirectToAction("Users");
        }

        [Authorize]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("About", "Home");
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Users()
        {
            List<DTOUser> users = repo.GetUsers();

            var viewUsers = users.Select(s => new ViewUser { Id = s.Id, UserName = s.UserName, Roles = s.Roles.Select(z => z.Name).ToList() });
            return View(viewUsers);
        }
    }
}