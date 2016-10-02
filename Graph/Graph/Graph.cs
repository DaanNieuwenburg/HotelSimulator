using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        public List<Node> NodeList { get; set; }
        public List<Vertice> VerticeList { get; set; }

        public Graph()
        {
            NodeList = new List<Node>();
            VerticeList = new List<Vertice>();
        }

        public Graph Convert2dArrayToGraph(Node[,] hotelLayout)
        {
            for (int y = 0; y < hotelLayout.GetLength(0); y++)
            {
                for (int x = 0; x < hotelLayout.GetLength(1); x++)
                {
                    hotelLayout.
                }
            }
            return this;
        }
    }
}
