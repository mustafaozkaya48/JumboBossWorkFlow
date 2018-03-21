using _DataBaseLayer.Messages;
using _DataBaseLayer.Messages.MessageObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class _BusinessLayer<T> where T : class
    {

        public List<ErrorMsgObj> Errors { get; set; }
        public List<InfoMsgObj> Info { get; set; }
        public T Result { get; set; }
        public _BusinessLayer()
        {
            Errors = new List<ErrorMsgObj>();
            Info = new List<InfoMsgObj>();
        }

        public void AddError(ErrorMessagesCode code, string message)
        {
            Errors.Add(new ErrorMsgObj() { Code = code, Message = message });
        }

        public void AddInfo(InfoMessageCode code, string message)
        {
            Info.Add(new InfoMsgObj() { InfoCode = code, InfoMessage = message });
        }
    }
}
