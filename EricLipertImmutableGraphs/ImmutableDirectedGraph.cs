using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace EricLipertImmutableGraphs
{
    public struct ImmutableDirectedGraph<N, E>
    {
        public static readonly ImmutableDirectedGraph<N, E> Empty =
            new ImmutableDirectedGraph<N, E>(ImmutableDictionary<N, ImmutableDictionary<E, N>>.Empty);

        private readonly ImmutableDictionary<N, ImmutableDictionary<E, N>> graph;

        private ImmutableDirectedGraph(ImmutableDictionary<N, ImmutableDictionary<E, N>> graph)
        {
            this.graph = graph;
        }

        public ImmutableDirectedGraph<N, E> AddNode(N node)
        {
            if (graph.ContainsKey(node))
                return this;

            return new ImmutableDirectedGraph<N, E>(
                graph.Add(node, ImmutableDictionary<E, N>.Empty));
        }

        public ImmutableDirectedGraph<N, E> AddEdge(N start, N finish, E edge)
        {
            var g = AddNode(start).AddNode(finish);
            return new ImmutableDirectedGraph<N, E>(
                g.graph.SetItem(
                    start,
                    g.graph[start].SetItem(edge, finish)));
        }

        public IReadOnlyDictionary<E, N> Edges(N node)
        {
            return graph.ContainsKey(node)
                ? graph[node]
                : ImmutableDictionary<E, N>.Empty;
        }
    }
}