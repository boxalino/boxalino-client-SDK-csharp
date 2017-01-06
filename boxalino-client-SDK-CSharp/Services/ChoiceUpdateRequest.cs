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
    /// Request object for changing the choice, that is changing possible variants
    /// or their random distribution
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class ChoiceUpdateRequest : TBase
    {
        private UserRecord _userRecord;
        private string _choiceId;
        private Dictionary<string, int> _variantIds;

        /// <summary>
        /// user record identifying the client
        /// </summary>
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

        /// <summary>
        /// Identifier of the choice to be changed. If it is not given, a new choice will be created
        /// </summary>
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

        /// <summary>
        /// Map containing variant identifier and corresponding positive integer weight.
        /// If for a choice there is no learned rule which can be applied, weights of
        /// variants will be used for variants random distribution.
        /// Higher weight makes corresponding variant more probable.
        /// </summary>
        public Dictionary<string, int> VariantIds
        {
            get
            {
                return _variantIds;
            }
            set
            {
                __isset.variantIds = true;
                this._variantIds = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool userRecord;
            public bool choiceId;
            public bool variantIds;
        }

        public ChoiceUpdateRequest()
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
                            ChoiceId = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 31:
                        if (field.Type == TType.Map)
                        {
                            {
                                VariantIds = new Dictionary<string, int>();
                                TMap _map126 = iprot.ReadMapBegin();
                                for (int _i127 = 0; _i127 < _map126.Count; ++_i127)
                                {
                                    string _key128;
                                    int _val129;
                                    _key128 = iprot.ReadString();
                                    _val129 = iprot.ReadI32();
                                    VariantIds[_key128] = _val129;
                                }
                                iprot.ReadMapEnd();
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
            TStruct struc = new TStruct("ChoiceUpdateRequest");
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
            if (ChoiceId != null && __isset.choiceId)
            {
                field.Name = "choiceId";
                field.Type = TType.String;
                field.ID = 21;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(ChoiceId);
                oprot.WriteFieldEnd();
            }
            if (VariantIds != null && __isset.variantIds)
            {
                field.Name = "variantIds";
                field.Type = TType.Map;
                field.ID = 31;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteMapBegin(new TMap(TType.String, TType.I32, VariantIds.Count));
                    foreach (string _iter130 in VariantIds.Keys)
                    {
                        oprot.WriteString(_iter130);
                        oprot.WriteI32(VariantIds[_iter130]);
                    }
                    oprot.WriteMapEnd();
                }
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("ChoiceUpdateRequest(");
            bool __first = true;
            if (UserRecord != null && __isset.userRecord)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("UserRecord: ");
                __sb.Append(UserRecord == null ? "<null>" : UserRecord.ToString());
            }
            if (ChoiceId != null && __isset.choiceId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ChoiceId: ");
                __sb.Append(ChoiceId);
            }
            if (VariantIds != null && __isset.variantIds)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("VariantIds: ");
                __sb.Append(VariantIds);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}
