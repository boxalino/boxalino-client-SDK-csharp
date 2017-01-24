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
public partial class AutocompleteHit : TBase
{
  private string _suggestion;
  private string _highlighted;
  private SearchResult _searchResult;
  private double _score;

  public string Suggestion
  {
    get
    {
      return _suggestion;
    }
    set
    {
      __isset.suggestion = true;
      this._suggestion = value;
    }
  }

  public string Highlighted
  {
    get
    {
      return _highlighted;
    }
    set
    {
      __isset.highlighted = true;
      this._highlighted = value;
    }
  }

  public SearchResult SearchResult
  {
    get
    {
      return _searchResult;
    }
    set
    {
      __isset.searchResult = true;
      this._searchResult = value;
    }
  }

  public double Score
  {
    get
    {
      return _score;
    }
    set
    {
      __isset.score = true;
      this._score = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool suggestion;
    public bool highlighted;
    public bool searchResult;
    public bool score;
  }

  public AutocompleteHit() {
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
              Suggestion = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 21:
            if (field.Type == TType.String) {
              Highlighted = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 31:
            if (field.Type == TType.Struct) {
              SearchResult = new SearchResult();
              SearchResult.Read(iprot);
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 41:
            if (field.Type == TType.Double) {
              Score = iprot.ReadDouble();
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
      TStruct struc = new TStruct("AutocompleteHit");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Suggestion != null && __isset.suggestion) {
        field.Name = "suggestion";
        field.Type = TType.String;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Suggestion);
        oprot.WriteFieldEnd();
      }
      if (Highlighted != null && __isset.highlighted) {
        field.Name = "highlighted";
        field.Type = TType.String;
        field.ID = 21;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Highlighted);
        oprot.WriteFieldEnd();
      }
      if (SearchResult != null && __isset.searchResult) {
        field.Name = "searchResult";
        field.Type = TType.Struct;
        field.ID = 31;
        oprot.WriteFieldBegin(field);
        SearchResult.Write(oprot);
        oprot.WriteFieldEnd();
      }
      if (__isset.score) {
        field.Name = "score";
        field.Type = TType.Double;
        field.ID = 41;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Score);
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
    StringBuilder __sb = new StringBuilder("AutocompleteHit(");
    bool __first = true;
    if (Suggestion != null && __isset.suggestion) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Suggestion: ");
      __sb.Append(Suggestion);
    }
    if (Highlighted != null && __isset.highlighted) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Highlighted: ");
      __sb.Append(Highlighted);
    }
    if (SearchResult != null && __isset.searchResult) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("SearchResult: ");
      __sb.Append(SearchResult== null ? "<null>" : SearchResult.ToString());
    }
    if (__isset.score) {
      if(!__first) { __sb.Append(", "); }
      __first = false;
      __sb.Append("Score: ");
      __sb.Append(Score);
    }
    __sb.Append(")");
    return __sb.ToString();
  }

}

