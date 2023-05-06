using System.Collections.Generic;

namespace Models
{
    public class Node
    {
        public ulong Id { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public List<Node> ConnectedNodes { get; set; }
    }
}