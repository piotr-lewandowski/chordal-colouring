namespace Logic;

public class SmallestLast : GreedyColouring
{
    public override IEnumerable<Vertex> OrderVertices(Graph graph)
    {
        var degrees = graph.Vertices.Select(v => v.Neighbours.Count()).ToList();
        var vertices = graph.Vertices.ToList();

        var stack = new Stack<Vertex>();

        var alreadyUsedDegree = vertices.Count;

        for(int i=0; i<vertices.Count; ++i)
        {
            var smallestDegree = alreadyUsedDegree;
            var index = 0;
            for(int j = 0; j<degrees.Count; ++j)
            {
                if(degrees[j] <= smallestDegree)
                {
                    smallestDegree = degrees[j];
                    index = j;
                }
            }

            var vertex = vertices[index];
            stack.Push(vertices[index]);
            degrees[index] = alreadyUsedDegree;

            foreach(var v in vertex.Neighbours)
            {
                degrees[v.Id]--;
            }
        }

        return stack.ToList();
    }
}