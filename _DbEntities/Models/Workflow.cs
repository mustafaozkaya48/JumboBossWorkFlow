using _DbEntities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public partial class Workflow
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string RequestingUserId { get; set; }

        public int LikeCount { get; set; }

        [Required]
        [StringLength(128)]
        public string EmployeeUsers_Id { get; set; }

        public virtual ApplicationUser RequestingUser { get; set; }

        public virtual ApplicationUser EmployeeUser { get; set; }
    }
}