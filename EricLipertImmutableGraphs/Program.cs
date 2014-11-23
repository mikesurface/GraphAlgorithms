using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace EricLipertImmutableGraphs
{
    class Program
    {
        static void Main(string[] args)
        {
            //var map = LoadData();

            //foreach (var path in AllEdgeTraversals("Troll Room", n => map.Edges(n)))
            //{
            //    Console.WriteLine(string.Join(" ", from pair in path select pair.Key));
            //}

            Func<Tuple<int, int>, IReadOnlyDictionary<string, Tuple<int, int>>>
                getEdges = latticePoint =>
                {
                    var edges = ImmutableDictionary<string, Tuple<int, int>>.Empty;
                    if (latticePoint.Item1 > 0)
                    {
                        edges = edges.Add(
                            "Left", Tuple.Create(latticePoint.Item1 - 1, latticePoint.Item2));
                    }
                    if (latticePoint.Item2 > 0)
                    {
                        edges = edges.Add(
                            "Down", Tuple.Create(latticePoint.Item1, latticePoint.Item2 - 1));
                    }
                    return edges;
                };

            foreach (var path in AllEdgeTraversals(Tuple.Create(3, 2), getEdges))
                Console.WriteLine(string.Join(" ", from pair in path select pair.Key));

            Console.ReadLine();
        }

        public static IEnumerable<ImmutableStack<KeyValuePair<E, N>>>
            AllEdgeTraversals<E, N>(
            N start,
            Func<N, IReadOnlyDictionary<E, N>> getEdges)
        {
            var edges = getEdges(start);
            if (edges.Count == 0)
            {
                yield return ImmutableStack<KeyValuePair<E, N>>.Empty;
            }
            else
            {
                foreach (var pair in edges)
                    foreach (var path in AllEdgeTraversals(pair.Value, getEdges))
                        yield return path.Push(pair);
            }
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
