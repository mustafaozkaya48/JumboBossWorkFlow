using _BusinessLayer;
using _DbEntities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JumboBossWorkFlow.Areas.WorkFlow.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public PanelController()
        {
        }

        public PanelController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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


        // GET: WorkFlow/Panel
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult WorkFlow()
        {
            return View();
        }
        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult WorkPlan()
        {
            return View();
        }

        public ActionResult AddBusiness()
        {
            return View();
        }

        public ActionResult MyWorks()
        {
            return View();
        }

        public ActionResult WaitingJobs()
        {
            return View();
        }

        public ActionResult CompletedJobs()
        {
            return View();
        }
        static EditProfileViewModel registerViewModel;
        public new ActionResult Profile(string id)
        {


            if (id != null)
            {
                User_Business ub = new User_Business();
                ApplicationUser applicationUser = ub.GetUserById(id);
                registerViewModel = new EditProfileViewModel
                {
                    Id = applicationUser.Id,
                    Email = applicationUser.Email,
                    UserName = applicationUser.UserName,
                    PhoneNumber = applicationUser.PhoneNumber,
                    Name = applicationUser.userInfo.Name,
                    About = applicationUser.userInfo.About,
                    SurName = applicationUser.userInfo.SurName,
                    Address = applicationUser.userInfo.Address,
                    CompanyInfo = applicationUser.userInfo.CompanyInfo,
                    ProfilPicture = applicationUser.userInfo.ProfilPicture
                };
                return View(registerViewModel);
            }
            else
            {
                return RedirectToAction("Home", "Panel");
            }
        }
        [HttpPost]
        public new ActionResult Profile(EditProfileViewModel model, HttpPostedFileBase ProfilPictures)
        {

            if (model != null)
            {
                if (ProfilPictures != null &&
                    (ProfilPictures.ContentType == "image/jpeg" ||
                    ProfilPictures.ContentType == "image/jpg" ||
                    ProfilPictures.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfilPictures.ContentType.Split('/')[1]}";
                    if (model.ProfilPicture != null)
                    {
                        if (model.ProfilPicture != "User.png")
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/images/" + model.ProfilPicture));
                        }

                    }
                    ProfilPictures.SaveAs(Server.MapPath($"~/Content/images/{filename}"));
                    model.ProfilPicture = filename;
                   
                    
                    return View(model);
                }
                if (model.Password == model.ConfirmPassword && model.Password != null)
                {

                   var result =  Membership.Provider.ChangePassword(model.Email,model.CurrentPassword,model.Password);
                 
                    if (result==true)
                    {
                        ViewBag.result = "true";
                    }
                
                }

            }
            return View(model);





        }


        
    }
}