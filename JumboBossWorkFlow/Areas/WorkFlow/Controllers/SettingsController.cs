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
using _DbEntities.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Security.Cryptography;
using System.Text;
using _DbEntities.Repository.Concrete;
using _BusinessLayer.Helpers;
using _BusinessLayer;
using _DbEntities.Models.ViewModel;
using _BusinessLayer.Messages;

namespace JumboBossWorkFlow.Areas.WorkFlow.Controllers
{
    [Authorize, LogFilter]
    public class SettingsController : Controller
    {
        private ApplicationUserManager _userManager;

        public SettingsController()
        {
        }

        public SettingsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult AddUser()
        {
            AddUserViewModel model = new AddUserViewModel();
            UserRepository userRepository = new UserRepository();
            model.Me = userRepository.GetUserById(User.Identity.GetUserId());//Kullanıcının kendisi Çekildi
            model.Users = userRepository.GetUserListDependencyId(User.Identity.GetUserId());//Şube Bağlı üyeler çekildi
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(AddUserViewModel model, string userd, string psw)
        {
            UserRepository userRepository = new UserRepository();
            ApplicationUser user = new ApplicationUser();
            user = userRepository.GetUserById(userd);
            if (user != null)
            {
                if (UserManager.PasswordHasher.VerifyHashedPassword(userRepository.GetUserById(User.Identity.GetUserId()).PasswordHash, psw) != PasswordVerificationResult.Failed)
            {
              
             
                    if (user.userInfo.DependencyId == userRepository.GetUserById(User.Identity.GetUserId()).Id)
                    {
                        await UserManager.RemoveFromRoleAsync(user.Id, "Member");
                        userRepository.DeleteUser(user);
                    }
              
            }
            else
            {
                ViewBag.Errorpass = true;
            }
            }


            if (model.EMail != null)
            {
                UserRepository ur = new UserRepository();
                EmployeeInviteBusiness _eb = new EmployeeInviteBusiness();
                _BusinessLayer<EmployeeInvite> _layer = new _BusinessLayer<EmployeeInvite>();
                _layer = _eb.SendEmployeeInvite(ur.GetUserById(User.Identity.GetUserId()), model.EMail);
                foreach (var item in _layer.Info)
                {
                    if (item.InfoCode == InfoMessageCode.AddInviteSendSuccess)
                    {
                        ViewBag.result =item.InfoMessage;
                    }
                }
                foreach (var item in _layer.Errors)
                {
                    ViewBag.error = true;
                    ModelState.AddModelError("",item.Message);
                }

            }

         
            model.Me = userRepository.GetUserById(User.Identity.GetUserId());//Kullanıcının kendisi Çekildi
            model.Users = userRepository.GetUserListDependencyId(User.Identity.GetUserId());//Şube Bağlı üyeler çekildi

            return View(model);
        }

    }
}