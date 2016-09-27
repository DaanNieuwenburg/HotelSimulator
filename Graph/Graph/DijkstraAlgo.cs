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
            node.Distance = 0;
            while (!visit(node, end))
            {
                node = toVisit.Aggregate((l, r) => l.Distance < r.Distance ? l : r);
            }
            Console.WriteLine(returnShortestPath());
        }

        private bool visit(Node node, Node end)
        {
            Console.WriteLine("Im visiting node: " + node.Name);

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
            foreach (KeyValuePair<Node, int> neighbour in node.Neighbours)
            {
                int newDistance = node.Distance + neighbour.Value;
                if (newDistance < neighbour.Key.Distance)
                {
                    neighbour.Key.Distance = newDistance;
                    neighbour.Key.Previous = node;
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
                path = path + node.Name;
                node = node.Previous;
            }
            return path + Start.Name;
        }
    }
}
