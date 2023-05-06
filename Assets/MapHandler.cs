using System;
using System.Collections.Generic;
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
        var Nodes = new List<Node>();

        foreach (var way in osmXml.way)
        {
            //create an empty game object
            GameObject go = new GameObject();
            // add line renderer component
            LineRenderer lr = go.AddComponent<LineRenderer>();
            //set line segments to nd.count

            lr.positionCount = way.nd.Length;
            lr.SetWidth(0.1f,.1f);
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
                var scaledY = (node.lat - latmin) / (latmax - latmin) * (200);
                var scaledX = (node.lon - longmin) /(longmax - longmin) * (200);
                //var scaledX = node.lat;
                //var scaledY = node.lon;
                lr.SetPosition(count, new Vector2((float)scaledX, (float)scaledY));

                var n = new Node
                {
                    X = scaledX,
                    Y = scaledY,
                    ConnectedNodes = new List<Node>()
                };
                Nodes.Add(n);
                count++;
            }
        }


        var nodeList = GenerateNodes(osmXml);
    }

    private static List<Node> GenerateNodes(OsmXML.osm osmXml)
    {
        var nodeList = new List<Node>();
        foreach (var node in osmXml.node)
        {
            nodeList.Add(new Node
            {
                Id = node.id,
                X = node.lon,
                Y = node.lat,
                ConnectedNodes = new List<Node>()
            });
        }

        foreach (var way in osmXml.way)
        {
            for (int i = 0; i < way.nd.Length; i++)
            {
                var node = nodeList.FirstOrDefault(x => x.Id == way.nd[i].@ref);
                if (node != null)
                {
                    if (i > 0)
                    {
                        var prevNode = nodeList.FirstOrDefault(x => x.Id == way.nd[i - 1].@ref);
                        node.ConnectedNodes.Add(prevNode);
                    }

                    if (i < way.nd.Length - 1)
                    {
                        var nextNode = nodeList.FirstOrDefault(x => x.Id == way.nd[i + 1].@ref);
                        node.ConnectedNodes.Add(nextNode);
                    }
                }
            }
        }

        return nodeList.Where(x => x.Id != 0).ToList();
    }

    // Update is called once per frame
    void Update()
    {
    }
}