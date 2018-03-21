using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _DataBaseLayer.Messages.MessageObj
{
    public class ErrorMsgObj
    {
        public ErrorMessagesCode Code { get; set; }
        public string Message { get; set; }
    }
}
