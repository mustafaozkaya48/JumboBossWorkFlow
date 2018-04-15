using _BusinessLayer;
using _DbEntities.Models;
using _DbEntities.Models.ViewModel;
using _DbEntities.Repository.Concrete;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JumboBossWorkFlow.Areas.WorkFlow.Controllers
{
    [Authorize, LogFilter]
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
            AddWorkViewModel model = new AddWorkViewModel();
            UserRepository userRepository = new UserRepository();
            model.Me = userRepository.GetUserById(User.Identity.GetUserId());
            model.Users = userRepository.GetUserListDependencyId(User.Identity.GetUserId());//Üyeliğe bağlı alt üyeler çağırıldı
            return View(model);
        }
        [HttpPost]
        public ActionResult AddBusiness(AddWorkViewModel model, string EmployeeUsers, HttpPostedFileBase[] files)
        {
            _BusinessLayer<AddWorkViewModel> work_layer = new _BusinessLayer<AddWorkViewModel>();
            UserRepository userRepository = new UserRepository();
            if (ModelState.IsValid)
            {


                WorkBusiness workBusiness = new WorkBusiness();
                List<WorkAddition> additionlist = new List<WorkAddition>();
                work_layer = workBusiness.AddWorks(model, EmployeeUsers, User.Identity.GetUserId()); // iş eklendi geri alınıp atandı.
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        if (!Directory.Exists(HttpContext.Server.MapPath($"~/Content/UploadedFiles/{User.Identity.GetUserName()}")))
                            Directory.CreateDirectory(HttpContext.Server.MapPath($"~/Content/UploadedFiles/{User.Identity.GetUserName()}"));
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath($"~/Content/UploadedFiles/{User.Identity.GetUserName()}/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        additionlist.Add(new WorkAddition { Filename = InputFileName, FilePath = userRepository.GetUserById(EmployeeUsers).Email + "/" + InputFileName, Work_Id = work_layer.Result.Works.Id });



                    }
                }
                foreach (var item in work_layer.Info)
                {
                    if (item.InfoCode == _BusinessLayer.Messages.InfoMessageCode.AddWorkSuccess)
                    {
                        workBusiness.AddWorkAddition(additionlist);
                        ViewBag.result = true;
                    }
                }
                foreach (var item in work_layer.Errors)
                {

                    ModelState.AddModelError("", item.Message);
                    ViewBag.result = false;
                }
            }
            else
            {

                ViewBag.result = false;
            }
            model.Users = userRepository.GetUserListDependencyId(User.Identity.GetUserId());//Üyeliğe bağlı alt üyeler çağırıldı
            model.Me = userRepository.GetUserById(User.Identity.GetUserId());
            return View(model);
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
        public new async Task<ActionResult> Profile(EditProfileViewModel model, HttpPostedFileBase ProfilPictures)
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
                }
            }
            var applicationUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            applicationUser.PhoneNumber = model.PhoneNumber;
            applicationUser.userInfo.About = model.About;
            applicationUser.userInfo.Address = model.Address;
            applicationUser.userInfo.CompanyInfo = model.CompanyInfo;
            applicationUser.userInfo.ProfilPicture = model.ProfilPicture;
            applicationUser.userInfo.Name = model.Name;
            applicationUser.userInfo.SurName = model.SurName;
            await UserManager.UpdateAsync(applicationUser);
            ViewBag.result = true;
            return View(model);





        }



    }
}