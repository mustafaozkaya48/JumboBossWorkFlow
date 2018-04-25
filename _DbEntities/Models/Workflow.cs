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
        public int LikeCount { get; set; }
        public string RequestingUserId { get; set; }
        [ForeignKey("RequestingUserId")]
        public virtual ApplicationUser RequestingUser { get; set; }
        public string EmployeeUsersId { get; set; }
        [ForeignKey("EmployeeUsersId")]
        public virtual ApplicationUser EmployeeUser { get; set; }
        public Guid Work_Id { get; set; }
        [ForeignKey("Work_Id")]
        public virtual Work Work { get; set; }
        public string DependencyId { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<WorkCommented> WorkCommented { get; set; }

    }
}