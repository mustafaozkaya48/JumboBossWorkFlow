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
    public class Workflow
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string RequestingUserId { get; set; }
        [Required]
        [ForeignKey("RequestingUserId")]
        public virtual ApplicationUser RequestingUser { get; set; }
        [Required]
        public virtual ApplicationUser EmployeeUsers { get; set; }
        public int LikeCount { get; set; }
        public ICollection<WorkCommenteds> Commenteds { get; set; }

    }
}
