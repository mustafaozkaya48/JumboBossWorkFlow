using _BusinessLayer.Helpers;
using _DbEntities.Models;
using _DbEntities.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BusinessLayer
{
    public class EmployeeInviteBusiness
    {
        _BusinessLayer<EmployeeInvite> _SendEmployeeInvite_layer = new _BusinessLayer<EmployeeInvite>();
        EmployeeInvitesRepositoy _employeeInvitesRepositoy = new EmployeeInvitesRepositoy();
        UserRepository userRepository = new UserRepository();
        public _BusinessLayer<EmployeeInvite> SendEmployeeInvite(ApplicationUser user,string EmployeeInviteEmail)
        {

            if (userRepository.GetUserByEmail(EmployeeInviteEmail)==null)
            {
                EmployeeInvite employeeInvite = new EmployeeInvite();
                employeeInvite= _employeeInvitesRepositoy.GetEmployeeInvites(EmployeeInviteEmail);
                if (employeeInvite==null)
                {
                    employeeInvite = new EmployeeInvite();
                    employeeInvite.cid =Guid.NewGuid();
                    employeeInvite.DependencyId = user.Id;
                    employeeInvite.InvitationEmail = EmployeeInviteEmail;
                  
                    if (_employeeInvitesRepositoy.AddEmployeeInvites(employeeInvite)>0)
                    {
                        string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                        string ActivateUri = $"{siteUri}/Workflow/Account/JoinUser?cid={employeeInvite.cid}";
                        string body = $"<div class=''><h1>{user.userInfo.Name + " " + user.userInfo.SurName}</h1> <br /><br /><h2>Sizi Jumbo Boss İş Takip Programına davet etti. <br>Hesap oluşturarak{user.userInfo.Name + " " + user.userInfo.SurName}'nın ekibine katılabilirsiniz.<br> Katılmak İçin <a href='{ActivateUri}' target='_blank'>Tıklayınız</a></h2>.</div>";
                        MailHelper.SendMail(body, EmployeeInviteEmail, user.userInfo.Name + " " + user.userInfo.SurName + " Sizi Jumbo Boss İş Takip Programına davet etti.");
                        _SendEmployeeInvite_layer.AddInfo(Messages.InfoMessageCode.AddInviteSendSuccess, "Davetiye Gönderildi.");
                    }
                    else
                    {
                        _SendEmployeeInvite_layer.AddError(Messages.ErrorMessagesCode.SendInvalteError, "Beklenmedik bir hata oluştu mail gönderilmedi tekrar deneyiniz.");
                    }
                }
                else
                {
                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string ActivateUri = $"{siteUri}/Workflow/Account/JoinUser?cid={employeeInvite.cid}";
                    string body = $"<div class=''><h1>{user.userInfo.Name + " " + user.userInfo.SurName}</h1> <br /><br /><h2>Sizi Jumbo Boss İş Takip Programına davet etti. <br>Hesap oluşturarak {user.userInfo.Name + " " + user.userInfo.SurName}'nın ekibine katılabilirsiniz.<br> Katılmak İçin <a href='{ActivateUri}' target='_blank'>Tıklayınız</a></h2>.</div>";
                    MailHelper.SendMail(body, EmployeeInviteEmail, user.userInfo.Name + " " + user.userInfo.SurName + " Sizi Jumbo Boss İş Takip Programına davet etti.");
                    _SendEmployeeInvite_layer.AddInfo(Messages.InfoMessageCode.AddInviteSendSuccess, "Davetiye Gönderildi.");
                }
          
             
            }
            else
            {
                _SendEmployeeInvite_layer.AddError(Messages.ErrorMessagesCode.SendInvalteError,"Şuan Sistemde Kayıtlı bir E-Posta adresi girdiniz.");

            }

            return _SendEmployeeInvite_layer;
        }
         




    }
}
