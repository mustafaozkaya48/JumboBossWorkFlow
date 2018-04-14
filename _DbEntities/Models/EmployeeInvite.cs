using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
   public class EmployeeInvite
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string DependencyId { get; set; }
        public Guid cid { get; set; }
        public string InvitationEmail { get; set; }

    }
}
