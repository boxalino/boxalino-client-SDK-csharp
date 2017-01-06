using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;

namespace boxalino_client_SDK_CSharp.Services
{
    public class PropertyHit : TBase
    {
        /**
   * @var string
   */
        private string _value = string.Empty;
        public string value
        {
            get
            {
                return _value;
            }
            set
            {
                this._value = value;
            }
        }
        /**
         * @var string
         */
        private string _label = string.Empty;
        public string label
        {
            get
            {
                return _label;
            }
            set
            {
                this._label = value;
            }
        }
        /**
         * @var int
         */
        private long _totalHitCount = 0;
        public long totalHitCount
        {
            get
            {
                return _totalHitCount;
            }
            set
            {
                this._totalHitCount = value;
            }
        }


        public string getName()
        {
            return "PropertyHit";
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
                        if (field.Type == TType.String)
                        {
                            this.value = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 21:
                        if (field.Type == TType.String)
                        {
                            this.label = iprot.ReadString();
                        }
                        else
                        {
                            TProtocolUtil.Skip(iprot, field.Type);
                        }
                        break;
                    case 31:
                        if (field.Type == TType.I64)
                        {
                            this.totalHitCount = iprot.ReadI64();
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
        public void Write(TProtocol oprot)
        {
            TStruct struc = new TStruct("PropertyHit");
            oprot.WriteStructBegin(struc);
            TField field = new TField();
            if (this.value != null)
            {
                field.Name = "value";
                field.Type = TType.String;
                field.ID = 11;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(this.value);
                oprot.WriteFieldEnd();

            }
            if (this.label != null)
            {

                field.Name = "label";
                field.Type = TType.String;
                field.ID = 21;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(this.label);
                oprot.WriteFieldEnd();


            }
            if (this.totalHitCount != null)
            {

                field.Name = "totalHitCount";
                field.Type = TType.I64;
                field.ID = 31;
                oprot.WriteFieldBegin(field);
                oprot.WriteString(this.label);
                oprot.WriteFieldEnd();


            }
            oprot.WriteFieldStop();
            oprot.WriteStructEnd();

        }

    }
}
