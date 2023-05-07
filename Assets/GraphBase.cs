using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphBase : MonoBehaviour
{
    public DijkstraPath Algo = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuildAlgo(List<OsmXmlCustom.osmWay> ways, List<OsmXmlCustom.osmNode> nodes)
    {
        if (Algo == null)
        {
            Algo = new DijkstraPath(ways, nodes);
        }
    }
}


//public class GraphBase
//{
//    public int NumberOfNodes{ get; private set; }
//    public bool IsDirected = false;

//    public GraphBase(int numOfNodes)
//    {
//        this.NumberOfNodes = numOfNodes;
//    }

//}

public class RoadEdge
{
    public RoadVertex VertexA { get; private set; }
    public RoadVertex VertexB { get; private set; }

    public float SegmentWeight { get; private set; }

    public RoadEdge(RoadVertex vertexA, RoadVertex vertexB)
    {
        VertexA = vertexA;
        VertexB = vertexB;

        SegmentWeight = Vector2.Distance(vertexA.Location, vertexB.Location);
    }
}

public class RoadVertex
{
    public long NodeId { get; set; }
    public Vector2 Location { get; private set; }
    //public float Latitude { get; set; }
    //public float Longitude { get; set; }

    public RoadVertex(float latitude, float longitude)
    {
        Location = new Vector2(latitude, longitude);
    }

    public RoadVertex(OsmXmlCustom.osmNode osmNode)
    {
        NodeId = osmNode.NodeId;
        Location = new Vector2((float)osmNode.lon, (float)osmNode.lat);
    }
}

public class DijkstraPath
{
    public Dictionary<long, List<RoadVertex>> Adjacency { get; set; }


    public DijkstraPath(List<OsmXmlCustom.osmWay> ways, List<OsmXmlCustom.osmNode> nodes)
    {
        try
        {
            Adjacency = new Dictionary<long, List<RoadVertex>>();
            foreach (var way in ways)
            {
                for (int i = 1; i < way.WayNodes.Count; i++)
                {
                    if (!Adjacency.ContainsKey(way.WayNodes[i - 1].NodeId))
                    {
                        Adjacency.Add(way.WayNodes[i - 1].NodeId, new List<RoadVertex>());
                    }
                    if (!Adjacency[way.WayNodes[i - 1].NodeId].Any(x => x.NodeId == way.WayNodes[i].NodeId))
                    {
                        Adjacency[way.WayNodes[i - 1].NodeId].Add(new RoadVertex(way.WayNodes[i]));
                    }
                    if (!Adjacency.ContainsKey(way.WayNodes[i].NodeId))
                    {
                        Adjacency.Add(way.WayNodes[i].NodeId, new List<RoadVertex>());
                    }
                    if (!Adjacency[way.WayNodes[i].NodeId].Any(x => x.NodeId == way.WayNodes[i - 1].NodeId))
                    {
                        Adjacency[way.WayNodes[i].NodeId].Add(new RoadVertex(way.WayNodes[i - 1]));
                    }
                }
            }
            Debug.Log("Adjacency List Created Maybe?");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public void Algorithm(List<RoadVertex> vertices)
    {
        var visited = new List<RoadVertex>();
        var unvisited = new List<RoadVertex>(vertices.Count);


    }

}