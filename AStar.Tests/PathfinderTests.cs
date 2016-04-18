using System;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AStar.Tests
{
    [TestClass]
    public class PathfinderTests
    {
        private bool[,] map;
        private SeachParameters seachParameters;

        [TestInitialize]
        public void Initialize()
        {
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □
            //  □ S □ □ □ F □
            //  □ □ □ □ □ □ □
            //  □ □ □ □ □ □ □

            map = new bool[7, 5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 7; x++)
                {
                    map[x, y] = true;
                }
            }

            seachParameters = new SeachParameters(new Point(1, 2), new Point(5, 2), map);
        }

        private void AddWallWithGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ ■ □ □
            //  □ □ □ □ □ □ □

            // Path: 1,2 ; 2,1 ; 3,0 ; 4,0 ; 5,1 ; 5,2

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[4, 1] = false;
        }


        private void AddWallWithoutGap()
        {
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □
            //  □ S □ ■ □ F □
            //  □ □ □ ■ □ □ □
            //  □ □ □ ■ □ □ □

            // No path

            this.map[3, 4] = false;
            this.map[3, 3] = false;
            this.map[3, 2] = false;
            this.map[3, 1] = false;
            this.map[3, 0] = false;
        }

        [TestMethod]
        public void Test_WihtoutWall_CanFindPath()
        {
            var pathfinder = new Pathfinder(seachParameters);

            var path = pathfinder.FindPath();

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(4, path.Count);
        }

        [TestMethod]
        public void Test_WithOpenWall_CanFindPath()
        {
            AddWallWithGap();

            var pathfinder = new Pathfinder(seachParameters);

            var path = pathfinder.FindPath();

            Assert.IsNotNull(path);
            Assert.IsTrue(path.Any());
            Assert.AreEqual(8, path.Count);
        }

        [TestMethod]
        public void Test_WithClosedWall_NoPath()
        {
            AddWallWithoutGap();

            var pathfinder = new Pathfinder(seachParameters);

            var path = pathfinder.FindPath();


            Assert.IsNotNull(path);
            Assert.IsFalse(path.Any());
        }
    }
}
