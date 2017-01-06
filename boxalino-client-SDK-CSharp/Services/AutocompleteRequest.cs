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
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class AutocompleteRequest : TBase
    {
        private UserRecord _userRecord;
        private string _scope;
        private string _choiceId;
        private string _profileId;
        private RequestContext _requestContext;
        private THashSet<string> _excludeVariantIds;
        private AutocompleteQuery _autocompleteQuery;
        private string _searchChoiceId;
        private SimpleSearchQuery _searchQuery;
        private List<PropertyQuery> _propertyQueries;

        public List<PropertyQuery> PropertyQueries
        {
            get
            {
                return _propertyQueries;
            }
            set
            {
                this._propertyQueries = value;
            }
        }
        public UserRecord UserRecord
        {
            get
            {
                return _userRecord;
            }
            set
            {
                __isset.userRecord = true;
                this._userRecord = value;
            }
        }

        public string Scope
        {
            get
            {
                return _scope;
            }
            set
            {
                __isset.scope = true;
                this._scope = value;
            }
        }

        public string ChoiceId
        {
            get
            {
                return _choiceId;
            }
            set
            {
                __isset.choiceId = true;
                this._choiceId = value;
            }
        }

        public string ProfileId
        {
            get
            {
                return _profileId;
            }
            set
            {
                __isset.profileId = true;
                this._profileId = value;
            }
        }

        public RequestContext RequestContext
        {
            get
            {
                return _requestContext;
            }
            set
            {
                __isset.requestContext = true;
                this._requestContext = value;
            }
        }

        public THashSet<string> ExcludeVariantIds
        {
            get
            {
                return _excludeVariantIds;
            }
            set
            {
                __isset.excludeVariantIds = true;
                this._excludeVariantIds = value;
            }
        }

        public AutocompleteQuery AutocompleteQuery
        {
            get
            {
                return _autocompleteQuery;
            }
            set
            {
                __isset.autocompleteQuery = true;
                this._autocompleteQuery = value;
            }
        }

        public string SearchChoiceId
        {
            get
            {
                return _searchChoiceId;
            }
            set
            {
                __isset.searchChoiceId = true;
                this._searchChoiceId = value;
            }
        }

        public SimpleSearchQuery SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                __isset.searchQuery = true;
                this._searchQuery = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool userRecord;
            public bool scope;
            public bool choiceId;
            public bool profileId;
            public bool requestContext;
            public bool excludeVariantIds;
            public bool autocompleteQuery;
            public bool searchChoiceId;
            public bool searchQuery;
        }

        public AutocompleteRequest()
        {
            this._scope = "system_rec";
            this.__isset.scope = true;
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
                    case 11:
                        if (field.Type == TType.Struct)
                        {
                            UserRecord = new UserRecord();
                            UserRecord.Read(iprot);
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 21:
                        if (field.Type == TType.String)
                        {
                            Scope = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 31:
                        if (field.Type == TType.String)
                        {
                            ChoiceId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 41:
                        if (field.Type == TType.String)
                        {
                            ProfileId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 51:
                        if (field.Type == TType.Struct)
                        {
                            RequestContext = new RequestContext();
                            RequestContext.Read(iprot);
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 61:
                        if (field.Type == TType.Set)
                        {
                            {
                                ExcludeVariantIds = new THashSet<string>();
                                TSet _set118 = iprot.ReadSetBegin();
                                for (int _i119 = 0; _i119 < _set118.Count; ++_i119)
                                {
                                    string _elem120;
                                    _elem120 = iprot.ReadString();
                                    ExcludeVariantIds.Add(_elem120);
                                }
                                iprot.ReadSetEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 71:
                        if (field.Type == TType.Struct)
                        {
                            AutocompleteQuery = new AutocompleteQuery();
                            AutocompleteQuery.Read(iprot);
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 81:
                        if (field.Type == TType.String)
                        {
                            SearchChoiceId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 91:
                        if (field.Type == TType.Struct)
                        {
                            SearchQuery = new SimpleSearchQuery();
                            SearchQuery.Read(iprot);
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
            TStruct struc = new TStruct("AutocompleteRequest");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (UserRecord != null && __isset.userRecord)
            {
                field.Name = "userRecord";
                field.Type = TType.Struct;
                field.ID = 11;
                oprot.WriteFieldBegin(field);
                UserRecord.Write(oprot);
                oprot.WriteFieldEnd();
            }
            if (Scope != null && __isset.scope)
            {
                field.Name = "scope";
                field.Type = TType.String;
                field.ID = 21;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(Scope);
                oprot.WriteFieldEnd();
            }
            if (ChoiceId != null && __isset.choiceId)
            {
                field.Name = "choiceId";
                field.Type = TType.String;
                field.ID = 31;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(ChoiceId);
                oprot.WriteFieldEnd();
            }
            if (ProfileId != null && __isset.profileId)
            {
                field.Name = "profileId";
                field.Type = TType.String;
                field.ID = 41;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(ProfileId);
                oprot.WriteFieldEnd();
            }
            if (RequestContext != null && __isset.requestContext)
            {
                field.Name = "requestContext";
                field.Type = TType.Struct;
                field.ID = 51;
                oprot.WriteFieldBegin(field);
                RequestContext.Write(oprot);
                oprot.WriteFieldEnd();
            }
            if (ExcludeVariantIds != null && __isset.excludeVariantIds)
            {
                field.Name = "excludeVariantIds";
                field.Type = TType.Set;
                field.ID = 61;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteSetBegin(new TSet(TType.String, ExcludeVariantIds.Count));
                    foreach (string _iter121 in ExcludeVariantIds)
                    {
                        oprot.WriteString(_iter121);
                    }
                    oprot.WriteSetEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (AutocompleteQuery != null && __isset.autocompleteQuery)
            {
                field.Name = "autocompleteQuery";
                field.Type = TType.Struct;
                field.ID = 71;
                oprot.WriteFieldBegin(field);
                AutocompleteQuery.Write(oprot);
                oprot.WriteFieldEnd();
            }
            if (SearchChoiceId != null && __isset.searchChoiceId)
            {
                field.Name = "searchChoiceId";
                field.Type = TType.String;
                field.ID = 81;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(SearchChoiceId);
                oprot.WriteFieldEnd();
            }
            if (SearchQuery != null && __isset.searchQuery)
            {
                field.Name = "searchQuery";
                field.Type = TType.Struct;
                field.ID = 91;
                oprot.WriteFieldBegin(field);
                SearchQuery.Write(oprot);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("AutocompleteRequest(");
            bool __first = true;
            if (UserRecord != null && __isset.userRecord)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("UserRecord: ");
                __sb.Append(UserRecord == null ? "<null>" : UserRecord.ToString());
            }
            if (Scope != null && __isset.scope)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Scope: ");
                __sb.Append(Scope);
            }
            if (ChoiceId != null && __isset.choiceId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ChoiceId: ");
                __sb.Append(ChoiceId);
            }
            if (ProfileId != null && __isset.profileId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ProfileId: ");
                __sb.Append(ProfileId);
            }
            if (RequestContext != null && __isset.requestContext)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("RequestContext: ");
                __sb.Append(RequestContext == null ? "<null>" : RequestContext.ToString());
            }
            if (ExcludeVariantIds != null && __isset.excludeVariantIds)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ExcludeVariantIds: ");
                __sb.Append(ExcludeVariantIds);
            }
            if (AutocompleteQuery != null && __isset.autocompleteQuery)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("AutocompleteQuery: ");
                __sb.Append(AutocompleteQuery == null ? "<null>" : AutocompleteQuery.ToString());
            }
            if (SearchChoiceId != null && __isset.searchChoiceId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SearchChoiceId: ");
                __sb.Append(SearchChoiceId);
            }
            if (SearchQuery != null && __isset.searchQuery)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("SearchQuery: ");
                __sb.Append(SearchQuery == null ? "<null>" : SearchQuery.ToString());
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}
