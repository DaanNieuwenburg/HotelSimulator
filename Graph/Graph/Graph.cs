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

        public Graph Convert2dArrayToGraph()
        {
            for (int y = 0; y < hotel.HotelLayout.GetLength(0); y++)
            {
                for (int x = 0; x < hotel.HotelLayout.GetLength(1); x++)
                {
                    if (hotel.HotelLayout[y, x] is Lobby)
                    {
                        lobby = new Rectangle(x * tegelBreedte, hoogte, 150, 90);
                        spriteBatch.Draw(tegelTextureLijst[hotel.HotelLayout[y, x].TextureCode], new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tegelTextureLijst[hotel.HotelLayout[y, x].TextureCode], new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);
                    }
                }
                hoogte = hoogte - 90;
            }
        }
    }
}
