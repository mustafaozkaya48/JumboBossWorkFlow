using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public class Likes
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LikeUserId { get; set; }
        [ForeignKey("LikeUserId")]
        public ApplicationUser LikeUser { get; set; }
        public Guid WorkflowId { get; set; }
        [ForeignKey("WorkflowId")]
        public Workflow Workflow { get; set; }
    }
}
