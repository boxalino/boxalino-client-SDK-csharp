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
    /// <dt>stringValue</dt>
    /// <dd>corresponding value of the facet</dd>
    /// 
    /// <dt>rangeFromInclusive</dt>
    /// <dd>if range facets lower bound (inclusive)</dd>
    /// 
    /// <dt>rangeToExclusive</dt>
    /// <dd>if range facets upper bound (inclusive)</dd>
    /// 
    /// <dt>hitCount</dt>
    /// <dd>number of hits found</dd>
    /// 
    /// <dt>hierarchyId</dt>
    /// <dd>id of hierarchy if corresponding field is hierarchical</dd>
    /// 
    /// <dt>hierarchy</dt>
    /// <dd>hierarchy if corresponding field is hierarchical</dd>
    /// 
    /// <dt>selected</dt>
    /// <dd>whether the facet value has been selected in corresponding FacetRequest</dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class FacetValue : TBase
    {
        private string _stringValue;
        private string _rangeFromInclusive;
        private string _rangeToExclusive;
        private long _hitCount;
        private string _hierarchyId;
        private List<string> _hierarchy;
        private bool _selected;

        public string StringValue
        {
            get
            {
                return _stringValue;
            }
            set
            {
                __isset.stringValue = true;
                this._stringValue = value;
            }
        }

        public string RangeFromInclusive
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

        public string RangeToExclusive
        {
            get
            {
                return _rangeToExclusive;
            }
            set
            {
                __isset.rangeToExclusive = true;
                this._rangeToExclusive = value;
            }
        }

        public long HitCount
        {
            get
            {
                return _hitCount;
            }
            set
            {
                __isset.hitCount = true;
                this._hitCount = value;
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

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                __isset.selected = true;
                this._selected = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool stringValue;
            public bool rangeFromInclusive;
            public bool rangeToExclusive;
            public bool hitCount;
            public bool hierarchyId;
            public bool hierarchy;
            public bool selected;
        }

        public FacetValue()
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
                            StringValue = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.String)
                        {
                            RangeFromInclusive = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.String)
                        {
                            RangeToExclusive = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.I64)
                        {
                            HitCount = iprot.ReadI64();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 50:
                        if (field.Type == TType.String)
                        {
                            HierarchyId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 60:
                        if (field.Type == TType.List)
                        {
                            {
                                Hierarchy = new List<string>();
                                TList _list8 = iprot.ReadListBegin();
                                for (int _i9 = 0; _i9 < _list8.Count; ++_i9)
                                {
                                    string _elem10;
                                    _elem10 = iprot.ReadString();
                                    Hierarchy.Add(_elem10);
                                }
                                iprot.ReadListEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 70:
                        if (field.Type == TType.Bool)
                        {
                            Selected = iprot.ReadBool();
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
            TStruct struc = new TStruct("FacetValue");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (StringValue != null && __isset.stringValue)
            {
                field.Name = "stringValue";
                field.Type = TType.String;
                field.ID = 1;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(StringValue);
                oprot.WriteFieldEnd();
            }
            if (RangeFromInclusive != null && __isset.rangeFromInclusive)
            {
                field.Name = "rangeFromInclusive";
                field.Type = TType.String;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(RangeFromInclusive);
                oprot.WriteFieldEnd();
            }
            if (RangeToExclusive != null && __isset.rangeToExclusive)
            {
                field.Name = "rangeToExclusive";
                field.Type = TType.String;
                field.ID = 3;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(RangeToExclusive);
                oprot.WriteFieldEnd();
            }
            if (__isset.hitCount)
            {
                field.Name = "hitCount";
                field.Type = TType.I64;
                field.ID = 4;
                oprot.WriteFieldBegin(field);
                oprot.WriteI64(HitCount);
                oprot.WriteFieldEnd();
            }
            if (HierarchyId != null && __isset.hierarchyId)
            {
                field.Name = "hierarchyId";
                field.Type = TType.String;
                field.ID = 50;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(HierarchyId);
                oprot.WriteFieldEnd();
            }
            if (Hierarchy != null && __isset.hierarchy)
            {
                field.Name = "hierarchy";
                field.Type = TType.List;
                field.ID = 60;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.String, Hierarchy.Count));
                    foreach (string _iter11 in Hierarchy)
                    {
                        oprot.WriteString(_iter11);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (__isset.selected)
            {
                field.Name = "selected";
                field.Type = TType.Bool;
                field.ID = 70;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(Selected);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("FacetValue(");
            bool __first = true;
            if (StringValue != null && __isset.stringValue)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("StringValue: ");
                __sb.Append(StringValue);
            }
            if (RangeFromInclusive != null && __isset.rangeFromInclusive)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeFromInclusive: ");
                __sb.Append(RangeFromInclusive);
            }
            if (RangeToExclusive != null && __isset.rangeToExclusive)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RangeToExclusive: ");
                __sb.Append(RangeToExclusive);
            }
            if (__isset.hitCount)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("HitCount: ");
                __sb.Append(HitCount);
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
            if (__isset.selected)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Selected: ");
                __sb.Append(Selected);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}
