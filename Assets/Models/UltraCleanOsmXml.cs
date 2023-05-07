using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class UltraCleanOsmXml
    {

        // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class osm
        {

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("way")]
            public osmWay[] Ways { get; set; }


            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("node")]
            public osmNode[] Nodes { get; set; }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmWay
        {
            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("nd")]
            public osmWayND[] WayNodes { get; set; }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("tag")]
            public osmWayTag[] Tags { get; set; }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("id")]
            public uint WayId { get; set; }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmWayND
        {
            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("ref")]
            public uint NodeId { get; set; }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmWayTag
        {

            private string kField;

            private string vField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("k")]
            //[System.Xml.Serialization.XmlEnum()]
            public string k { get; set; }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string v
            {
                get
                {
                    return this.vField;
                }
                set
                {
                    this.vField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmNode
        {

            private uint idField;

            private decimal latField;

            private decimal lonField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal lat
            {
                get
                {
                    return this.latField;
                }
                set
                {
                    this.latField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal lon
            {
                get
                {
                    return this.lonField;
                }
                set
                {
                    this.lonField = value;
                }
            }
        }


    }
}
