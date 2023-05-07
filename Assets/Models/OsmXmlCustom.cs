using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Models
{
    public class OsmXmlCustom
    {
        public osm CustomDeserialization(string fileName = "reproduce.xml")
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(OsmXmlCustom.osm));
            xmlSerializer.UnknownElement += new XmlElementEventHandler(this.Deerializer_Tag);

            var fileStream = new FileStream($"Assets/osm-xml/{fileName}", FileMode.Open);
            Console.WriteLine("Deserializing...");
            try
            {
                OsmXmlCustom.osm osmXml = (OsmXmlCustom.osm)xmlSerializer.Deserialize(fileStream);
                Console.WriteLine("Done");
                return osmXml ?? new osm();

            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                throw;
            }

        }

        private void Deerializer_Tag(object sender, XmlElementEventArgs e)
        {
            //if (e.Element.Name == "tag")
            //{
            //    Console.WriteLine($"\t {e.Element.Attributes["k"].Value}: {e.Element.Attributes["v"].Value}");
            //}
            //else
            //{
            //    Console.WriteLine($"Not a Tag - Element Name: {e.Element.Name}");
            //    return;
            //}
            if (e.Element.Name != "tag") return;

            osmWay x = (osmWay)e.ObjectBeingDeserialized;

            switch (e.Element.Attributes["k"].Value)
            {
                case "highway":
                    x.Type = e.Element.Attributes["v"]?.Value ?? "Unknown";
                    break;
                case "name":
                    x.Name = e.Element.Attributes["v"]?.Value ?? "Unknown";
                    break;
                default:
                    //Console.WriteLine($"Attribute Not Defined: {e.Element.Attributes["k"].Value} ");
                    break;
            }


            return;

        }


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
            public List<osmWay> Ways { get; set; } = new List<osmWay>();


            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("node")]
            public List<osmNode> Nodes { get; set; } = new List<osmNode>();
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmWay
        {
            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("id")]
            public long WayId { get; set; }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("nd")]
            public List<osmNode> WayNodes { get; set; } = new List<osmNode>();

            public string Name { get; set; }
            public string County { get; set; }
            public string ZipCode { get; set; }
            public int MaxSpeed { get; set; }
            public string Type { get; set; }
            public int Lanes { get; set; }


            /// <remarks/>
            // [System.Xml.Serialization.XmlElementAttribute("tag")]
            // public osmWayTag[] Tags { get; set; }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmWayND
        {
            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("ref")]
            public long NodeId { get; set; }
        }

        /// <remarks/>
        //[System.SerializableAttribute()]
        //[System.ComponentModel.DesignerCategoryAttribute("code")]
        //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        //public partial class osmWayTag
        //{

        //    private string kField;

        //    private string name;

        //    public string HighwayType { get; private set; }

        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute("k")]
        //    public string k { get; set; }

        //    /// <remarks/>
        //    [System.Xml.Serialization.XmlAttributeAttribute()]
        //    public string v
        //    {
        //        get
        //        {
        //            return this.vField;
        //        }
        //        set
        //        {
        //            this.vField = value;
        //        }
        //    }


        //}

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class osmNode
        {

            private long nodeId;

            private decimal latField;

            private decimal lonField;

            [System.Xml.Serialization.XmlAttribute("ndOrder")]
            public long NdOrder { get; set; }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute("id")]
            public long NodeId
            {
                get
                {
                    return this.nodeId;
                }
                set
                {
                    this.nodeId = value;
                }
            }

            [System.Xml.Serialization.XmlAttributeAttribute("ref")]
            public long NdRefId
            {
                get
                {
                    return this.nodeId;
                }
                set
                {
                    this.nodeId = value;
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
