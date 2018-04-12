using _DbEntities.Models;
using _DbEntities.Models.ValueObj;
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
        _BusinessLayer<Works> work_layer = new _BusinessLayer<Works>();
        WorkRepository workRepository = new WorkRepository();
        public _BusinessLayer<Works> AddWorks(Works _worksModel)
        {
            try
            {
               int result = workRepository.AddWork(_worksModel);
                if (result > 0)
                {
                    work_layer.AddInfo(Messages.InfoMessageCode.AddWorkSuccess, "İş Eklendi.");
                    work_layer.Result = workRepository.GetWork(_worksModel);
                    return work_layer;
                }
                else
                {
                    work_layer.Result = _worksModel;
                    work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                    return work_layer;
                }
            }
            catch (Exception ex)
            {
                work_layer.Result = _worksModel;
                work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                return work_layer;
            }
        }
        public _BusinessLayer<Works> UpdateWorks(Works _worksModel)
        {
            try
            {
                int result = workRepository.WorkUpdate(_worksModel);
                if (result > 0)
                {
                    work_layer.AddInfo(Messages.InfoMessageCode.AddWorkSuccess, "İş Güncellendi.");
                    work_layer.Result = workRepository.GetWork(_worksModel);
                    return work_layer;
                }
                else
                {
                    work_layer.Result = _worksModel;
                    work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                    return work_layer;
                }
            }
            catch (Exception)
            {
                work_layer.Result = _worksModel;
                work_layer.AddError(Messages.ErrorMessagesCode.AddWorkError, "Hay aksi bir sorun oluştu.Yeniden deneyiniz.");
                return work_layer;
            }
        }
        public void AddWorkAddition(List<WorkAddition> _worksadditionModel)
        {
            WorkAdditionRepository workAdditionRepository = new WorkAdditionRepository();
            workAdditionRepository.AddWorkAddition(_worksadditionModel);
        }
    }
}
