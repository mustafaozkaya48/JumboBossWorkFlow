using _DbEntities.Models;
using _DbEntities.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Repository.Concrete
{
   public class EmployeeInvitesRepositoy
    {
        private IRepository<EmployeeInvite> _EmployeeInvitesRepository;
        private IUnitOfWork _EmployeeInvitesUnitofWork;
        private ApplicationDbContext _dbContext;
        public EmployeeInvitesRepositoy()
        {
            _dbContext = new ApplicationDbContext();
            _EmployeeInvitesRepository = new EFRepository<EmployeeInvite>(_dbContext);
            _EmployeeInvitesUnitofWork = new EFUnitOfWork(_dbContext);
        }

        public int AddEmployeeInvites(EmployeeInvite entity)
        {
            _EmployeeInvitesRepository.Insert(entity);
            return _EmployeeInvitesUnitofWork.SaveChanges();
        }
        public EmployeeInvite GetEmployeeInvites(string Email)
        {
            
            return _EmployeeInvitesRepository.Get(m => m.InvitationEmail == Email);
        }
        public void DeleteEmployeeInvites(EmployeeInvite entity)
        {
            _EmployeeInvitesRepository.Delete(entity);
        }
        public EmployeeInvite GetEmployeeInvitesByCid(Guid cid)
        {

            return _EmployeeInvitesRepository.Get(m => m.cid == cid);
        }


    }
}
