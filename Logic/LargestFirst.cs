namespace Logic;

public class LargestFirst : GreedyColouring
{
    protected override IEnumerable<Vertex> OrderVertices(Graph graph) =>
        graph.Vertices.OrderByDescending(v => v.Neighbours.Count());
}