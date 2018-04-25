using _BusinessLayer.Messages;
using _DbEntities.Models;
using _DbEntities.Models.ViewModel;
using _DbEntities.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BusinessLayer
{
    public class WorkFlowBusiness
    {
        private _BusinessLayer<Workflow> layer;
        private WorkFlowRepository wfr;
        private LikesRepository lkd;
        private CommendRepository cmdr;
        public WorkFlowBusiness()
        {
            layer = new _BusinessLayer<Workflow>();
            wfr = new WorkFlowRepository();
            lkd = new LikesRepository();
            cmdr = new CommendRepository();
        }
        public _BusinessLayer<Workflow> AddWorkflow(Workflow model)
        {
            try
            {
                wfr.AddWorkFlow(model);
                layer.AddInfo(InfoMessageCode.Success,"Başarılı");
            }
            catch (Exception ex)
            {
                layer.AddError(ErrorMessagesCode.WorkFlowError,ex.InnerException.Message);
            }
            return layer;
          
        }

        public List<Workflow> GetList(ApplicationUser user)
        {
            string DepId;
            if (user.userInfo.DependencyId != null) DepId = user.userInfo.DependencyId;
            else DepId = user.Id;
            return wfr.GetListDepenctyID(DepId);
        }
        public Workflow GetWorkflow(Guid id)
        {
          return  wfr.GetWorkflow(id);
        }

        public int UpdateWorkflow(Workflow workflow)
        {
           return wfr.UpdateWorkflow(workflow);
        }

        public int AddCommend(Guid wfid, string message, string Uid)
        {
         return cmdr.AddCommend(new WorkCommented {Commented=message,User_Id=Uid,Id=wfid});
        }


    }
}
