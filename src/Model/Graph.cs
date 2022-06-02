/* Graph.cs */

using System;
using System.Collections.Generic;

namespace Go.src.Model
{



    #region Exceptions
    public class DuplicateVertexException : InvalidOperationException
    {
        public DuplicateVertexException(string message) : base(message){}
    }
    public class DuplicateEdgeException : InvalidOperationException
    {
        public DuplicateEdgeException(string message) : base(message){}
    }
    public class VertexNotFoundException : InvalidOperationException
    {
        public VertexNotFoundException(string message) : base(message){}
    }
    public class EdgeNotFoundException : InvalidOperationException
    {
        public EdgeNotFoundException(string message) : base(message){}
    }
    #endregion

    /// <summary>
    /// Edge is the Connection between a Pair of Nodes.
    /// An Edge Has a Direction and Therefore a Start Vertex and an Ending Vertex,
    /// These are 'from' and 'to' respectively.
    /// </summary>
    /// <typeparam name="T"> Generic Node Type. </typeparam>
    public struct Edge<T>
    {
        public T from;
        public T to;

        public Edge(T from, T to)
        {
            this.from = from;
            this.to = to;
        }

    }

    public class Graph<NodeType, ValType>
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<NodeType, HashSet<NodeType>> AdjList { get; }

        private Dictionary<NodeType, ValType> values { get; }

        /// <summary>
        /// 
        /// </summary>
        public Graph()
        {
            AdjList = new Dictionary<NodeType, HashSet<NodeType>>();
            values = new Dictionary<NodeType, ValType>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="edges"></param>
        /// <param name="initializer"></param>
        public Graph(IEnumerable<NodeType> vertices, IEnumerable<Edge<NodeType>> edges, ValType initializer):this()
        {
            foreach (NodeType vertex in vertices)
            {
                if (!IsVertex(vertex))
                    AddVertex(vertex, initializer);
            }
            foreach (Edge<NodeType> edge in edges)
            {
                if (IsVertex(edge.from) && IsVertex(edge.to))
                    AddEdge(edge);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCopy"></param>
        public Graph(Graph<NodeType, ValType> toCopy):this()
        {
            foreach(NodeType vertex in toCopy.AdjList.Keys)
                AddVertex(vertex, toCopy.values[vertex]);
            foreach(NodeType vertex in toCopy.AdjList.Keys)
            {
                foreach (NodeType adjacent in toCopy.AdjList[vertex])
                    AddEdge(new Edge<NodeType>(vertex, adjacent));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool IsEdge(Edge<NodeType> edge)
        {
            if (!IsVertex(edge.from))
                return false;
            if (!IsVertex(edge.to))
                return false;

            return AdjList[edge.from].Contains(edge.to);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        /// <exception cref="VertexNotFoundException"></exception>
        /// <exception cref="DuplicateEdgeException"></exception>
        public void AddEdge(Edge<NodeType> edge)
        {
            if (!IsVertex(edge.from))
                throw new VertexNotFoundException("Vertex Not Found");
            if (!IsVertex(edge.to))
                throw new VertexNotFoundException("Vertex Not Found");
            if (IsEdge(edge))
                throw new DuplicateEdgeException("Edge Already Exists.");

            AdjList[edge.from].Add(edge.to);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="value"></param>
        /// <exception cref="DuplicateVertexException"></exception>
        public void AddVertex(NodeType vertex, ValType value)
        {
            if (IsVertex(vertex))
                throw new DuplicateVertexException("Invalid Operation. Vertex already Exists.");
            AdjList[vertex] = new HashSet<NodeType>();
            values[vertex] = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool IsVertex(NodeType vertex)
        {
            return AdjList.ContainsKey(vertex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        /// <exception cref="VertexNotFoundException"></exception>
        public ValType Get(NodeType vertex)
        {
            if (!IsVertex(vertex))
                throw new VertexNotFoundException(
                    String.Format("Couln't Retrieve Value Because Vertex ({0:d}) Doesn't Exist.", vertex.ToString()));
            return values[vertex];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="value"></param>
        /// <exception cref="VertexNotFoundException"></exception>
        public void Set(NodeType vertex, ValType value)
        {
            if(!IsVertex(vertex))
                throw new VertexNotFoundException(
                    String.Format("Couln't Set Value Because Vertex ({0:d}) Doesn't Exist.", vertex.ToString()));
            values[vertex] = value;
        }

        public NodeType[] GetAdjacent(NodeType node)
        {
            if (!IsVertex(node))
                throw new VertexNotFoundException("Vertex ("+node.ToString()+"), Does Not Exist");

            NodeType[] Adjacent = new NodeType[AdjList[node].Count];
            AdjList[node].CopyTo(Adjacent);
            return Adjacent;
        }

        public NodeType[] GetVertices()
        {
            NodeType[] vertices = new NodeType[AdjList.Count];
            AdjList.Keys.CopyTo(vertices, 0);
            return vertices;
        }

        public Edge<NodeType>[] GetEdges()
        {
            List<Edge<NodeType>> edges = new List<Edge<NodeType>>();
            foreach(NodeType vertex in AdjList.Keys)
            {
                foreach (NodeType adjacent in AdjList[vertex])
                    edges.Add(new Edge<NodeType>(vertex, adjacent));
            }

            return edges.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString()
        {
            string serialized = "";
            foreach (NodeType n in AdjList.Keys)
            {
                serialized += n.ToString() + " -> ";
                foreach (NodeType Adjnt in AdjList[n])
                    serialized += Adjnt.ToString() + " -> ";

                serialized += "||-\n";
            }
            return serialized;
        }

    }
}


