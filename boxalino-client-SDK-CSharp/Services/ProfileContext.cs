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
    public partial class ProfileContext : TBase
    {
        private string _profileId;
        private RequestContext _requestContext;

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


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool profileId;
            public bool requestContext;
        }

        public ProfileContext()
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
                            if (field.Type == TType.String)
                            {
                                ProfileId = iprot.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(iprot, field.Type);
                            }
                            break;
                        case 2:
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
                TStruct struc = new TStruct("ProfileContext");
                oprot.WriteStructBegin(struc);
                TField field = new TField();
                if (ProfileId != null && __isset.profileId)
                {
                    field.Name = "profileId";
                    field.Type = TType.String;
                    field.ID = 1;
                    oprot.WriteFieldBegin(field);
                    oprot.WriteString(ProfileId);
                    oprot.WriteFieldEnd();
                }
                if (RequestContext != null && __isset.requestContext)
                {
                    field.Name = "requestContext";
                    field.Type = TType.Struct;
                    field.ID = 2;
                    oprot.WriteFieldBegin(field);
                    RequestContext.Write(oprot);
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
            StringBuilder __sb = new StringBuilder("ProfileContext(");
            bool __first = true;
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
            __sb.Append(")");
            return __sb.ToString();
        }

    }

}