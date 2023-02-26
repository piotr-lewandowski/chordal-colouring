namespace Logic;

public class LargestFirst : GreedyColouring
{
    protected override IEnumerable<Vertex> OrderVertices(Graph<Vertex> graph) =>
        graph.Vertices.OrderByDescending(v => v.Neighbours.Count());
}