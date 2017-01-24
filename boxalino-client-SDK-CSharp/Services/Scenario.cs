/**
 * Autogenerated by Thrift Compiler (0.9.3)
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


#if !SILVERLIGHT
[Serializable]
#endif
public partial class Scenario : TBase
{
  private string _id;
  private Dictionary<string, string> _localizedTitles;
  private string _queryP13nScript;
  private int _minHitCount;

  public string Id
  {
    get
    {
      return _id;
    }
    set
    {
      __isset.id = true;
      this._id = value;
    }
  }

  public Dictionary<string, string> LocalizedTitles
  {
    get
    {
      return _localizedTitles;
    }
    set
    {
      __isset.localizedTitles = true;
      this._localizedTitles = value;
    }
  }

  public string QueryP13nScript
  {
    get
    {
      return _queryP13nScript;
    }
    set
    {
      __isset.queryP13nScript = true;
      this._queryP13nScript = value;
    }
  }

  public int MinHitCount
  {
    get
    {
      return _minHitCount;
    }
    set
    {
      __isset.minHitCount = true;
      this._minHitCount = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool id;
    public bool localizedTitles;
    public bool queryP13nScript;
    public bool minHitCount;
  }

  public Scenario() {
  }

  public void Read (TProtocol iprot)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 11:
            if (field.Type == TType.String) {
              Id = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 21:
            if (field.Type == TType.Map) {
              {
                LocalizedTitles = new Dictionary<string, string>();
                TMap _map10 = iprot.ReadMapBegin();
                for( int _i11 = 0; _i11 < _map10.Count; ++_i11)
                {
                  string _key12;
                  string _val13;
                  _key12 = iprot.ReadString();
                  _val13 = iprot.ReadString();
                  LocalizedTitles[_key12] = _val13;
                }
                iprot.ReadMapEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 31:
            if (field.Type == TType.String) {
              QueryP13nScript = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 41:
            if (field.Type == TType.I32) {
              MinHitCount = iprot.ReadI32();
            } else { 
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

  public void Write(TProtocol oprot) {
    oprot.IncrementRecursionDepth();
    try
    {
      TStruct struc = new TStruct("Scenario");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Id != null && __isset.id) {
        field.Name = "id";
        field.Type = TType.String;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Id);
        oprot.WriteFieldEnd();
      }
      if (LocalizedTitles != null && __isset.localizedTitles) {
        field.Name = "localizedTitles";
        field.Type = TType.Map;
        field.ID = 21;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteMapBegin(new TMap(TType.String, TType.String, LocalizedTitles.Count));
          foreach (string _iter14 in LocalizedTitles.Keys)
          {
            oprot.WriteString(_iter14);
            oprot.WriteString(LocalizedTitles[_iter14]);
          }
          oprot.WriteMapEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (QueryP13nScript != null && __isset.queryP13nScript) {
        field.Name = "queryP13nScript";
        field.Type = TType.String;
        field.ID = 31;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(QueryP13nScript);
        oprot.WriteFieldEnd();
      }
      if (__isset.minHitCount) {
        field.Name = "minHitCount";
        field.Type = TType.I32;
        field.ID = 41;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(MinHitCount);
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

  public override string ToString() {
    StringBuilder __sb = new StringBuilder("Scenario(");
    bool __first = true;
    if (Id != null && __isset.id) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Id: ");
      __sb.Append(Id);
    }
    if (LocalizedTitles != null && __isset.localizedTitles) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("LocalizedTitles: ");
      __sb.Append(LocalizedTitles);
    }
    if (QueryP13nScript != null && __isset.queryP13nScript) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("QueryP13nScript: ");
      __sb.Append(QueryP13nScript);
    }
    if (__isset.minHitCount) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("MinHitCount: ");
      __sb.Append(MinHitCount);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}

