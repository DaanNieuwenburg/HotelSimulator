using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Node
    {
        public Dictionary<Node, int> Neighbours { get; set; }
        public int Distance { get; set; }
        public Node Previous { get; set; }
        public string Name { get; set; }
        public Node(string name)
        {
            Previous = null;
            Distance = Int32.MaxValue / 2;
            Neighbours = new Dictionary<Node, int>();
            Name = name;
        }
    }
}
