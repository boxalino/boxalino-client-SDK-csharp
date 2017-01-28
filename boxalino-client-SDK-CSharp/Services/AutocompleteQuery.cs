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
    public partial class AutocompleteQuery : TBase
    {
        private string _indexId;
        private string _language;
        private string _queryText;
        private int _suggestionsHitCount;
        private bool _highlight;
        private string _highlightPre;
        private string _highlightPost;

        public string IndexId
        {
            get
            {
                return _indexId;
            }
            set
            {
                __isset.indexId = true;
                this._indexId = value;
            }
        }

        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                __isset.language = true;
                this._language = value;
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

        public int SuggestionsHitCount
        {
            get
            {
                return _suggestionsHitCount;
            }
            set
            {
                __isset.suggestionsHitCount = true;
                this._suggestionsHitCount = value;
            }
        }

        public bool Highlight
        {
            get
            {
                return _highlight;
            }
            set
            {
                __isset.highlight = true;
                this._highlight = value;
            }
        }

        public string HighlightPre
        {
            get
            {
                return _highlightPre;
            }
            set
            {
                __isset.highlightPre = true;
                this._highlightPre = value;
            }
        }

        public string HighlightPost
        {
            get
            {
                return _highlightPost;
            }
            set
            {
                __isset.highlightPost = true;
                this._highlightPost = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool indexId;
            public bool language;
            public bool queryText;
            public bool suggestionsHitCount;
            public bool highlight;
            public bool highlightPre;
            public bool highlightPost;
        }

        public AutocompleteQuery()
        {
            this._highlightPre = "<em>";
            this.__isset.highlightPre = true;
            this._highlightPost = "</em>";
            this.__isset.highlightPost = true;
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
                            if (field.Type == TType.String)
                            {
                                IndexId = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 21:
                            if (field.Type == TType.String)
                            {
                                Language = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 31:
                            if (field.Type == TType.String)
                            {
                                QueryText = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 41:
                            if (field.Type == TType.I32)
                            {
                                SuggestionsHitCount = iprot.ReadI32();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 51:
                            if (field.Type == TType.Bool)
                            {
                                Highlight = iprot.ReadBool();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 61:
                            if (field.Type == TType.String)
                            {
                                HighlightPre = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 71:
                            if (field.Type == TType.String)
                            {
                                HighlightPost = iprot.ReadString();
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
                TStruct struc = new TStruct("AutocompleteQuery");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (IndexId != null && __isset.indexId)
                {
                    field.Name = "indexId";
                    field.Type = TType.String;
                    field.ID = 11;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(IndexId);
                    oprot.WriteFieldEnd();
                }
                if (Language != null && __isset.language)
                {
                    field.Name = "language";
                    field.Type = TType.String;
                    field.ID = 21;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(Language);
                    oprot.WriteFieldEnd();
                }
                if (QueryText != null && __isset.queryText)
                {
                    field.Name = "queryText";
                    field.Type = TType.String;
                    field.ID = 31;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(QueryText);
                    oprot.WriteFieldEnd();
                }
                if (__isset.suggestionsHitCount)
                {
                    field.Name = "suggestionsHitCount";
                    field.Type = TType.I32;
                    field.ID = 41;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteI32(SuggestionsHitCount);
                    oprot.WriteFieldEnd();
                }
                if (__isset.highlight)
                {
                    field.Name = "highlight";
                    field.Type = TType.Bool;
                    field.ID = 51;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteBool(Highlight);
                    oprot.WriteFieldEnd();
                }
                if (HighlightPre != null && __isset.highlightPre)
                {
                    field.Name = "highlightPre";
                    field.Type = TType.String;
                    field.ID = 61;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(HighlightPre);
                    oprot.WriteFieldEnd();
                }
                if (HighlightPost != null && __isset.highlightPost)
                {
                    field.Name = "highlightPost";
                    field.Type = TType.String;
                    field.ID = 71;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(HighlightPost);
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
            StringBuilder __sb = new StringBuilder("AutocompleteQuery(");
            bool __first = true;
            if (IndexId != null && __isset.indexId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("IndexId: ");
                __sb.Append(IndexId);
            }
            if (Language != null && __isset.language)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Language: ");
                __sb.Append(Language);
            }
            if (QueryText != null && __isset.queryText)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("QueryText: ");
                __sb.Append(QueryText);
            }
            if (__isset.suggestionsHitCount)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SuggestionsHitCount: ");
                __sb.Append(SuggestionsHitCount);
            }
            if (__isset.highlight)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Highlight: ");
                __sb.Append(Highlight);
            }
            if (HighlightPre != null && __isset.highlightPre)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("HighlightPre: ");
                __sb.Append(HighlightPre);
            }
            if (HighlightPost != null && __isset.highlightPost)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("HighlightPost: ");
                __sb.Append(HighlightPost);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}