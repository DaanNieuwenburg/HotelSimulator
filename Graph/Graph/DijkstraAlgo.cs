using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class DijkstraAlgo
    {
        public List<Node> toVisit { get; set; }
        public Node End { get; set; }
        public Node Start { get; set; }

        public DijkstraAlgo(Node start, Node end)
        {
            End = end;
            Start = start;
            toVisit = new List<Node>();
            Node node = start;
            node.Afstand = 0;
            while (!visit(node, end))
            {
                node = toVisit.Aggregate((l, r) => l.Afstand < r.Afstand ? l : r);
            }
            Console.WriteLine(returnShortestPath());
        }

        private bool visit(Node node, Node end)
        {
            Console.WriteLine("Im visiting node: " + node.Naam);

            // If node end has been found
            if (node == end)
            {
                return true;
            }

            // If node has already been visited
            if (toVisit.Contains(node))
            {
                toVisit.Remove(node);
            }

            // Visit neighbours
            foreach (KeyValuePair<Node, int> neighbour in node.Buren)
            {
                int newDistance = node.Afstand + neighbour.Value;
                if (newDistance < neighbour.Key.Afstand)
                {
                    neighbour.Key.Afstand = newDistance;
                    neighbour.Key.Vorige = node;
                    toVisit.Add(neighbour.Key);
                }
            }
            return false;
        }

        // Return shortest path
        private string returnShortestPath()
        {
            string path = "";
            Node node = End;
            while (node != Start)
            {
                path = path + node.Naam;
                node = node.Vorige;
            }
            return path + Start.Naam;
        }
    }
}
