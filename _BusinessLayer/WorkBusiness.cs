using _DbEntities.Models;
using _DbEntities.Models.ValueObj;
using _DbEntities.Models.ViewModel;
using _DbEntities.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BusinessLayer
{
   public class WorkBusiness
    {
        _BusinessLayer<AddWorkViewModel> _work_layer = new _BusinessLayer<AddWorkViewModel>();
        WorkRepository workRepository = new WorkRepository();
        UserRepository userRepository = new UserRepository();
        public _BusinessLayer<AddWorkViewModel> AddWorks(AddWorkViewModel model,string EmployeeUsers,string RequestingUser)
        {
            try
            {
                
                model.Works.EmployeeUser_Id = EmployeeUsers;
                model.Works.RequestingUser_Id = RequestingUser;
                model.Works.CreateDateTime = DateTime.Now;
                model.Works.UpdateDateTime = DateTime.Now;
                if (model.Works.EmployeeUser_Id == "")
                {
                    _work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Lütfen bir Görevli seçimi yapınız.");
                }
                if (model.Works.WorkTitle!=null&&model.Works.EmployeeUser_Id!=null&& model.Works.WorkDescription!=null&& model.Works.WorkDateTime!=null)
                {
                    _work_layer.Result = model;
                    int result = workRepository.AddWork(model.Works);
                    
                    if (result > 0)
                    {
                        _work_layer.AddInfo(Messages.InfoMessageCode.AddWorkSuccess, "İş Eklendi.");
                        return _work_layer;
                    }
                }
                else
                {
                    _work_layer.Result.Users = userRepository.GetUserListDependencyId(RequestingUser);//Üyeliğe bağlı alt üyeler çağırıldı
                    _work_layer.Result.Me = userRepository.GetUserById(RequestingUser);
                    _work_layer.Result = model;
                    return _work_layer;
                }
            }
            catch (Exception ex)
            {


                _work_layer.Result = model;
                _work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, ex.Message);
                return _work_layer;
            }
            return _work_layer;
        }
        public _BusinessLayer<AddWorkViewModel> UpdateWorks(AddWorkViewModel _worksModel)
        {
            try
            {
                int result = workRepository.WorkUpdate(_worksModel.Works);
                if (result > 0)
                {
                    _work_layer.AddInfo(Messages.InfoMessageCode.AddWorkSuccess, "İş Güncellendi.");
                    _work_layer.Result.Works = workRepository.GetWork(_worksModel.Works);
                    return _work_layer;
                }
                else
                {
                    _work_layer.Result = _worksModel;
                    _work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                    return _work_layer;
                }
            }
            catch (Exception)
            {
                _work_layer.Result = _worksModel;
                _work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                return _work_layer;
            }
        }
        public void AddWorkAddition(List<WorkAddition> _worksadditionModel)
        {
            WorkAdditionRepository workAdditionRepository = new WorkAdditionRepository();
            workAdditionRepository.AddWorkAddition(_worksadditionModel);
        }
        public List<Work> GetAllWorks(string UserId)
        {
            return workRepository.GetListWorks(UserId).OrderBy(m => m.WorkDateTime).ToList();
        }
    }
}
