using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DefaultNamespace;
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
        OsmXML.osm osmXml = (OsmXML.osm)xmlSerializer.Deserialize(fileStream);
        Debug.Log("Done");

        var latmin = osmXml.node.Min(x => x.lat);
        var latmax = osmXml.node.Max(x => x.lat);
        var longmin = osmXml.node.Min(x => x.lon);
        var longmax = osmXml.node.Max(x => x.lon);
        
        var nodeList = GenerateNodes(osmXml);

        foreach (var node in nodeList)
        {
            var scaledY = (node.Y - latmin) / (latmax - latmin) * (200);
            var scaledX = (node.X - longmin) / (longmax - longmin) * (200);

            foreach (var connectedNode in node.ConnectedNodes)
            {
                var connectedScaledY = (connectedNode.Y - latmin) / (latmax - latmin) * (200);
                var connectedScaledX = (connectedNode.X - longmin) / (longmax - longmin) * (200);
                //create an empty game object
                GameObject go = new GameObject();
                // add line renderer component
                LineRenderer lr = go.AddComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
                //set line segments to nd.count
                lr.positionCount = 2;
                lr.SetWidth(0.1f, .1f);

                lr.SetPosition(0, new Vector2((float)scaledX, (float)scaledY));
                lr.SetPosition(1, new Vector2((float)connectedScaledX, (float)connectedScaledY));
            }
        }

        //Pick 2 Random Nodes
        var random = new System.Random();
        var startNode = nodeList[random.Next(0, nodeList.Count)];
        var endNode = nodeList[random.Next(0, nodeList.Count)];
        //var startNode = nodeList.FirstOrDefault(x => x.Id == 7115700967);
        //var endNode = nodeList.FirstOrDefault(x => x.Id == 1847104899);
        Debug.Log($"Start Node: {startNode.Id} End Node: {endNode.Id}");

        //Run pathfinding
        var path = new AStar().Path(startNode, endNode);
        if (path == null)
        {
            Debug.Log($"Start Node: {startNode.Id} End Node: {endNode.Id} -- could not find a path");
        }

        //Draw a straight line between the two nodes
        //create an empty game object
        var gameObject = new GameObject();
        gameObject.name = "BirdPath";
        // add line renderer component
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        //set line segments to nd.count
        lineRenderer.positionCount = 2;
        lineRenderer.SetWidth(1f, 1f);
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
        var x1 = (startNode.X - longmin) / (longmax - longmin) * (200);
        var x2 = (endNode.X - longmin) / (longmax - longmin) * (200);
        var y1 = (startNode.Y - latmin) / (latmax - latmin) * (200);
        var y2 = (endNode.Y - latmin) / (latmax - latmin) * (200);
        lineRenderer.SetPosition(0, new Vector2((float)x1, (float)y1));
        lineRenderer.SetPosition(1, new Vector2((float)x2, (float)y2));

        string s = "s";
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
            
            if (way.tag == null || !IsRoadType(way.tag.FirstOrDefault(x => x.k == "highway")?.v))
            {
                continue;
            }

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

        return nodeList.Where(x => x.Id != 0 && x.ConnectedNodes.Count > 0).ToList();
    }

    private static bool IsRoadType(string roadType)
    {

        if (string.IsNullOrEmpty(roadType))
        {
            return false;
        }
        
        if (roadType == "motorway" ||
            roadType == "residential" ||
            roadType == "motorway_link" ||
            roadType == "tertiary" ||
            roadType == "secondary" ||
            roadType == "unclassified" ||
            roadType == "motorway" ||
            roadType == "primary_link" ||
            roadType == "primary")
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}