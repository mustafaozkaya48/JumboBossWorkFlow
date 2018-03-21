using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DataBaseLayer.Models.ValueObjects
{
    public class LoginModel
    {
        public string EmailorPhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
