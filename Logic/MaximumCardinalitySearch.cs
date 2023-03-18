namespace Logic;

public class MaximumCardinalitySearch : GreedyColouring
{
    protected override IEnumerable<Vertex> OrderVertices(Graph graph)
    {
        var degrees = graph.Vertices.Select(v => 0).ToList();
        var vertices = graph.Vertices.ToList();

        var ordered = new List<Vertex>();

        var alreadyUsedWeight = -1;

        for (int i = 0; i < vertices.Count; ++i)
        {
            var biggestWeight = alreadyUsedWeight;
            var index = 0;
            for (int j = 0; j < degrees.Count; ++j)
            {
                if (degrees[j] >= biggestWeight)
                {
                    biggestWeight = degrees[j];
                    index = j;
                }
            }

            var vertex = vertices[index];
            ordered.Add(vertex);
            degrees[index] = alreadyUsedWeight;

            foreach (var v in vertex.Neighbours)
            {
                if (degrees[v.Id] != alreadyUsedWeight)
                    degrees[v.Id]++;
            }
        }


        return ordered;
    }
}