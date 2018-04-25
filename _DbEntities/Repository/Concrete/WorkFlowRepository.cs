using _DbEntities.Models;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
    public class WorkFlowRepository
    {
        private IRepository<Workflow> _WorkFlowRepository;
        private IUnitOfWork _WorkFlowUnitofWork;
        private ApplicationDbContext _dbContext;
        public WorkFlowRepository()
        {
            _dbContext = new ApplicationDbContext();
            _WorkFlowRepository = new EFRepository<Workflow>(_dbContext);
            _WorkFlowUnitofWork = new EFUnitOfWork(_dbContext);
        }
        public void AddWorkFlow(Workflow entity)
        {
            entity.Id = Guid.NewGuid();
            _WorkFlowRepository.Insert(entity);
            _WorkFlowUnitofWork.SaveChanges();
        }
        public List<Workflow> GetListDepenctyID(string DepId)
        {
         return _WorkFlowRepository.GetAll(m=>m.DependencyId==DepId).OrderByDescending(m=>m.Work.UpdateDateTime).ToList();
        }

        public Workflow GetWorkflow(Guid guid)
        {
            return _WorkFlowRepository.Get(x=>x.Id==guid);
        }
        public int UpdateWorkflow(Workflow entity)
        {
            _WorkFlowRepository.Updete(entity);
            return _WorkFlowUnitofWork.SaveChanges();
        }
    }
}
