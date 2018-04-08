using _DbEntities.Models;
using System.Web;
using _DbEntities.Repository.Concrete;

namespace _BusinessLayer
{
   public class User_Business
    {
        _BusinessLayer<ApplicationUser> _user_layer = new _BusinessLayer<ApplicationUser>();
        UserRepository _userrepository = new UserRepository();


        public ApplicationUser GetUserByEmail(string Email)
        {
            return _userrepository.GetUserByEmail(Email);

             
        }
        public ApplicationUser GetUserById(string userID)
        {
            return _userrepository.GetUserById(userID);


        }

    }
}
