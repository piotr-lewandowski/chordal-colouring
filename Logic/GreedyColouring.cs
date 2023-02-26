namespace Logic;

public abstract class GreedyColouring : IColouring
{

    protected abstract IEnumerable<Vertex> OrderVertices(Graph<Vertex> graph);

    public Graph<ColouredVertex> Colour(Graph<Vertex> graph)
    {
        var orderedVertices = OrderVertices(graph);
        var colours = new Colour?[orderedVertices.Count()];
        foreach (var vertex in orderedVertices)
        {
            var usedColours = vertex.Neighbours
                .Select(n => colours[n.Id])
                .OfType<Colour>()
                .OrderBy(c => c.Id);

            var smallestPossibleId = 0;
            foreach(var c in usedColours)
            {
                if(smallestPossibleId == c.Id)
                {
                    smallestPossibleId++;
                }
                else
                {
                    break;
                }
            }

            var colour = new Colour(smallestPossibleId);
            colours[vertex.Id] = colour;
        }

        var newVertices = graph.Vertices.Select(v => v.WithColour(colours[v.Id]!));

        return new(newVertices);
    }
}