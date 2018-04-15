using _BusinessLayer;
using _BusinessLayer.Helpers;
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
            if (model.Me.userInfo.DependencyId!=null)
            {
                model.Users.Add(userRepository.GetUserById(model.Me.userInfo.DependencyId));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddBusiness(AddWorkViewModel model, string EmployeeUsers, HttpPostedFileBase[] files)
        {
            UserRepository userRepository = new UserRepository();
            _BusinessLayer<AddWorkViewModel> work_layer = new _BusinessLayer<AddWorkViewModel>();

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
                        var fileType = file.ContentType;
                        var ServerSavePath = Path.Combine(Server.MapPath($"~/Content/UploadedFiles/{User.Identity.GetUserName()}/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        additionlist.Add(new WorkAddition { FileType=fileType, Filename = InputFileName, FilePath = userRepository.GetUserById(EmployeeUsers).Email + "/" + InputFileName, Work_Id = work_layer.Result.Works.Id });



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
            WorkBusiness wb = new WorkBusiness();
            return View(wb.GetAllWorks(User.Identity.GetUserId()));
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
                    ProfilPicture = applicationUser.userInfo.ProfilPicture,
                    ConfirmEMail = applicationUser.EmailConfirmed,
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
        public async Task<ActionResult> SendEmailConfrimMail()
        {
            if (User.Identity.IsAuthenticated)
            {
                User_Business ub = new User_Business();
                ApplicationUser user = ub.GetUserById(User.Identity.GetUserId());
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(User.Identity.GetUserId());
                string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                string ActivateUri = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                string body = $"<h1>Merhaba {user.userInfo.Name + " " + user.userInfo.SurName}</h1> <br /><br /><h2>Hesabınızı aktifleştirmek için <a href='{ActivateUri}' target='_blank'>Tıklayınız</a></h2>.";
                MailHelper.SendMail(body, user.Email, "Jumbo Boss İş Takip Programı Hesap Aktifleştrime");
                TempData["PostMail"] = true;
                return RedirectToAction("Profile", "Panel",new {id=User.Identity.GetUserId()});
              
            }
            else return Redirect("~Error404.aspx");
        
         
        }

        public FileResult DownloadFile(string id)
        {
            WorkAdditionRepository rp = new WorkAdditionRepository();
            Guid guid = Guid.Parse(id.ToUpper());
            WorkAddition addition = rp.GetWorkById(guid);
            FilePathResult filePathResult = new FilePathResult(Server.MapPath("~/Content/UploadedFiles/" + addition.FilePath + ""), addition.FileType);
            filePathResult.FileDownloadName = $"{DateTime.Now.ToString()}_{addition.Filename}";
            return filePathResult;
        }



    }
}