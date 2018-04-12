using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public class WorkCommenteds
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public Workflow Workflow { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Required,StringLength(300)]
        public string Commented { get; set; }
    }
}
