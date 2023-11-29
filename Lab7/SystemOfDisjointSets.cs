namespace Lab7
{
    public class SystemOfDisjointSets<TVertex>
        where TVertex : notnull, IComparable<TVertex>, IEquatable<TVertex>
    {
        public List<MySet<TVertex>> Sets { get; set; } = [];

        public void Add(MyEdge<TVertex> edge)
        {
            MySet<TVertex>? setA = Find(edge.VertexA);
            MySet<TVertex>? setB = Find(edge.VertexB);

            if (setA != null && setB == null)
            {
                setA.Add(edge);
            }
            else if (setA == null && setB != null)
            {
                setB.Add(edge);
            }
            else if (setA == null && setB == null)
            {
                MySet<TVertex> set = new MySet<TVertex>(edge);
                Sets.Add(set);
            }
            else if (setA != setB)
            {
                setA!.Union(setB!, edge);
                Sets.Remove(setB!);
            }
        }

        public MySet<TVertex>? Find(TVertex vertex)
        {
            foreach (var set in Sets)
            {
                if (set.Contains(vertex)) 
                    return set;
            }
            return null;
        }
    }
}
