using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public class Works
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }

        [Required]
        public DateTime UpdateDateTime { get; set; }
        [Required]
        public virtual ApplicationUser RequestingUser { get; set; }
        [Required]
        public virtual ApplicationUser EmployeeUser{ get; set; }

        public virtual ICollection<WorkAddition> Addition { get; set; }

        [Column(TypeName = "VARCHAR")]
        [Required,StringLength(300)]
        public string WorkDescription { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Required, StringLength(300)]
        public string WorkTitle { get; set; }
      


    }
}
