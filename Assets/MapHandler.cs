using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Models;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //read xml file into OsmXml class
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(OsmXML.osm));
        
        Debug.Log("Reading file...");
        FileStream fileStream = new FileStream("Assets/closer.osm", FileMode.Open);
        Debug.Log("Deserializing...");
        OsmXML.osm osmXml = (OsmXML.osm) xmlSerializer.Deserialize(fileStream);
        Debug.Log("Done");

        var latmin = osmXml.node.Min(x => x.lat);
        var latmax = osmXml.node.Max(x => x.lat);
        var longmin = osmXml.node.Min(x => x.lon);
        var longmax = osmXml.node.Max(x => x.lon);

        foreach (var way in osmXml.way)
        {
            //create an empty game object
            GameObject go = new GameObject();
            // add line renderer component
            LineRenderer lr = go.AddComponent<LineRenderer>();
            //set line segments to nd.count

            lr.positionCount = way.nd.Length;
            var count = 0;



            foreach (var nd in way.nd)
            {
                var node = osmXml.node.FirstOrDefault(x => x.id == nd.@ref);
                if (node == null)
                {
                    Debug.Log("Null node");
                    continue;
                }
                if(node.lat == 0 || node.lon == 0)
                    continue;
                
                //rescale x and y to fit in -100,100 based on the max and min values
                var scaledX = (node.lat - latmin) / (latmax - latmin) * (100-(-100)) + (-100);
                var scaledY = (node.lon - longmin) /(longmax - longmin) * (100-(-100)) + (-100);
                lr.SetPosition(count, new Vector2((float)scaledX, (float)scaledY));
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}