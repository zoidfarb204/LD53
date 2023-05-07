using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace DefaultNamespace
{
    public class AStar
    {
        private Queue<Node> path;

        //given a starting node and ending node what is the fastest path between them
        //use a priority queue to keep track of the nodes to visit
        
        
        public Dictionary<Node,Node> Path(Node start, Node end)
        {
            path = new Queue<Node>();

            var birdFlyDistance = Cost(start, end);
            
            var activeTiles = new List<Node>();
            activeTiles.Add(start);
            var visitedTiles = new Dictionary<Node,Node>();
            
            var g_score = new Dictionary<Node, double>();
            g_score.Add(start, 0);

            var f_score = new Dictionary<Node, double>();
            f_score.Add(start, birdFlyDistance);
            
            while (activeTiles.Any())
            {
                var current = activeTiles.OrderBy(x => f_score[x]).First();
                if(current.Id == end.Id)
                {
                    return visitedTiles;
                }
                
                activeTiles.Remove(current);
                foreach (var connectedNode in current.ConnectedNodes)
                {
                    if (!g_score.ContainsKey(connectedNode))
                    {
                        g_score.Add(connectedNode, double.MaxValue);
                    }

                    double tentative_g_score = g_score[current] + Cost(current, connectedNode);
                    if(tentative_g_score < g_score[connectedNode])
                    {
                        visitedTiles[connectedNode] = current;

                        g_score[connectedNode] = tentative_g_score;
                        f_score[connectedNode] = g_score[connectedNode] + Cost(connectedNode, end);
                        if (!activeTiles.Contains(connectedNode))
                        {
                            activeTiles.Add(connectedNode);
                        }
                    }
                }
            }

            return null;
        }
        

        private double Cost(Node node1, Node node2)
        {
            return Math.Sqrt(
                Math.Pow((double)(node2.X - node1.X),2) 
                + Math.Pow((double)(node2.Y - node1.Y),2));
        }
    }
}