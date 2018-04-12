using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DbEntities.Models.ValueObj
{
    public class AddWork
    {
        public Works Works { get; set; }
        public WorkAddition WorkAddition { get; set; }
        public Workflow Workflow { get; set; }

    }
}
