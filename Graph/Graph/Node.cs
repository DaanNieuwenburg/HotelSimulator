using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Node
    {
        public Dictionary<Node, int> Buren { get; set; }
        public int Afstand { get; set; }
        public Node Vorige { get; set; }
        public string Naam { get; set; }
        public Node(string naam)
        {
            Vorige = null;
            Afstand = Int32.MaxValue / 2;
            Buren = new Dictionary<Node, int>();
            Naam = naam;
        }
    }
}
