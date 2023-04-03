namespace Logic;

public class LargestFirst : GreedyColouring
{
    public override IEnumerable<Vertex> OrderVertices(Graph graph) =>
        graph.Vertices.OrderByDescending(v => v.Neighbours.Count());
}