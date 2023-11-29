using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    public class MySet<TVertex>(MyEdge<TVertex> edge)
        where TVertex : notnull, IComparable<TVertex>, IEquatable<TVertex>
    {
        public MyGraph<TVertex> SetGraph { get; set; } = new MyGraph<TVertex>(edge);
        public List<TVertex> Vertices { get; set; } = [edge.VertexA, edge.VertexB];

        public void Union(MySet<TVertex> other, MyEdge<TVertex> connectingEdge)
        {
            SetGraph.Merge(other.SetGraph);
            Vertices.AddRange(other.Vertices);
            SetGraph.Add(connectingEdge);
        }

        public void Add(MyEdge<TVertex> edge)
        {
            SetGraph.Add(edge);
            Vertices.Add(edge.VertexA);
            Vertices.Add(edge.VertexB);
        }

        public bool Contains(TVertex vertex)
        {
            return Vertices.Contains(vertex);
        }
    }
}
