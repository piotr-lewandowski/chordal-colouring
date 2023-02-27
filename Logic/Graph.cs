namespace Logic;

public record Colour(int Id);


public record Vertex
{
    public Vertex(int id)
    {
        Id = id;
        Neighbours = Enumerable.Empty<Vertex>();
    }

    public Vertex(int id, IEnumerable<Vertex> neighbours)
    {
        Id = id;
        Neighbours = neighbours;
    }

    public int Id { get; }
    public IEnumerable<Vertex> Neighbours { get; set; }
    public ColouredVertex WithColour(Colour colour) => new(Id, Neighbours, colour);
}

public record ColouredVertex(int Id, IEnumerable<Vertex> Neighbours, Colour Colour) : Vertex(Id, Neighbours);

public record Edge(Vertex Start, Vertex End)
{
    public IEnumerable<Vertex> Vertices { get; } = new[] { Start, End };
}

public record Graph<T>(IEnumerable<T> Vertices) where T : Vertex
{
    public IEnumerable<Edge> Edges { get; } =
        Vertices.SelectMany(v => v.Neighbours.Where(n => n.Id > v.Id).Select(n => new Edge(v, n))).ToList();
}

public class Graph 
{
    public static Graph<Vertex> FromEdges(IEnumerable<IEnumerable<int>> edges)
    {
        var vertexCount = edges.Count();
        var vertices = Enumerable.Range(0, vertexCount).Select(i => new Vertex(i)).ToArray();

        foreach(var vertex in vertices)
        {
            var neighbours = edges.ElementAt(vertex.Id).Select(i => vertices[i]);
            vertex.Neighbours = neighbours;
        }

        return new Graph<Vertex>(vertices);
    }
}