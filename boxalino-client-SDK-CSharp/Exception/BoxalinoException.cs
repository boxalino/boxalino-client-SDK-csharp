using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boxalino_client_SDK_CSharp.Exception
{
   public class BoxalinoException : System.Exception
    {
       private string message = string.Empty;

        public BoxalinoException() : base()
        {

        }

        public BoxalinoException(String message) : base(message)
        {
            setMessage(message);
        }
        public string getMessage()
        {
            return message;
        }
        public void setMessage(string Message)
        {
            message = Message;
        }
    }
}
