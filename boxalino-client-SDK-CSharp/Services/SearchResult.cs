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

    /// <summary>
    /// <dl>
    /// <dt>hits</dt>
    /// <dd>list of hits found for given SimpleSearchQuery</dd>
    /// 
    /// <dt>facetResponses</dt>
    /// <dd>list of requested facets or null if none requested</dd>
    /// 
    /// <dt>totalHitCount</dt>
    /// <dd>total number of hits; -1 in case of mixed recommendation strategy</dd>
    /// 
    /// <dt>queryText</dt>
    /// <dd>relaxation query text for relaxation results or requested queryText for a
    /// regular SearchResult</dd>
    /// 
    /// <dt>hitsGroups</dt>
    /// <dd>grouped hits; not null when corresponding SimplSearchQuery has
    /// groupBy!=null </dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class SearchResult : TBase
    {
        private List<Hit> _hits;
        private List<FacetResponse> _facetResponses;
        private long _totalHitCount;
        private string _queryText;
        private List<HitsGroup> _hitsGroups;

        public List<Hit> Hits
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

        public List<FacetResponse> FacetResponses
        {
            get
            {
                return _facetResponses;
            }
            set
            {
                __isset.facetResponses = true;
                this._facetResponses = value;
            }
        }

        public long TotalHitCount
        {
            get
            {
                return _totalHitCount;
            }
            set
            {
                __isset.totalHitCount = true;
                this._totalHitCount = value;
            }
        }

        public string QueryText
        {
            get
            {
                return _queryText;
            }
            set
            {
                __isset.queryText = true;
                this._queryText = value;
            }
        }

        public List<HitsGroup> HitsGroups
        {
            get
            {
                return _hitsGroups;
            }
            set
            {
                __isset.hitsGroups = true;
                this._hitsGroups = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool hits;
            public bool facetResponses;
            public bool totalHitCount;
            public bool queryText;
            public bool hitsGroups;
        }

        public SearchResult()
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
                        case 1:
                            if (field.Type == TType.List)
                            {
                                {
                                    Hits = new List<Hit>();
                                    TList _list84 = iprot.ReadListBegin();
                                    for (int _i85 = 0; _i85 < _list84.Count; ++_i85)
                                    {
                                        Hit _elem86;
                                        _elem86 = new Hit();
                                        _elem86.Read(iprot);
                                        Hits.Add(_elem86);
                                    }
                                    iprot.ReadListEnd();
                                }
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
                                    FacetResponses = new List<FacetResponse>();
                                    TList _list87 = iprot.ReadListBegin();
                                    for (int _i88 = 0; _i88 < _list87.Count; ++_i88)
                                    {
                                        FacetResponse _elem89;
                                        _elem89 = new FacetResponse();
                                        _elem89.Read(iprot);
                                        FacetResponses.Add(_elem89);
                                    }
                                    iprot.ReadListEnd();
                                }
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 3:
                            if (field.Type == TType.I64)
                            {
                                TotalHitCount = iprot.ReadI64();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 40:
                            if (field.Type == TType.String)
                            {
                                QueryText = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 50:
                            if (field.Type == TType.List)
                            {
                                {
                                    HitsGroups = new List<HitsGroup>();
                                    TList _list90 = iprot.ReadListBegin();
                                    for (int _i91 = 0; _i91 < _list90.Count; ++_i91)
                                    {
                                        HitsGroup _elem92;
                                        _elem92 = new HitsGroup();
                                        _elem92.Read(iprot);
                                        HitsGroups.Add(_elem92);
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
                TStruct struc = new TStruct("SearchResult");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (Hits != null && __isset.hits)
                {
                    field.Name = "hits";
                    field.Type = TType.List;
                    field.ID = 1;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.Struct, Hits.Count));
                        foreach (Hit _iter93 in Hits)
                        {
                            _iter93.Write(oprot);
                        }
                        oprot.WriteListEnd();
                    }
                    oprot.WriteFieldEnd();
                }
                if (FacetResponses != null && __isset.facetResponses)
                {
                    field.Name = "facetResponses";
                    field.Type = TType.List;
                    field.ID = 2;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.Struct, FacetResponses.Count));
                        foreach (FacetResponse _iter94 in FacetResponses)
                        {
                            _iter94.Write(oprot);
                        }
                        oprot.WriteListEnd();
                    }
                    oprot.WriteFieldEnd();
                }
                if (__isset.totalHitCount)
                {
                    field.Name = "totalHitCount";
                    field.Type = TType.I64;
                    field.ID = 3;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteI64(TotalHitCount);
                    oprot.WriteFieldEnd();
                }
                if (QueryText != null && __isset.queryText)
                {
                    field.Name = "queryText";
                    field.Type = TType.String;
                    field.ID = 40;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(QueryText);
                    oprot.WriteFieldEnd();
                }
                if (HitsGroups != null && __isset.hitsGroups)
                {
                    field.Name = "hitsGroups";
                    field.Type = TType.List;
                    field.ID = 50;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.Struct, HitsGroups.Count));
                        foreach (HitsGroup _iter95 in HitsGroups)
                        {
                            _iter95.Write(oprot);
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
            StringBuilder __sb = new StringBuilder("SearchResult(");
            bool __first = true;
            if (Hits != null && __isset.hits)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Hits: ");
                __sb.Append(Hits);
            }
            if (FacetResponses != null && __isset.facetResponses)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("FacetResponses: ");
                __sb.Append(FacetResponses);
            }
            if (__isset.totalHitCount)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("TotalHitCount: ");
                __sb.Append(TotalHitCount);
            }
            if (QueryText != null && __isset.queryText)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("QueryText: ");
                __sb.Append(QueryText);
            }
            if (HitsGroups != null && __isset.hitsGroups)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("HitsGroups: ");
                __sb.Append(HitsGroups);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}