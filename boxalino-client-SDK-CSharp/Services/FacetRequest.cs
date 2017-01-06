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
    /// <dd>name of the field to get facet for</dd>
    /// 
    /// <dt>numerical</dt>
    /// <dd>whether the facet is numerical</dd>
    /// 
    /// <dt>range</dt>
    /// <dd>whether the facet is range facet</dd>
    /// 
    /// <dt>maxCount</dt>
    /// <dd>maximum number of facets to return by given order, -1 for all of them</dd>
    /// 
    /// <dt>minPopulation</dt>
    /// <dd>minimum facet population to return</dd>
    /// 
    /// <dt>dateRangeGap</dt>
    /// <dd>if the corresponding field is date then the gap to be used for facet</dd>
    /// 
    /// <dt>sortOrder</dt>
    /// <dd>sort order</dd>
    /// 
    /// <dt>sortAscending</dt>
    /// <dd>whether the sort should be done ascending</dd>
    /// 
    /// <dt>selectedValues</dt>
    /// <dd>values selected from the facet.</dd>
    /// <dd>Note that results will be filtered by these values, but the corresponding
    /// FacetResponse is as if this filter was not applied</dd>
    /// 
    /// <dt>andSelectedValues</dt>
    /// <dd>whether selectedValues should be considered in AND logic, meaning filter
    /// out those that don't contain ALL selected values - default is OR - include
    /// those contianing any of selectedValue</dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class FacetRequest : TBase
    {
        private string _fieldName;
        private bool _numerical;
        private bool _range;
        private int _maxCount;
        private int _minPopulation;
        private DateRangeGap _dateRangeGap;
        private FacetSortOrder _sortOrder;
        private bool _sortAscending;
        private List<FacetValue> _selectedValues;
        private bool _andSelectedValues;
        private bool _boundsOnly;
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

        public bool Numerical
        {
            get
            {
                return _numerical;
            }
            set
            {
                __isset.numerical = true;
                this._numerical = value;
            }
        }

        public bool Range
        {
            get
            {
                return _range;
            }
            set
            {
                __isset.range = true;
                this._range = value;
            }
        }

        public int MaxCount
        {
            get
            {
                return _maxCount;
            }
            set
            {
                __isset.maxCount = true;
                this._maxCount = value;
            }
        }

        public int MinPopulation
        {
            get
            {
                return _minPopulation;
            }
            set
            {
                __isset.minPopulation = true;
                this._minPopulation = value;
            }
        }

        /// <summary>
        /// 
        /// <seealso cref="DateRangeGap"/>
        /// </summary>
        public DateRangeGap DateRangeGap
        {
            get
            {
                return _dateRangeGap;
            }
            set
            {
                __isset.dateRangeGap = true;
                this._dateRangeGap = value;
            }
        }

        /// <summary>
        /// 
        /// <seealso cref="FacetSortOrder"/>
        /// </summary>
        public FacetSortOrder SortOrder
        {
            get
            {
                return _sortOrder;
            }
            set
            {
                __isset.sortOrder = true;
                this._sortOrder = value;
            }
        }

        public bool SortAscending
        {
            get
            {
                return _sortAscending;
            }
            set
            {
                __isset.sortAscending = true;
                this._sortAscending = value;
            }
        }

        public List<FacetValue> SelectedValues
        {
            get
            {
                return _selectedValues;
            }
            set
            {
                __isset.selectedValues = true;
                this._selectedValues = value;
            }
        }

        public bool AndSelectedValues
        {
            get
            {
                return _andSelectedValues;
            }
            set
            {
                __isset.andSelectedValues = true;
                this._andSelectedValues = value;
            }
        }


        public bool boundsOnly
        {
            get
            {
                return _boundsOnly;
            }
            set
            {
                _boundsOnly = true;
                this._boundsOnly = value;
            }
        }

        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool fieldName;
            public bool numerical;
            public bool range;
            public bool maxCount;
            public bool minPopulation;
            public bool dateRangeGap;
            public bool sortOrder;
            public bool sortAscending;
            public bool selectedValues;
            public bool andSelectedValues;
        }

        public FacetRequest()
        {
            this._maxCount = -1;
            this.__isset.maxCount = true;
            this._minPopulation = 1;
            this.__isset.minPopulation = true;
            this._andSelectedValues = false;
            this.__isset.andSelectedValues = true;
            this._boundsOnly = false;
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
                            Numerical = iprot.ReadBool();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.Bool)
                        {
                            Range = iprot.ReadBool();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.I32)
                        {
                            MaxCount = iprot.ReadI32();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 5:
                        if (field.Type == TType.I32)
                        {
                            MinPopulation = iprot.ReadI32();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 6:
                        if (field.Type == TType.I32)
                        {
                            DateRangeGap = (DateRangeGap)iprot.ReadI32();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 7:
                        if (field.Type == TType.I32)
                        {
                            SortOrder = (FacetSortOrder)iprot.ReadI32();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 8:
                        if (field.Type == TType.Bool)
                        {
                            SortAscending = iprot.ReadBool();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 90:
                        if (field.Type == TType.List)
                        {
                            {
                                SelectedValues = new List<FacetValue>();
                                TList _list12 = iprot.ReadListBegin();
                                for (int _i13 = 0; _i13 < _list12.Count; ++_i13)
                                {
                                    FacetValue _elem14;
                                    _elem14 = new FacetValue();
                                    _elem14.Read(iprot);
                                    SelectedValues.Add(_elem14);
                                }
                                iprot.ReadListEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 100:
                        if (field.Type == TType.Bool)
                        {
                            AndSelectedValues = iprot.ReadBool();
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
            TStruct struc = new TStruct("FacetRequest");
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
            if (__isset.numerical)
            {
                field.Name = "numerical";
                field.Type = TType.Bool;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(Numerical);
                oprot.WriteFieldEnd();
            }
            if (__isset.range)
            {
                field.Name = "range";
                field.Type = TType.Bool;
                field.ID = 3;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(Range);
                oprot.WriteFieldEnd();
            }
            if (__isset.maxCount)
            {
                field.Name = "maxCount";
                field.Type = TType.I32;
                field.ID = 4;
                oprot.WriteFieldBegin(field);
                oprot.WriteI32(MaxCount);
                oprot.WriteFieldEnd();
            }
            if (__isset.minPopulation)
            {
                field.Name = "minPopulation";
                field.Type = TType.I32;
                field.ID = 5;
                oprot.WriteFieldBegin(field);
                oprot.WriteI32(MinPopulation);
                oprot.WriteFieldEnd();
            }
            if (__isset.dateRangeGap)
            {
                field.Name = "dateRangeGap";
                field.Type = TType.I32;
                field.ID = 6;
                oprot.WriteFieldBegin(field);
                oprot.WriteI32((int)DateRangeGap);
                oprot.WriteFieldEnd();
            }
            if (__isset.sortOrder)
            {
                field.Name = "sortOrder";
                field.Type = TType.I32;
                field.ID = 7;
                oprot.WriteFieldBegin(field);
                oprot.WriteI32((int)SortOrder);
                oprot.WriteFieldEnd();
            }
            if (__isset.sortAscending)
            {
                field.Name = "sortAscending";
                field.Type = TType.Bool;
                field.ID = 8;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(SortAscending);
                oprot.WriteFieldEnd();
            }
            if (SelectedValues != null && __isset.selectedValues)
            {
                field.Name = "selectedValues";
                field.Type = TType.List;
                field.ID = 90;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.Struct, SelectedValues.Count));
                    foreach (FacetValue _iter15 in SelectedValues)
                    {
                        _iter15.Write(oprot);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (__isset.andSelectedValues)
            {
                field.Name = "andSelectedValues";
                field.Type = TType.Bool;
                field.ID = 100;
                oprot.WriteFieldBegin(field);
                oprot.WriteBool(AndSelectedValues);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("FacetRequest(");
            bool __first = true;
            if (FieldName != null && __isset.fieldName)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("FieldName: ");
                __sb.Append(FieldName);
            }
            if (__isset.numerical)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Numerical: ");
                __sb.Append(Numerical);
            }
            if (__isset.range)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Range: ");
                __sb.Append(Range);
            }
            if (__isset.maxCount)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("MaxCount: ");
                __sb.Append(MaxCount);
            }
            if (__isset.minPopulation)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("MinPopulation: ");
                __sb.Append(MinPopulation);
            }
            if (__isset.dateRangeGap)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("DateRangeGap: ");
                __sb.Append(DateRangeGap);
            }
            if (__isset.sortOrder)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SortOrder: ");
                __sb.Append(SortOrder);
            }
            if (__isset.sortAscending)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SortAscending: ");
                __sb.Append(SortAscending);
            }
            if (SelectedValues != null && __isset.selectedValues)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SelectedValues: ");
                __sb.Append(SelectedValues);
            }
            if (__isset.andSelectedValues)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("AndSelectedValues: ");
                __sb.Append(AndSelectedValues);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}
