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
    /// field to be used for sorting
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class SortField : TBase
    {
        private string _fieldName;
        private bool _reverse;

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

        public bool Reverse
        {
            get
            {
                return _reverse;
            }
            set
            {
                __isset.reverse = true;
                this._reverse = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool fieldName;
            public bool reverse;
        }

        public SortField()
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
                        if (field.Type == TType.Bool)
                        {
                            Reverse = iprot.ReadBool();
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
            TStruct struc = new TStruct("SortField");
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
            if (__isset.reverse)
            {
                field.Name = "reverse";
                field.Type = TType.Bool;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(Reverse);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("SortField(");
            bool __first = true;
            if (FieldName != null && __isset.fieldName)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("FieldName: ");
                __sb.Append(FieldName);
            }
            if (__isset.reverse)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Reverse: ");
                __sb.Append(Reverse);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}
