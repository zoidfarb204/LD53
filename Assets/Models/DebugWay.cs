using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DebugWay : MonoBehaviour
{

    public List<DebugNode> Nodes;
    public long WayId;
    public string Name;
    public string County;
    public string ZipCode;
    public int MaxSpeed;
    public string Type;
    public int Lanes;

    public DebugWay()
    {

    }

    public static void BuildDebugWay(DebugWay debugWay, OsmXmlCustom.osmWay osmWay)
    {
        debugWay.WayId = osmWay.WayId;
        debugWay.Name = osmWay.Name;
        debugWay.County = osmWay.County;
        debugWay.ZipCode = osmWay.ZipCode;
        debugWay.MaxSpeed = osmWay.MaxSpeed;
        debugWay.Type = osmWay.Type;
        debugWay.Lanes = osmWay.Lanes;
        debugWay.Nodes = DebugNode.DebugNodeList(osmWay.WayNodes);
    }
}

public class DebugNode //: MonoBehaviour
{
    public long NodeId;

    public decimal Latitude;

    public decimal Longitude;

    public DebugNode(OsmXmlCustom.osmNode osmNode)
    {
        NodeId = osmNode.NodeId;
        Latitude = osmNode.lat;
        Longitude = osmNode.lon;
    }

    public static List<DebugNode> DebugNodeList(List<OsmXmlCustom.osmNode> osmNodeList)
    {
        var debugNodes = new List<DebugNode>();
        foreach (var node in osmNodeList)
        {
            debugNodes.Add(new DebugNode(node));
        }
        return debugNodes;
    }
}
