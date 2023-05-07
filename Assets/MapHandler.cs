using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Models;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public Material RoadMaterial;

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

        // latitude is Y
        // Longitude is X

        var latmin = osmXml.node.Min(x => x.lat);
        var latmax = osmXml.node.Max(x => x.lat);
        var longmin = osmXml.node.Min(x => x.lon);
        var longmax = osmXml.node.Max(x => x.lon);

        var latTotalHalf = (latmax - latmin) / 2;
        var longTotalHalf = (longmax - longmin) / 2;

        var xFtMax = longmax * 364_000;
        var xFtMin = longmin * 364_000;
        var yFtMax = latmax * 364_000;
        var yFtMin = latmin * 364_000;

        var xLengthInFeet = (latmax - latmin) * 364_000;
        var yLengthInFeet = (longmax - longmin) * 364_000;
        var oneDegree = 9100;
        Debug.Log($"Length of Total X: {xLengthInFeet}");

        foreach (var way in osmXml.way)
        {
            if (way.tag.Any(x => x.v == "path" ||
                                 x.v == "service" ||
                                 x.v == "unclassified" ||
                                 x.v == "footway" ||
                                 x.v == "cycleway" ||
                                 x.v == "private" ||
                                 x.v == "driveway" ||
                                 x.k == "access" ||
                                 x.k == "leisure" ||
                                 x.k == "amenity"
            ))
            {
                continue;
            }
            //create an empty game object
            GameObject go = new GameObject();
            // add line renderer component
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.material = RoadMaterial;
            lr.numCornerVertices = 5;

            //set line segments to nd.count
            lr.positionCount = way.nd.Length;
            lr.SetWidth(0.5f, .5f);
            //lr.widthCurve = new AnimationCurve();
            //lr.widthMultiplier = 0.1f;
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
                var scaledY = (node.lat - latmin - latTotalHalf) * 9100;            // (latmax - latmin) * (100);
                var scaledX = (node.lon - longmin - longTotalHalf) * 9100;         //(longmax - longmin) * (100);
                //var scaledX = node.lat;
                //var scaledY = node.lon;
                lr.SetPosition(count, new Vector2((float)scaledX, (float)scaledY));
                count++;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}