using System;
using EricLipertImmutableGraphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphTheory.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var map = LoadData();

            var edges = map.Edges("Troll Room");

            Assert.IsTrue(edges.Count > 0);
        }

        static ImmutableDirectedGraph<string, string> LoadData()
        {
            var map = ImmutableDirectedGraph<string, string>.Empty
                .AddEdge("Troll Room", "East West Passage", "East")
                .AddEdge("East West Passage", "Round Room", "East")
                .AddEdge("East West Passage", "Chasm", "North")
                .AddEdge("Round Room", "North South Passage", "North")
                .AddEdge("Round Room", "Loud Room", "East")
                .AddEdge("Chasm", "Reservoir South", "Northeast")
                .AddEdge("North South Passage", "Chasm", "North")
                .AddEdge("North South Passage", "Deep Canyon", "Northeast")
                .AddEdge("Loud Room", "Deep Canyon", "Up")
                .AddEdge("Reservoir South", "Dam", "East")
                .AddEdge("Deep Canyon", "Reservoir South", "Northwest")
                .AddEdge("Dam", "Dam Lobby", "North")
                .AddEdge("Dam Lobby", "Maintenance Room", "East")
                .AddEdge("Dam Lobby", "Maintenance Room", "North");

            return map;
        }
    }
}
