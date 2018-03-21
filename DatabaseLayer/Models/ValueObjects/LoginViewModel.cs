using _DataBaseLayer.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DataBaseLayer.Models.ValueObjects
{
   
    public class LoginViewModel
    {
        public LoginModel LoginModel { get; set; }
        public RegisterModel RegisterModel { get; set; }
    }
}
