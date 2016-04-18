using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AStar
{
    public class Node
    {

        private Node parentNode;

        public Point Location { get; set; }
        public bool IsWalkable { get; set; }
        //cost from start node
        public float G { get; private set; }
        //stright line cost from this node to end
        public float H { get; private set; }
        //Estimated Total cost to move
        public float F { get { return G + H; } }

        public NodeState State { get; set; }

        public Node ParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
                G = parentNode.G + GetTraversalCost(Location, parentNode.Location);
            }
        }

        public Node(int x, int y, bool isWalkable, Point endLocation)
        {
            Location = new Point(x, y);
            State = NodeState.Untested;
            IsWalkable = isWalkable;
            SetEndPoint(endLocation);
            G = 0;
        }

        public void SetEndPoint(Point endLocation)
        {
            H = GetTraversalCost(Location, endLocation);
        }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}: {State}";
        }

        internal static float GetTraversalCost(Point location, Point destination)
        {
            float deltaX = destination.X - location.X;
            float deltaY = destination.Y - location.Y;
            return (float) Math.Sqrt(deltaX*deltaX + deltaY*deltaY);
        }

         
    }

    public enum NodeState
    {
        Untested,
        Open,
        Closed
    }

}

