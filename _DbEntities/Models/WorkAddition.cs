using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
   public class WorkAddition
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public virtual Works Work { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string Filename { get; set; }
        public string FilePath { get; set; }
    }
}
