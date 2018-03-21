using _DataBaseLayer.Messages;

using BusinessLayer;
using _DataBaseLayer.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using _DataBaseLayer.Repository.Abstract;
using _DataBaseLayer.Models;
using _DataBaseLayer;
using _DataBaseLayer.Repository.Concrete;

namespace _DataBaseLayer
{
    public class UserManager
    {
        private IRepository<User> _userRepository;
        private IUnitOfWork _userUnitofWork;
        private DbContext _dbContext;
        
        public UserManager()
        {
            _dbContext = new DatabaseContext();
            _userRepository = new EFRepository<User>(_dbContext);
            _userUnitofWork = new EFUnitOfWork(_dbContext);
          
        }

        public _BusinessLayer<RegisterModel> AddUser(RegisterModel model)
        {
            _BusinessLayer<RegisterModel> _result = new _BusinessLayer<RegisterModel>();
            User user =_userRepository.Get(x => x.PhoneNumber == model.PhoneNumber||x.EMail==model.EMail);
            if (user != null)
            {
                if (user.EMail == model.EMail)
                {
                    _result.AddError(ErrorMessagesCode.EmailAlredyExists, "E-Posta adresi daha önce kullanılmış.");
                }
                if (user.PhoneNumber == model.PhoneNumber)
                {
                    _result.AddError(ErrorMessagesCode.PhoneAlredyExists, "Bu Telefon numarası daha önce kullanılmış.");
                }
            }
            else {

                _userRepository.Insert(new User
                {
                    Name = model.Name,
                    SurName = model.Surname,
                    EMail = model.EMail,
                    PhoneNumber = model.PhoneNumber,
                });
                _result.AddInfo(InfoMessageCode.RegisterSuccess,"Kayıt Başarılı");
            }
            return _result;

        }


    }
}