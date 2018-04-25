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
            WorkFlowBusiness wfb = new WorkFlowBusiness();

            return View(wfb.GetList(UserManager.FindById(User.Identity.GetUserId())));
        }
        [HttpPost]
        public ActionResult Home(Guid id, string msg)
        {
            WorkFlowBusiness wfb = new WorkFlowBusiness();
            wfb.AddCommend(id,msg,User.Identity.GetUserId());



            return View(wfb.GetList(UserManager.FindById(User.Identity.GetUserId())));
        }
        public ActionResult WorkFlow()
        {
            WorkBusiness wb = new WorkBusiness();
            List<WorkViewModel> model = new List<WorkViewModel>();
            model = wb.GetListItemsWorkFlow(User.Identity.GetUserId());
            return View(model);
        }
        public ActionResult Calendar()
        {

            return View();
        }
        public JsonResult GetWorkList()
        {
            WorkBusiness wb = new WorkBusiness();
            List<Work> list = wb.GetAllWorks(User.Identity.GetUserId());
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WorkPlan()
        {

            return View();
        }
        public ActionResult AddWork()
        {
            WorkViewModel model = new WorkViewModel();
            UserRepository userRepository = new UserRepository();
            model.Me = userRepository.GetUserById(User.Identity.GetUserId());
            model.Users = userRepository.GetUserListDependencyId(User.Identity.GetUserId());//Üyeliğe bağlı alt üyeler çağırıldı
            if (model.Me.userInfo.DependencyId != null)
            {
                model.Users.Add(userRepository.GetUserById(model.Me.userInfo.DependencyId));
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult AddWork(WorkViewModel model, string EmployeeUsers, HttpPostedFileBase[] files)
        {
            UserRepository userRepository = new UserRepository();
            _BusinessLayer<WorkViewModel> work_layer = new _BusinessLayer<WorkViewModel>();
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
                        var fileSize = file.ContentLength;
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        additionlist.Add(new WorkAddition { FileType = fileType, Filename = InputFileName, FilePath = userRepository.GetUserById(EmployeeUsers).Email + "/" + InputFileName, Work_Id = work_layer.Result.Works.Id, FileSize = fileSize.ToString() });



                    }
                }
                foreach (var item in work_layer.Info)
                {
                    if (item.InfoCode == _BusinessLayer.Messages.InfoMessageCode.AddWorkSuccess)
                    {
                        workBusiness.AddWorkAddition(additionlist);
                        ViewBag.result = true;
                        WorkFlowBusiness wfb = new WorkFlowBusiness();
                        string dependencyId;

                        ApplicationUser user = userRepository.GetUserById(User.Identity.GetUserId());
                        if (user.userInfo.DependencyId != null) dependencyId = user.userInfo.DependencyId;
                        else dependencyId = user.Id;
                        wfb.AddWorkflow(new Workflow { EmployeeUsersId = work_layer.Result.EmployeeUsers, LikeCount = 0, RequestingUserId = User.Identity.GetUserId(), Work_Id = work_layer.Result.Works.Id, DependencyId = dependencyId });
                        return RedirectToAction("MyWorks");
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
            if (model.Me.userInfo.DependencyId != null)
            {
                model.Users.Add(userRepository.GetUserById(model.Me.userInfo.DependencyId));
            }
            return View(model);
        }
        public ActionResult MyWorks()
        {
            WorkBusiness wb = new WorkBusiness();
            return View(wb.GetPlannedWorksList(User.Identity.GetUserId()));
        }
        public ActionResult JobCompleted(string id, string returnUrl)
        {
            WorkBusiness wb = new WorkBusiness();
            if (id != null)
            {
                try
                {
                    if (wb.JobStateUpdate(Guid.Parse(id), "Tamamlandı"))
                    {
                        TempData["jobstateUptade"] = true;
                        return Redirect("~" + returnUrl);
                    }
                    else
                    {

                        return Redirect("~" + returnUrl);
                    }
                }
                catch (Exception)
                {
                    return Redirect("~" + returnUrl);
                }
            }
            return Redirect("~" + returnUrl);
        }
        public ActionResult JobWait(string id, string returnUrl)
        {
            WorkBusiness wb = new WorkBusiness();
            if (id != null)
            {
                try
                {
                    if (wb.JobStateUpdate(Guid.Parse(id), "Beklemede"))
                    {
                        TempData["jobstateWait"] = true;
                        return Redirect("~" + returnUrl);
                    }
                    else
                    {

                        return Redirect("~" + returnUrl);
                    }
                }
                catch (Exception)
                {
                    return Redirect("~" + returnUrl);
                }
            }
            return Redirect("~" + returnUrl);
        }
        public ActionResult JobPlanned(string id, string returnUrl)
        {
            WorkBusiness wb = new WorkBusiness();
            if (id != null)
            {
                try
                {
                    if (wb.JobStateUpdate(Guid.Parse(id), "Planlanan"))
                    {
                        TempData["jobstateplanned"] = true;
                        return Redirect("~" + returnUrl);
                    }
                    else
                    {
                        return Redirect("~" + returnUrl);
                    }
                }
                catch (Exception)
                {
                    return Redirect("~" + returnUrl);
                }
            }
            return Redirect("~" + returnUrl);
        }
        public ActionResult DeleteWork(string id)
        {
            WorkAdditionRepository wap = new WorkAdditionRepository();
            WorkBusiness wb = new WorkBusiness();
            try
            {

                List<WorkAddition> list = wap.GetListWorkById(Guid.Parse(id));
                if (wb.DeteleWork(Guid.Parse(id)))
                {
                    foreach (var item in list)
                    {
                        var fullPath = Server.MapPath("~/Content/UploadedFiles/" + item.FilePath);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("MyWorks");
            }

            return RedirectToAction("MyWorks");
        }
        public ActionResult WaitingJobs()
        {
            WorkBusiness wb = new WorkBusiness();
            return View(wb.GetWaitingWorksList(User.Identity.GetUserId()));
        }
        public ActionResult CompletedJobs()
        {
            WorkBusiness wb = new WorkBusiness();
            return View(wb.GetCompletedWorksList(User.Identity.GetUserId()));
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
                return RedirectToAction("Profile", "Panel", new { id = User.Identity.GetUserId() });

            }
            else return Redirect("~Error404.aspx");


        }

        public FileResult DownloadFile(string id)
        {
            try
            {
                WorkAdditionRepository rp = new WorkAdditionRepository();
                Guid guid = Guid.Parse(id.ToUpper());
                WorkAddition addition = rp.GetWorkById(guid);
                FilePathResult filePathResult = new FilePathResult(Server.MapPath("~/Content/UploadedFiles/" + addition.FilePath + ""), addition.FileType);
                filePathResult.FileDownloadName = $"{DateTime.Now.ToString()}_{addition.Filename}";
                return filePathResult;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public ActionResult EditWork(string id)
        {
            if (id != null)
            {
                WorkViewModel model = new WorkViewModel();
                try
                {
                    UserRepository up = new UserRepository();
                    WorkBusiness wb = new WorkBusiness();
                    model.Works = wb.GetWorkById(Guid.Parse(id));
                    if (model.Works != null)
                    {


                        model.Users = up.GetUserListDependencyId(User.Identity.GetUserId());
                        model.Me = up.GetUserById(User.Identity.GetUserId());
                        model.WorkAdditionList = wb.GetListWorkAdditionByWorkId(model.Works.Id);
                        model.EmployeeUsers = model.Works.EmployeeUser_Id;
                        if (model.Me.userInfo.DependencyId != null)
                        {
                            model.Users.Add(up.GetUserById(model.Me.userInfo.DependencyId));
                        }
                    }
                    else return RedirectToAction("MyWorks");
                }
                catch (Exception)
                {
                    Redirect("~/Error500.aspx");
                }
                return View(model);
            }
            else return RedirectToAction("MyWorks");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditWork(WorkViewModel model, string EmployeeUsers, HttpPostedFileBase[] files)
        {
            UserRepository userRepository = new UserRepository();
            _BusinessLayer<WorkViewModel> work_layer = new _BusinessLayer<WorkViewModel>();
            if (ModelState.IsValid)
            {


                WorkBusiness workBusiness = new WorkBusiness();
                List<WorkAddition> additionlist = new List<WorkAddition>();
                work_layer = workBusiness.UpdateWorks(model, EmployeeUsers, User.Identity.GetUserId()); // iş eklendi geri alınıp atandı.
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
                        var fileSize = file.ContentLength;
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                        additionlist.Add(new WorkAddition { FileType = fileType, Filename = InputFileName, FilePath = userRepository.GetUserById(EmployeeUsers).Email + "/" + InputFileName, Work_Id = work_layer.Result.Works.Id, FileSize = fileSize.ToString() });
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
            WorkAdditionRepository workAddition = new WorkAdditionRepository();
            model.WorkAdditionList = workAddition.GetWorkAdditions(model.Works.Id);
            if (model.Me.userInfo.DependencyId != null)
            {
                model.Users.Add(userRepository.GetUserById(model.Me.userInfo.DependencyId));
            }
            return View(model);
        }

        public ActionResult DeleteFile(string id)
        {
            if (id != null)
            {
                try
                {
                    WorkAdditionRepository wap = new WorkAdditionRepository();
                    WorkAddition workAddition = wap.DeleteById(Guid.Parse(id));
                    var fullPath = Server.MapPath("~/Content/UploadedFiles/" + workAddition.FilePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    return RedirectToAction("EditWork", new { id = workAddition.Id });
                }
                catch (Exception)
                {

                    return RedirectToAction("MyWorks");
                }
            }
            return RedirectToAction("MyWorks");
        }
        public ActionResult GetLiked(Guid[] ids)
        {
            if (ids != null)
            {
                if (ids.Count() > 0)
                {
                    List<Guid> LikedWorkflowIds = new List<Guid>();
                    LikesRepository like = new LikesRepository();
                    return Json(new { result = like.GetLikedList(User.Identity.GetUserId(), ids) });
                }
            }
      

            return Json(null);
        }
        public ActionResult SetLikeState(Guid workfid, bool likedstate)
        {
            try
            {
                LikesRepository likemanger = new LikesRepository();
                WorkFlowBusiness wfb = new WorkFlowBusiness();
                Likes like = likemanger.GetLiked(User.Identity.GetUserId(), workfid);
                Workflow wf = wfb.GetWorkflow(workfid);
                int res = 0;
                if (like != null && likedstate == false)
                {
                    if (likemanger.RemoveLike(like) > 0)
                    {
                        if (likedstate == true)
                        {
                            wf.LikeCount++;
                        }
                        else
                        {
                            wf.LikeCount--;
                        }
                        res = wfb.UpdateWorkflow(wf);
                        return Json(new { hasError = false, errorMessage = "", result = wf.LikeCount });
                    }

                }
                else if (like == null && likedstate == true)
                {
                    res = likemanger.AddLike(new Likes { LikeUserId = User.Identity.GetUserId(), WorkflowId = workfid });
                    if (res > 0)
                    {
                        if (likedstate == true)
                        {
                            wf.LikeCount++;
                        }
                        else
                        {
                            wf.LikeCount--;
                        }
                        res = wfb.UpdateWorkflow(wf);
                        return Json(new { hasError = false, errorMessage = "", result = wf.LikeCount });
                    }
                }
                else if (res == 0)
                {
                    return Json(new { hasError = true, errorMessage = "Hay aksi bir sorun oluştu. Lütfen tekrar deneyiniz.", result = wf.LikeCount });
                }
                return Json(new { hasError = false, errorMessage = "", result = wf.LikeCount });

            }
            catch (Exception)
            {

                return Json(new { hasError = true, errorMessage = "Hay aksi bir sorun oluştu. Lütfen tekrar deneyiniz." });
            }

        }



    }
}