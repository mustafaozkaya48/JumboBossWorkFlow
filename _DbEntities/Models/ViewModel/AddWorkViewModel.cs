using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace _DbEntities.Models.ViewModel
{
    public class AddWorkViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<WorkAddition> WorkAdditionList { get; set; }
        public ApplicationUser Me { get; set; }
        public string EmployeeUsers { get; set; }
        public FileModel FileModel { get; set; }
        public Work Works { get; set; }

    }
    public class FileModel
    {
       
        [Display(Name = "files"),DisplayName("files")]
        public HttpPostedFileBase[] files { get; set; }

    }

}

