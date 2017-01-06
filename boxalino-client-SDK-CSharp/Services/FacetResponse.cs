/**
 * Autogenerated by Thrift Compiler (0.9.2)
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
    /// <summary>
    /// <dl>
    /// <dt>fieldName</dt>
    /// <dd>name of the facet field</dd>
    /// 
    /// <dt>values</dt>
    /// <dd>list of facet values</dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class FacetResponse : TBase
    {
        private string _fieldName;
        private List<FacetValue> _values;

        public string FieldName
        {
            get
            {
                return _fieldName;
            }
            set
            {
                __isset.fieldName = true;
                this._fieldName = value;
            }
        }

        public List<FacetValue> Values
        {
            get
            {
                return _values;
            }
            set
            {
                __isset.values = true;
                this._values = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool fieldName;
            public bool values;
        }

        public FacetResponse()
        {
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
                    case 1:
                        if (field.Type == TType.String)
                        {
                            FieldName = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.List)
                        {
                            {
                                Values = new List<FacetValue>();
                                TList _list53 = iprot.ReadListBegin();
                                for (int _i54 = 0; _i54 < _list53.Count; ++_i54)
                                {
                                    FacetValue _elem55;
                                    _elem55 = new FacetValue();
                                    _elem55.Read(iprot);
                                    Values.Add(_elem55);
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
            TStruct struc = new TStruct("FacetResponse");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (FieldName != null && __isset.fieldName)
            {
                field.Name = "fieldName";
                field.Type = TType.String;
                field.ID = 1;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(FieldName);
                oprot.WriteFieldEnd();
            }
            if (Values != null && __isset.values)
            {
                field.Name = "values";
                field.Type = TType.List;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.Struct, Values.Count));
                    foreach (FacetValue _iter56 in Values)
                    {
                        _iter56.Write(oprot);
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
            StringBuilder __sb = new StringBuilder("FacetResponse(");
            bool __first = true;
            if (FieldName != null && __isset.fieldName)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("FieldName: ");
                __sb.Append(FieldName);
            }
            if (Values != null && __isset.values)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Values: ");
                __sb.Append(Values);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}