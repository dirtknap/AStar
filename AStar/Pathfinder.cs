using System.Collections.Generic;
using System.Drawing;

namespace AStar
{
    public class Pathfinder
    {
        private int width;
        private int height;
        private Node[,] nodes;
        private Node startNode;
        private Node endNode;
        private SeachParameters seachParameters;

        public Pathfinder(SeachParameters seachParameters)
        {
            this.seachParameters = seachParameters;
            InitializeNodes(seachParameters.Map);
            startNode = nodes[seachParameters.StartLocation.X, seachParameters.StartLocation.Y];
            startNode.State = NodeState.Open;
            endNode = nodes[seachParameters.EndLocation.X, seachParameters.EndLocation.Y];
        }

        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();
            bool success = Search(startNode);
            if (success)
            {
                Node node = endNode;
                while (node.ParentNode != null)
                {
                    path.Add(node.Location);
                    node = node.ParentNode;
                }
                path.Reverse();
            }
            return path;
        }

        private void InitializeNodes(bool[,] map)
        {
            width = map.GetLength(0);
            height = map.GetLength(1);
            nodes = new Node[width,height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[x,y] = new Node(x, y, map[x,y], seachParameters.EndLocation);
                }
            }

        }

        private bool Search(Node currentNode)
        {
            currentNode.State = NodeState.Closed;
            var nextNodes = GetAdjacentWalkableNode(currentNode);
            nextNodes.Sort((n1, n2) => n1.F.CompareTo(n2.F));
            foreach (var nextNode in nextNodes)
            {
                if (nextNode.Location == endNode.Location) return true;

                if (Search(nextNode)) return true;
            }
            return false;
        }

        private List<Node> GetAdjacentWalkableNode(Node fromNode)
        {
            List<Node> walkableNodes = new List<Node>();
            IEnumerable<Point> nextLocations = GetAdjacentLocations(fromNode.Location);

            foreach (var location in nextLocations)
            {
                int x = location.X;
                int y = location.Y;

                if (x < 0 || x >= width || y < 0 || y >= height) continue;

                var node = nodes[x, y];

                if (!node.IsWalkable) continue;

                if (node.State == NodeState.Closed) continue;

                if (node.State == NodeState.Open)
                {
                    float traversalCost = Node.GetTraversalCost(node.Location, node.ParentNode.Location);
                    float gTemp = fromNode.G + traversalCost;
                    if (gTemp < node.G)
                    {
                        node.ParentNode = fromNode;
                        walkableNodes.Add(node);
                    }
                }
                else
                {
                    node.ParentNode = fromNode;
                    node.State = NodeState.Open;
                    walkableNodes.Add(node);
                }
            }
            return walkableNodes;
        }

        private IEnumerable<Point> GetAdjacentLocations(Point location)
        {
            return new Point[]
            {
                // new Point(location.X + 1, location.Y - 1),
                new Point(location.X + 1, location.Y),
                //new Point(location.X + 1, location.Y + 1),
                // new Point(location.X - 1, location.Y - 1),
                new Point(location.X - 1, location.Y),
                //new Point(location.X - 1, location.Y + 1),
                new Point(location.X, location.Y + 1),
                new Point(location.X, location.Y - 1)
            };
        }
    }
}