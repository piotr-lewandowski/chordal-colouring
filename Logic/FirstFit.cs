namespace Logic;

class FirstFit : GreedyColouring
{
    public override IEnumerable<Vertex> OrderVertices(Graph graph) => graph.Vertices;
}