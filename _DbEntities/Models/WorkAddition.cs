using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models
{
    public partial class WorkAddition
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Filename { get; set; }

        public string FilePath { get; set; }

        public Guid Work_Id { get; set; }
        [ForeignKey("Work_Id")]
        public virtual Work Work { get; set; }
    }
}
