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
    public partial class PropertyResult : TBase
    {
        private List<PropertyHit> _hits;
        private string _name;

        public List<PropertyHit> Hits
        {
            get
            {
                return _hits;
            }
            set
            {
                __isset.hits = true;
                this._hits = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                __isset.name = true;
                this._name = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool hits;
            public bool name;
        }

        public PropertyResult()
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
                                    Hits = new List<PropertyHit>();
                                    TList _list153 = iprot.ReadListBegin();
                                    for (int _i154 = 0; _i154 < _list153.Count; ++_i154)
                                    {
                                        PropertyHit _elem155;
                                        _elem155 = new PropertyHit();
                                        _elem155.Read(iprot);
                                        Hits.Add(_elem155);
                                    }
                                    iprot.ReadListEnd();
                                }
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 21:
                            if (field.Type == TType.String)
                            {
                                Name = iprot.ReadString();
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
                TStruct struc = new TStruct("PropertyResult");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (Hits != null && __isset.hits)
                {
                    field.Name = "hits";
                    field.Type = TType.List;
                    field.ID = 11;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.Struct, Hits.Count));
                        foreach (PropertyHit _iter156 in Hits)
                        {
                            _iter156.Write(oprot);
                        }
                        oprot.WriteListEnd();
                    }
                    oprot.WriteFieldEnd();
                }
                if (Name != null && __isset.name)
                {
                    field.Name = "name";
                    field.Type = TType.String;
                    field.ID = 21;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(Name);
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
            StringBuilder __sb = new StringBuilder("PropertyResult(");
            bool __first = true;
            if (Hits != null && __isset.hits)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Hits: ");
                __sb.Append(Hits);
            }
            if (Name != null && __isset.name)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Name: ");
                __sb.Append(Name);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}