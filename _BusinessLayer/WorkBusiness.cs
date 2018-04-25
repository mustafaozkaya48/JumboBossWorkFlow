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
        _BusinessLayer<WorkViewModel> _work_layer = new _BusinessLayer<WorkViewModel>();
        WorkRepository wr = new WorkRepository();
        UserRepository userRepository = new UserRepository();
       
        public _BusinessLayer<WorkViewModel> AddWorks(WorkViewModel model, string EmployeeUsers, string RequestingUser)
        {
            try
            {
                model.Works.EmployeeUser_Id = EmployeeUsers;
                model.Works.RequestingUser_Id = RequestingUser;
                model.Works.CreateDateTime = DateTime.Now;
                model.Works.UpdateDateTime = DateTime.Now;
                model.Works.WorkState = "Planlanan";
                if (model.Works.EmployeeUser_Id == "")
                {
                    _work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Lütfen bir Görevli seçimi yapınız.");
                }
                if (model.Works.WorkTitle != null && model.Works.EmployeeUser_Id != null && model.Works.WorkDescription != null && model.Works.WorkDateTime != null)
                {
                    _work_layer.Result = model;
                    int result = wr.AddWork(model.Works);

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
                    _work_layer.Result.Works = wr.GetWork(model.Works);
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
        public _BusinessLayer<WorkViewModel> UpdateWorks(WorkViewModel model, string EmployeeUsers, string RequestingUser)
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
                if (model.Works.WorkTitle != null && model.Works.EmployeeUser_Id != null && model.Works.WorkDescription != null && model.Works.WorkDateTime != null)
                {
                    _work_layer.Result = model;
                    int result = wr.WorkUpdate(model.Works);

                    if (result > 0)
                    {
                        _work_layer.AddInfo(Messages.InfoMessageCode.AddWorkSuccess, "İş Güncellendi");
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
        public void AddWorkAddition(List<WorkAddition> _worksadditionModel)
        {
            WorkAdditionRepository workAdditionRepository = new WorkAdditionRepository();
            workAdditionRepository.AddWorkAddition(_worksadditionModel);
        }
        public Work GetWorkById(Guid workid) {
           return wr.GetWorkById(workid);
        }
        public List<Work> GetAllWorks(string UserId)
        {
            return wr.GetListWorks(UserId).OrderBy(m => m.WorkDateTime).ToList();
        }
        public List<Work> GetPlannedWorksList(string UserId)
        {
            return wr.GetListPlannedWorks(UserId).OrderBy(m => m.WorkDateTime).ToList();
        }
        public List<Work> GetWaitingWorksList(string UserId)
        {
            return wr.GetListWaitingWorks(UserId).OrderBy(m => m.WorkDateTime).ToList();
        }
        public List<Work> GetCompletedWorksList(string UserId)
        {
            return wr.GetListComletedWorks(UserId).OrderBy(m => m.WorkDateTime).ToList();
        }
        public List<WorkAddition> GetListWorkAdditionByWorkId(Guid WorkId) {
            WorkAdditionRepository warp = new WorkAdditionRepository();
           return warp.GetWorkAdditions(WorkId);

        }
        public bool DeteleWork(Guid id)
        {
            if (wr.DeleteWork(wr.GetWorkById(id)) > 0) return true;
            else return false;
        }
        public bool JobStateUpdate(Guid İd,string state)
        {
          Work work=wr.GetWorkById(İd);
            if (work!=null)
            {
                work.WorkState = state;
                work.UpdateDateTime = DateTime.Now;
                if (wr.WorkUpdate(work)>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public List<WorkViewModel> GetListItemsWorkFlow(string Uid)
        {
            WorkAdditionRepository wap = new WorkAdditionRepository();
            List<WorkViewModel> list = new List<WorkViewModel>();
           
            foreach (var item in wr.GetListWorksAftertoday(Uid))
            {
                WorkViewModel model = new WorkViewModel();
                model.Works = item;
                model.WorkAdditionList = wap.GetListWorkById(model.Works.Id);
                list.Add(model);
            }
            return list.OrderBy(x=>x.Works.WorkDateTime).ToList();
           
        }
    }
}
