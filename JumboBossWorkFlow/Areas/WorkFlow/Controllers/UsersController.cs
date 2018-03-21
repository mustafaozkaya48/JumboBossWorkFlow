using _DataBaseLayer;
using _DataBaseLayer.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JumboBossWorkFlow.Areas.WorkFlow.Controllers
{
    public class UsersController : Controller
    {
        // GET: WorkFlow/Users
        public ActionResult Login()
        {
           
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Login(RegisterModel model)
        {
            return View();

        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            MembershipCreateStatus membershipCreateStatus;
            try
            {
                Membership.CreateUser(model.Name, model.Password, model.PhoneNumber, "soru", "cevap", true, out membershipCreateStatus);
                FormsAuthentication.SetAuthCookie(model.EMail, false);
                if (membershipCreateStatus == MembershipCreateStatus.Success)
                {
                    return RedirectToAction("ListUser");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(null, ex);
            }
            return View(model);
        }

    }
}