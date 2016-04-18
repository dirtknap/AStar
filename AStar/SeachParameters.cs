using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Xml;

namespace AStar
{
    public class SeachParameters
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public bool[,] Map { get; set; }

        public SeachParameters(Point startLocation, Point endLocation, bool[,] map)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            Map = map;
        }
       
    }
}