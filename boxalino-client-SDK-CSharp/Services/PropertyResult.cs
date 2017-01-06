using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;

namespace boxalino_client_SDK_CSharp.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Thrift.Protocol.TBase" />
    public class PropertyResult : TBase
    {
        private List<PropertyHit> _hits;
        public string _name = string.Empty;
        public List<PropertyHit> Hits
        {
            get
            {
                return _hits;
            }
            set
            {
                this._hits = value;
            }
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return "PropertyResult";
        }

        /// <summary>
        /// Reads the specified iprot.
        /// </summary>
        /// <param name="iprot">The iprot.</param>
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
                        if (field.Type == TType.List)
                        {
                            this.Hits = new List<PropertyHit>();
                            int _size246 = 0;
                            TList _list250 = iprot.ReadListBegin();
                            for (int _i250 = 0; _i250 < _size246; ++_i250)
                            {
                                PropertyHit elem251 = null;
                                elem251 = new PropertyHit();
                                elem251.Read(iprot);
                                this.Hits.Add(elem251);
                            }
                            iprot.ReadListEnd();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 21:
                        if (field.Type == TType.String)
                        {
                            iprot.ReadString();
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

        /// <summary>
        /// Writes the specified oprot.
        /// </summary>
        /// <param name="oprot">The oprot.</param>
        /// <exception cref="TProtocolException">Bad type in structure." + TProtocolException.INVALID_DATA</exception>
        public void Write(TProtocol oprot)
        {
            TStruct struc = new TStruct("PropertyResult");
            oprot.WriteStructBegin(struc);

            if (this.Hits != null)
            {
                if (this.Hits.GetType() != typeof(List<PropertyHit>))
                {
                    throw new TProtocolException("Bad type in structure." + TProtocolException.INVALID_DATA);
                }
                TField field = new TField();
                field.Name = "hits";
                field.Type = TType.List;
                field.ID = 11;

                oprot.WriteFieldBegin(field);
                {
                    oprot.WriteListBegin(new TList(TType.Struct, this.Hits.Count));
                    foreach (PropertyHit _iter252 in this.Hits)
                    {
                        _iter252.Write(oprot);
                    }
                    oprot.WriteListEnd();
                }
                oprot.WriteFieldEnd();

            }

            if (this.name != null)
            {
                TField field = new TField();
                field.Name = "name";
                field.Type = TType.String;
                field.ID = 21;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(this.name);

                oprot.WriteFieldEnd();
            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();


        }
    }
}