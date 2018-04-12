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
    public class WorkRepository
    {
        private IRepository<Works> _WorkRepository;
        private IUnitOfWork _WorkUnitofWork;
        private ApplicationDbContext _dbContext;
        public WorkRepository()
        {
            _dbContext = new ApplicationDbContext();
            _WorkRepository = new EFRepository<Works>(_dbContext);
            _WorkUnitofWork = new EFUnitOfWork(_dbContext);
        }
        public int AddWork(Works entity) {
           _WorkRepository.Insert(entity);
            return _WorkUnitofWork.SaveChanges();

        }
        public int WorkUpdate(Works entity)
        {
            _WorkRepository.Updete(entity);
            return _WorkUnitofWork.SaveChanges();

        }
        public Works GetWork(Works entity)
        {
           
            return _WorkRepository.Find(entity);
        }


    }
}
