using _DbEntities.Models;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
    public class CommendRepository
    {
        private IRepository<WorkCommented> _CommendRepository;
        private IUnitOfWork _CommendUnitofWork;
        private ApplicationDbContext _dbContext;
        public CommendRepository()
        {
            _dbContext = new ApplicationDbContext();
            _CommendRepository = new EFRepository<WorkCommented>(_dbContext);
            _CommendUnitofWork = new EFUnitOfWork(_dbContext);
        }

        public int AddCommend(WorkCommented entity)
        {
            _CommendRepository.Insert(entity);
            return _CommendUnitofWork.SaveChanges();
        }
    }
}
