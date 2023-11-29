using System.Collections;
using System.Text;

namespace Lab7
{
    public class MyGraph<TVertex> : IEnumerable<MyEdge<TVertex>>, ICloneable
        where TVertex : notnull, IComparable<TVertex>, IEquatable<TVertex>
    {
        private List<MyEdge<TVertex>> graph;

        public int Size 
        {
            get => graph.Count;
        }

        public MyGraph()
        {
            graph = new List<MyEdge<TVertex>>();
        }

        public MyGraph(MyEdge<TVertex> val)
        {
            graph = new List<MyEdge<TVertex>>([val]);
        }

        public MyGraph(List<MyEdge<TVertex>> edges)
        {
            graph = new List<MyEdge<TVertex>>(edges);
        }

        public void Merge(MyGraph<TVertex> other)
        {
            foreach (var e in other)
            {
                if (!graph.Contains(e))
                    graph.Add(e);
            }
        }

        public void Add(MyEdge<TVertex> edge)
        {
            if (edge.VertexA.Equals(edge.VertexB))
                return;

            if (Contains(edge))
                graph.Remove(edge);
            graph.Add(edge);
        }

        public void Remove(MyEdge<TVertex> e)
        {
            graph.Remove(e);
        }

        public void Remove(TVertex v1, TVertex v2)
        {
            Remove(new MyEdge<TVertex>(v1, v2, -1));
        }

        public int GetWeight()
        {
            int weight = 0;
            foreach (var e in graph)
            {
                weight += e.Weight;
            }
            return weight;
        }

        public bool Contains(TVertex v1, TVertex v2) => 
            Contains(new(v1, v2, -1));

        public bool Contains(MyEdge<TVertex> edge) => 
            Find(edge) != null;

        public MyEdge<TVertex>? Find(TVertex v1, TVertex v2) =>
            Find(new(v1, v2, -1));

        public MyEdge<TVertex>? Find(MyEdge<TVertex> other) => 
            graph.FirstOrDefault(edge => edge.Equals(other));


        public List<MyEdge<TVertex>> GetIncidentalEdges(MyEdge<TVertex> edge)
        {
            List<MyEdge<TVertex>> res = [];

            foreach (var e in graph)
            {
                if (!edge.Equals(e) && edge.IsIncidental(e))
                {
                    res.Add(e);
                }
            }

            return res;
        }

        public List<MyEdge<TVertex>> GetIncidentalEdges(TVertex vertex)
        {
            List<MyEdge<TVertex>> res = [];

            foreach (var e in graph)
            {
                if (e.VertexA.Equals(vertex) || e.VertexB.Equals(vertex))
                {
                    res.Add(e);
                }
            }

            return res;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var e in graph)
            {
                sb.Append(e.ToString() + '\n');
            }

            return sb.ToString();
        }

        public void PrintAsMatrix()
        {
            SortedSet<TVertex> vertexes = new(graph.Select(edge => edge.VertexA).Union(graph.Select(edge => edge.VertexB)));

            Console.Write(" ");
            foreach (var vertex in vertexes)
            {
                Console.Write("  " + vertex);
            }
            Console.WriteLine();

            foreach (var vertexA in vertexes)
            {
                Console.Write(vertexA + "  ");
                foreach (var vertexB in vertexes)
                {
                    Console.Write(Find(vertexA, vertexB)?.Weight.ToString() ?? "0");
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
        }

        public void Sort()
        {
            graph.Sort();
        }

        public IEnumerator<MyEdge<TVertex>> GetEnumerator()
        {
            return graph.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return graph.GetEnumerator();
        }

        public MyEdge<TVertex> this[int index]
        {
            get => graph[index];
        }

        public List<TVertex> FindMinVertexCover()
        {
            Random rnd = new Random();
            var result = new List<TVertex> ();
            MyGraph<TVertex> dummy = (MyGraph<TVertex>)this.Clone();

            while (dummy.Size > 0)
            {
                var curr = dummy[rnd.Next(dummy.Size)];
                result.Add(curr.VertexA);
                result.Add(curr.VertexB);
                foreach (var edge in dummy.GetIncidentalEdges(curr))
                {
                    dummy.Remove(edge);
                }
                dummy.Remove(curr);
            }
            
            return result;
        }

        public List<MyEdge<TVertex>> FindMinEdgesCover()
        {
            HashSet<TVertex> not_visited = new(graph.Select(edge => edge.VertexA).Union(graph.Select(edge => edge.VertexB)));
            List<MyEdge<TVertex>> res = new();

            foreach (var edge in graph)
            {
                if (not_visited.Contains(edge.VertexA) && not_visited.Contains(edge.VertexB))
                {
                    res.Add(edge);
                    not_visited.Remove(edge.VertexA);
                    not_visited.Remove(edge.VertexB);
                }
            }

            foreach (var vertex in not_visited)
            {
                res.Add(graph.First(edge => edge.VertexA.Equals(vertex) || edge.VertexB.Equals(vertex)));
                not_visited.Remove(vertex);
            }

            return res;
        }

        public MyGraph<TVertex> FindMinSpanningTree()
        {
            Sort();
            var disjointSets = new SystemOfDisjointSets<TVertex>();
            
            foreach (var edge in graph){
                disjointSets.Add(edge);
            }

            return disjointSets.Sets.First().SetGraph;
        }

        public object Clone()
        {
            return new MyGraph<TVertex>(graph);
        }
    }
}
