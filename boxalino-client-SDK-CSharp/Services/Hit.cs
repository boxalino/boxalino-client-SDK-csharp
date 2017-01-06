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
    /// item found
    /// 
    /// <dl>
    /// <dt>values</dt>
    /// <dd>map containing name of the field and list of values as strings</dd>
    /// <dd>if index contains no value for a field, empty array will be returned.</dd>
    /// 
    /// <dt>score</dt>
    /// <dd>index score of the hit</dd>
    /// 
    /// <dt>scenarioId</dt>
    /// <dd>source scenarioId in case of mixed recommendations modes</dd>
    /// </dl>
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    public partial class Hit : TBase
    {
        private Dictionary<string, List<string>> _values;
        private double _score;
        private string _scenarioId;

        public Dictionary<string, List<string>> Values
        {
            get
            {
                return _values;
            }
            set
            {
                __isset.values = true;
                this._values = value;
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

        public string ScenarioId
        {
            get
            {
                return _scenarioId;
            }
            set
            {
                __isset.scenarioId = true;
                this._scenarioId = value;
            }
        }


        public Isset __isset;
#if !SILVERLIGHT
        [Serializable]
#endif
        public struct Isset
        {
            public bool values;
            public bool score;
            public bool scenarioId;
        }

        public Hit()
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
                        if (field.Type == TType.Map)
                        {
                            {
                                Values = new Dictionary<string, List<string>>();
                                TMap _map57 = iprot.ReadMapBegin();
                                for (int _i58 = 0; _i58 < _map57.Count; ++_i58)
                                {
                                    string _key59;
                                    List<string> _val60;
                                    _key59 = iprot.ReadString();
                                    {
                                        _val60 = new List<string>();
                                        TList _list61 = iprot.ReadListBegin();
                                        for (int _i62 = 0; _i62 < _list61.Count; ++_i62)
                                        {
                                            string _elem63;
                                            _elem63 = iprot.ReadString();
                                            _val60.Add(_elem63);
                                        }
                                        iprot.ReadListEnd();
                                    }
                                    Values[_key59] = _val60;
                                }
                                iprot.ReadMapEnd();
                            }
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.Double)
                        {
                            Score = iprot.ReadDouble();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 30:
                        if (field.Type == TType.String)
                        {
                            ScenarioId = iprot.ReadString();
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
            TStruct struc = new TStruct("Hit");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (Values != null && __isset.values)
            {
                field.Name = "values";
                field.Type = TType.Map;
                field.ID = 1;
                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteMapBegin(new TMap(TType.String, TType.List, Values.Count));
                    foreach (string _iter64 in Values.Keys)
                    {
                        oprot.WriteString(_iter64);
                        {
                            oprot.WriteListBegin(new TList(TType.String, Values[_iter64].Count));
                            foreach (string _iter65 in Values[_iter64])
                            {
                                oprot.WriteString(_iter65);
                            }
                            oprot.WriteListEnd();
                        }
                    }
                    oprot.WriteMapEnd();
                }
                oprot.WriteFieldEnd();
            }
            if (__isset.score)
            {
                field.Name = "score";
                field.Type = TType.Double;
                field.ID = 2;
                oprot.WriteFieldBegin(field);
                oprot.WriteDouble(Score);
                oprot.WriteFieldEnd();
            }
            if (ScenarioId != null && __isset.scenarioId)
            {
                field.Name = "scenarioId";
                field.Type = TType.String;
                field.ID = 30;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(ScenarioId);
                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();
        }

        public override string ToString()
        {
            StringBuilder __sb = new StringBuilder("Hit(");
            bool __first = true;
            if (Values != null && __isset.values)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Values: ");
                __sb.Append(Values);
            }
            if (__isset.score)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("Score: ");
                __sb.Append(Score);
            }
            if (ScenarioId != null && __isset.scenarioId)
            {
                if (!__first) { __sb.Append(", "); }
                __first = false;
                __sb.Append("ScenarioId: ");
                __sb.Append(ScenarioId);
            }
            __sb.Append(")");
            return __sb.ToString();
        }

    }
}