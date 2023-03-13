namespace Logic;

public record Colour(int Id)
{
    public override string ToString()
    {
        return (Id + 1).ToString();
    }
}


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

    public Vertex(int id, IEnumerable<Vertex> neighbours, Colour colour)
    {
        Id = id;
        Neighbours = neighbours;
        Colour = colour;
    }

    public int Id { get; }
    public IEnumerable<Vertex> Neighbours { get; set; }
    public Colour? Colour { get; set; }
}

public record Edge(Vertex Start, Vertex End)
{
    public IEnumerable<Vertex> Vertices { get; } = new[] { Start, End };
}


public record Graph(IEnumerable<Vertex> Vertices)
{
    public IEnumerable<Edge> Edges { get; } =
        Vertices.SelectMany(v => v.Neighbours.Where(n => n.Id > v.Id).Select(n => new Edge(v, n))).ToList();

    public bool IsColoured() => Vertices.All(v => v.Colour is not null);

    public static Graph FromNeighbourLists(IEnumerable<IEnumerable<int>> edges)
    {
        var vertexCount = edges.Count();
        var vertices = Enumerable.Range(0, vertexCount).Select(i => new Vertex(i)).ToArray();

        foreach(var vertex in vertices)
        {
            var neighbours = edges.ElementAt(vertex.Id).Select(i => vertices[i]);
            vertex.Neighbours = neighbours;
        }

        return new Graph(vertices);
    }
}