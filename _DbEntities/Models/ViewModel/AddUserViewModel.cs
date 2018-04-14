using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models.ViewModel
{
    public class AddUserViewModel
    {
        [DisplayName("E-Posta")]
        public string EMail { get; set; }
        public List<ApplicationUser> Users{ get; set; }
        public ApplicationUser Me { get; set; }
    }
}
