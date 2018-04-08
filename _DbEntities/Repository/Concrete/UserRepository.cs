using _DbEntities.Models;
using _DbEntities.Repository.Abstract;


namespace _DbEntities.Repository.Concrete
{
    public class UserRepository
    {
        private IRepository<ApplicationUser> _UserRepository;
        private IUnitOfWork _UserUnitofWork;
        private ApplicationDbContext _dbContext;
        public UserRepository()
        {
            _dbContext = new ApplicationDbContext();
            _UserRepository = new EFRepository<ApplicationUser>(_dbContext);
            _UserUnitofWork = new EFUnitOfWork(_dbContext);
        }

        public ApplicationUser GetUser(ApplicationUser model)
        { 
                
            return _UserRepository.Get(x => x.Email == model.Email);
        }

        public ApplicationUser GetUserById(string id)
        {
            return _UserRepository.Get(x => x.Id == id);
        }
        public ApplicationUser GetUserByEmail(string email)
        {
            return _UserRepository.Get(x => x.Email == email);
        }
        public void UpdateUser(ApplicationUser user)
        {
          _UserRepository.Updete(user);
        }


    }
}
