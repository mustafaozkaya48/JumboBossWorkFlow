using _DbEntities.Models;
using _DbEntities.Models.ValueObj;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
    public class WorkAdditionRepository
    {
        private IRepository<WorkAddition> _WorkAdditionRepository;
        private IUnitOfWork _WorkAdditionUnitofWork;
        private ApplicationDbContext _dbContext;
        public WorkAdditionRepository()
        {
            _dbContext = new ApplicationDbContext();
           _WorkAdditionRepository = new EFRepository<WorkAddition>(_dbContext);
            _WorkAdditionUnitofWork = new EFUnitOfWork(_dbContext);
        }
        public void AddWorkAddition(List<WorkAddition> entity)
        {
            foreach (var item in entity)
            {
                _WorkAdditionRepository.Insert(item);
                
            }
            _WorkAdditionUnitofWork.SaveChanges();
        }
       


    }
}
