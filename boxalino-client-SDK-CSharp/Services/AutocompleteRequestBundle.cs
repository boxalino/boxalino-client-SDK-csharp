/**
 * Autogenerated by Thrift Compiler (0.10.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;


namespace boxalino_client_SDK_CSharp.Services
{
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class AutocompleteRequestBundle : TBase
    {
        private List<AutocompleteRequest> _requests;

        public List<AutocompleteRequest> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                __isset.requests = true;
                this._requests = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool requests;
        }

        public AutocompleteRequestBundle()
        {
        }

        public void Read(TProtocol iprot)
        {
            iprot.IncrementRecursionDepth();
            try
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
                                    Requests = new List<AutocompleteRequest>();
                                    TList _list170 = iprot.ReadListBegin();
                                    for (int _i171 = 0; _i171 < _list170.Count; ++_i171)
                                    {
                                        AutocompleteRequest _elem172;
                                        _elem172 = new AutocompleteRequest();
                                        _elem172.Read(iprot);
                                        Requests.Add(_elem172);
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
            finally
            {
                iprot.DecrementRecursionDepth();
            }
        }

        public void Write(TProtocol oprot)
        {
            oprot.IncrementRecursionDepth();
            try
            {
                TStruct struc = new TStruct("AutocompleteRequestBundle");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (Requests != null && __isset.requests)
                {
                    field.Name = "requests";
                    field.Type = TType.List;
                    field.ID = 11;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.Struct, Requests.Count));
                        foreach (AutocompleteRequest _iter173 in Requests)
                        {
                            _iter173.Write(oprot);
                        }
                        oprot.WriteListEnd();
                    }
                    oprot.WriteFieldEnd();
                }
                oprot.WriteFieldStop();
                oprot.WriteStructEnd();
            }
            finally
            {
                oprot.DecrementRecursionDepth();
            }
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("AutocompleteRequestBundle(");
            bool __first = true;
            if (Requests != null && __isset.requests)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Requests: ");
                __sb.Append(Requests);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}