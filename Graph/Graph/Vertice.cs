using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Vertice
    {
        public Node FirstNode { get; set; }
        public Node LastNode { get; set; }
        public int Weight { get; set; }

        public Vertice(Node firstNode, Node lastNode, int weight)
        {
            FirstNode = firstNode;
            LastNode = lastNode;
            Weight = weight;
        }
    }
}
