using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public partial class Work
    {
 
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreateDateTime { get; set; }
 
        public DateTime UpdateDateTime { get; set; }

        [Required,DisplayName("Tamamlanma Tarihi")]
        public DateTime WorkDateTime { get; set; }
        [Required]
        [StringLength(300),DisplayName("İş Açlaması")]
        public string WorkDescription { get; set; }

        [Required]
        [StringLength(300),DisplayName("İş Başlığı")]
        public string WorkTitle { get; set; }   
        public string EmployeeUser_Id { get; set; }   
        public string RequestingUser_Id { get; set; }
        [ForeignKey("EmployeeUser_Id")]
        public virtual ApplicationUser EmployeeUser { get; set; }
        [ForeignKey("RequestingUser_Id")]
        public virtual ApplicationUser RequestingUser { get; set; }


    }
}
