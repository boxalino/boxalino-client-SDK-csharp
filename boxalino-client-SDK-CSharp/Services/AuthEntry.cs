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
    public partial class AuthEntry : TBase
    {
        private string _username;
        private string _apiKey;
        private string _apiSecret;
        private List<string> _solrIndexPatterns;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                __isset.username = true;
                this._username = value;
            }
        }

        public string ApiKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                __isset.apiKey = true;
                this._apiKey = value;
            }
        }

        public string ApiSecret
        {
            get
            {
                return _apiSecret;
            }
            set
            {
                __isset.apiSecret = true;
                this._apiSecret = value;
            }
        }

        public List<string> SolrIndexPatterns
        {
            get
            {
                return _solrIndexPatterns;
            }
            set
            {
                __isset.solrIndexPatterns = true;
                this._solrIndexPatterns = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool username;
            public bool apiKey;
            public bool apiSecret;
            public bool solrIndexPatterns;
        }

        public AuthEntry()
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
                            if (field.Type == TType.String)
                            {
                                Username = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 21:
                            if (field.Type == TType.String)
                            {
                                ApiKey = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 31:
                            if (field.Type == TType.String)
                            {
                                ApiSecret = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 41:
                            if (field.Type == TType.List)
                            {
                                {
                                    SolrIndexPatterns = new List<string>();
                                    TList _list19 = iprot.ReadListBegin();
                                    for (int _i20 = 0; _i20 < _list19.Count; ++_i20)
                                    {
                                        string _elem21;
                                        _elem21 = iprot.ReadString();
                                        SolrIndexPatterns.Add(_elem21);
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
                TStruct struc = new TStruct("AuthEntry");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (Username != null && __isset.username)
                {
                    field.Name = "username";
                    field.Type = TType.String;
                    field.ID = 11;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(Username);
                    oprot.WriteFieldEnd();
                }
                if (ApiKey != null && __isset.apiKey)
                {
                    field.Name = "apiKey";
                    field.Type = TType.String;
                    field.ID = 21;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(ApiKey);
                    oprot.WriteFieldEnd();
                }
                if (ApiSecret != null && __isset.apiSecret)
                {
                    field.Name = "apiSecret";
                    field.Type = TType.String;
                    field.ID = 31;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(ApiSecret);
                    oprot.WriteFieldEnd();
                }
                if (SolrIndexPatterns != null && __isset.solrIndexPatterns)
                {
                    field.Name = "solrIndexPatterns";
                    field.Type = TType.List;
                    field.ID = 41;
                    oprot.WriteFieldBegin(field);
                    {
                        oprot.WriteListBegin(new TList(TType.String, SolrIndexPatterns.Count));
                        foreach (string _iter22 in SolrIndexPatterns)
                        {
                            oprot.WriteString(_iter22);
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
            StringBuilder __sb = new StringBuilder("AuthEntry(");
            bool __first = true;
            if (Username != null && __isset.username)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Username: ");
                __sb.Append(Username);
            }
            if (ApiKey != null && __isset.apiKey)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ApiKey: ");
                __sb.Append(ApiKey);
            }
            if (ApiSecret != null && __isset.apiSecret)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ApiSecret: ");
                __sb.Append(ApiSecret);
            }
            if (SolrIndexPatterns != null && __isset.solrIndexPatterns)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SolrIndexPatterns: ");
                __sb.Append(SolrIndexPatterns);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}