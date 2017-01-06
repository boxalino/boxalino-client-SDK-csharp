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
    /// Filter to be used in query. Note that type of generated filter depends on first non-null and non-empty value in order of preference. Values of lower priority are ignored:
    /// stringValues!=null && simpleValues.size()>0 => simple match, prefix!=null => prefix match, hierarchy!=null && hierarchy.size()>0 => hierarchy filter, else range filter
    /// 
    /// <dl>
    /// <dt>negative</dt>
    /// <dd>whether the filter is negative (boolean NOT)</dd>
    /// 
    /// <dt>fieldName</dt>
    /// <dd>field name to apply filter to</dd>
    /// 
    /// <dt>stringValues</dt>
    /// <dd>values for simple match</dd>
    /// 
    /// <dt>prefix</dt>
    /// <dd>prefix match</dd>
    /// 
    /// <dt>hierarchyId</dt>
    /// <dd>hierarchy filter - when corresponding hierarchical field has encoded id</dd>
    /// 
    /// <dt>hierarchy</dt>
    /// <dd>hierarchy filter - for example categories path in top-down order</dd>
    /// 
    /// <dt>rangeFrom</dt>
    /// <dd>lower bound for range filter</dd>
    /// 
    /// <dt>rangeFromInclusive</dt>
    /// <dd>whether the lower bound is inclusive</dd>
    /// 
    /// <dt>rangeTo</dt>
    /// <dd>upper bound for range filter</dd>
    /// 
    /// <dt>rangeToInclusive</dt>
    /// <dd>whether the upper bound is inclusive</dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class Filter : TBase
    {
        private bool _negative;
        private string _fieldName;
        private List<string> _stringValues;
        private string _prefix;
        private string _hierarchyId;
        private List<string> _hierarchy;
        private string _rangeFrom;
        private bool _rangeFromInclusive;
        private string _rangeTo;
        private bool _rangeToInclusive;

        public bool Negative
        {
            get
            {
                return _negative;
            }
            set
            {
                __isset.negative = true;
                this._negative = value;
            }
        }

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

        public List<string> StringValues
        {
            get
            {
                return _stringValues;
            }
            set
            {
                __isset.stringValues = true;
                this._stringValues = value;
            }
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }
            set
            {
                __isset.prefix = true;
                this._prefix = value;
            }
        }

        public string HierarchyId
        {
            get
            {
                return _hierarchyId;
            }
            set
            {
                __isset.hierarchyId = true;
                this._hierarchyId = value;
            }
        }

        public List<string> Hierarchy
        {
            get
            {
                return _hierarchy;
            }
            set
            {
                __isset.hierarchy = true;
                this._hierarchy = value;
            }
        }

        public string RangeFrom
        {
            get
            {
                return _rangeFrom;
            }
            set
            {
                __isset.rangeFrom = true;
                this._rangeFrom = value;
            }
        }

        public bool RangeFromInclusive
        {
            get
            {
                return _rangeFromInclusive;
            }
            set
            {
                __isset.rangeFromInclusive = true;
                this._rangeFromInclusive = value;
            }
        }

        public string RangeTo
        {
            get
            {
                return _rangeTo;
            }
            set
            {
                __isset.rangeTo = true;
                this._rangeTo = value;
            }
        }

        public bool RangeToInclusive
        {
            get
            {
                return _rangeToInclusive;
            }
            set
            {
                __isset.rangeToInclusive = true;
                this._rangeToInclusive = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool negative;
            public bool fieldName;
            public bool stringValues;
            public bool prefix;
            public bool hierarchyId;
            public bool hierarchy;
            public bool rangeFrom;
            public bool rangeFromInclusive;
            public bool rangeTo;
            public bool rangeToInclusive;
        }

        public Filter()
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
                        if (field.Type == TType.Bool)
                        {
                            Negative = iprot.ReadBool();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.String)
                        {
                            FieldName = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.List)
                        {
                            {
                                StringValues = new List<string>();
                                TList _list0 = iprot.ReadListBegin();
                                for (int _i1 = 0; _i1 < _list0.Count; ++_i1)
                                {
                                    string _elem2;
                                    _elem2 = iprot.ReadString();
                                    StringValues.Add(_elem2);
                                }
                                iprot.ReadListEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.String)
                        {
                            Prefix = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 41:
                        if (field.Type == TType.String)
                        {
                            HierarchyId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 5:
                        if (field.Type == TType.List)
                        {
                            {
                                Hierarchy = new List<string>();
                                TList _list3 = iprot.ReadListBegin();
                                for (int _i4 = 0; _i4 < _list3.Count; ++_i4)
                                {
                                    string _elem5;
                                    _elem5 = iprot.ReadString();
                                    Hierarchy.Add(_elem5);
                                }
                                iprot.ReadListEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 6:
                        if (field.Type == TType.String)
                        {
                            RangeFrom = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 7:
                        if (field.Type == TType.Bool)
                        {
                            RangeFromInclusive = iprot.ReadBool();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 8:
                        if (field.Type == TType.String)
                        {
                            RangeTo = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 9:
                        if (field.Type == TType.Bool)
                        {
                            RangeToInclusive = iprot.ReadBool();
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
            TStruct struc = new TStruct("Filter");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (__isset.negative)
            {
                field.Name = "negative";
                field.Type = TType.Bool;
                field.ID = 1;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(Negative);
                oprot.WriteFieldEnd();
            }
            if (FieldName != null && __isset.fieldName)
            {
                field.Name = "fieldName";
                field.Type = TType.String;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(FieldName);
                oprot.WriteFieldEnd();
            }
            if (StringValues != null && __isset.stringValues)
            {
                field.Name = "stringValues";
                field.Type = TType.List;
                field.ID = 3;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.String, StringValues.Count));
                    foreach (string _iter6 in StringValues)
                    {
                        oprot.WriteString(_iter6);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (Prefix != null && __isset.prefix)
            {
                field.Name = "prefix";
                field.Type = TType.String;
                field.ID = 4;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(Prefix);
                oprot.WriteFieldEnd();
            }
            if (Hierarchy != null && __isset.hierarchy)
            {
                field.Name = "hierarchy";
                field.Type = TType.List;
                field.ID = 5;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.String, Hierarchy.Count));
                    foreach (string _iter7 in Hierarchy)
                    {
                        oprot.WriteString(_iter7);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (RangeFrom != null && __isset.rangeFrom)
            {
                field.Name = "rangeFrom";
                field.Type = TType.String;
                field.ID = 6;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(RangeFrom);
                oprot.WriteFieldEnd();
            }
            if (__isset.rangeFromInclusive)
            {
                field.Name = "rangeFromInclusive";
                field.Type = TType.Bool;
                field.ID = 7;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(RangeFromInclusive);
                oprot.WriteFieldEnd();
            }
            if (RangeTo != null && __isset.rangeTo)
            {
                field.Name = "rangeTo";
                field.Type = TType.String;
                field.ID = 8;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(RangeTo);
                oprot.WriteFieldEnd();
            }
            if (__isset.rangeToInclusive)
            {
                field.Name = "rangeToInclusive";
                field.Type = TType.Bool;
                field.ID = 9;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(RangeToInclusive);
                oprot.WriteFieldEnd();
            }
            if (HierarchyId != null && __isset.hierarchyId)
            {
                field.Name = "hierarchyId";
                field.Type = TType.String;
                field.ID = 41;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(HierarchyId);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("Filter(");
            bool __first = true;
            if (__isset.negative)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Negative: ");
                __sb.Append(Negative);
            }
            if (FieldName != null && __isset.fieldName)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("FieldName: ");
                __sb.Append(FieldName);
            }
            if (StringValues != null && __isset.stringValues)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("StringValues: ");
                __sb.Append(StringValues);
            }
            if (Prefix != null && __isset.prefix)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Prefix: ");
                __sb.Append(Prefix);
            }
            if (HierarchyId != null && __isset.hierarchyId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("HierarchyId: ");
                __sb.Append(HierarchyId);
            }
            if (Hierarchy != null && __isset.hierarchy)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Hierarchy: ");
                __sb.Append(Hierarchy);
            }
            if (RangeFrom != null && __isset.rangeFrom)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeFrom: ");
                __sb.Append(RangeFrom);
            }
            if (__isset.rangeFromInclusive)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeFromInclusive: ");
                __sb.Append(RangeFromInclusive);
            }
            if (RangeTo != null && __isset.rangeTo)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeTo: ");
                __sb.Append(RangeTo);
            }
            if (__isset.rangeToInclusive)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeToInclusive: ");
                __sb.Append(RangeToInclusive);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}