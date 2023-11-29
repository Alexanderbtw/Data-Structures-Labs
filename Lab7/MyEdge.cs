namespace Lab7
{
    public class MyEdge<TVertex> : IComparable<MyEdge<TVertex>>, IEquatable<MyEdge<TVertex>>
        where TVertex : notnull, IComparable<TVertex>, IEquatable<TVertex>
    {
        public int Weight { get; set; }
        public TVertex VertexA { get; set; }
        public TVertex VertexB { get; set; }

        public MyEdge(TVertex a, TVertex b, int weight)
        {
            VertexA = a;
            VertexB = b;
            Weight = weight;
        }

        public int CompareTo(MyEdge<TVertex>? other)
        {
            if (other == null) return 1;
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return $"{VertexA} - {VertexB} : {Weight}";
        }

        public bool Equals(MyEdge<TVertex>? other)
        {
            if (other == null) return false;
            return this.VertexA.Equals(other.VertexA) && this.VertexB.Equals(other.VertexB) || this.VertexB.Equals(other.VertexA) && this.VertexA.Equals(other.VertexB);
        }

        public bool IsIncidental(MyEdge<TVertex>? other)
        {
            if (other == null) return false;
            return this.VertexA.Equals(other.VertexA) || this.VertexB.Equals(other.VertexB) || this.VertexB.Equals(other.VertexA) || this.VertexA.Equals(other.VertexB);
        }
    }
}
