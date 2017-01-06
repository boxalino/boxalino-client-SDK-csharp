using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
   public class TBinaryProtocolAccelerated :TBinaryProtocol
    {
        TBinaryProtocolAccelerated(TTransport trans, bool strictRead= false,bool strictWrite= true):base
            (trans, strictRead, strictWrite)
        {
            if (!method_exists(trans,"putBack"))
            {
                // trans = new TBufferedTransport(trans);
            }
        }

        public bool isStrictRead()
        {
            return this.strictRead_;
        }
        public bool isStrictWrite()
        {
            return this.strictWrite_;
        }

        public static bool method_exists(object obj, string method_name)
        {
            MethodInfo methodInfo = obj.GetType().GetMethod(method_name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            return (methodInfo != null);
        }
    }
}
