using boxalino_client_SDK_CSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;

namespace boxalino_client_SDK_CSharp.Services
{
    public class AutocompleteResponseBundle
    {

        static string _TSPEC;

        /**
         * @var \com\boxalino\p13n\api\thrift\AutocompleteResponse[]
         */
        private List<AutocompleteResponse> responses = new List<AutocompleteResponse>();

        public List<AutocompleteResponse> Responses
        {
            get
            {
                return responses;
            }
            set
            {
                this.responses = value;
            }
        }


        public string getName()
        {
            return "AutocompleteResponseBundle";
        }

        public void Read(TProtocol iprot)
        {
            TField field;

            iprot.ReadStructBegin();
            while (true)
            {
                field = iprot.ReadFieldBegin();
                if (field.Type == TType.Stop)
                {
                    break;
                }
                switch (field.ID)
                {
                    case 11:
                        if (field.Type == TType.List)
                        {
                            {

                                TList _list122 = iprot.ReadListBegin();
                                for (int _i123 = 0; _i123 < _list122.Count; ++_i123)
                                {
                                    AutocompleteResponse _elem124;
                                    _elem124 = new AutocompleteResponse();
                                    _elem124.Read(iprot);
                                    this.responses.Add(_elem124);
                                }
                                iprot.ReadListEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;

                    default:
                        TProtocolUtil.Skip(iprot, field.Type);
                        break;
                }
                iprot.ReadFieldEnd();
            }
            iprot.ReadStructEnd();
        }

        public void Write(TProtocol oprot)
        {
            TStruct struc = new TStruct("AutocompleteResponseBundle");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (this.responses != null)
            {
                field.Name = "requests";
                field.Type = TType.List;
                field.ID = 11;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.Struct, this.responses.Count));
                    foreach (AutocompleteResponse _iter125 in this.responses)
                    {
                        _iter125.Write(oprot);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("AutocompleteResponseBundle(");
            bool __first = true;
            if (this.responses != null)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("responses: ");
                __sb.Append(this.responses);
            }
            __sb.Append(")");
            return __sb.ToString();
        }
    }
}
