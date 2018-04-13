using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public partial class WorkCommented
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Commented { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        public Guid Workflow_Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Work Work { get; set; }
    }
}
