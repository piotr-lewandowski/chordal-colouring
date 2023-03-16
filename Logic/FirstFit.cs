namespace Logic;

class FirstFit : GreedyColouring
{
    protected override IEnumerable<Vertex> OrderVertices(Graph graph) => graph.Vertices;
}