namespace Interface;

using Logic;

class GraphReader : IGraphReader
{
    public Graph<Vertex> ReadSingle(string path)
    {
        var lines = File.ReadAllLines(path);
        var count = int.Parse(lines[0]);

        var edges = new List<List<int>>();

        for(int i=0; i<count; ++i)
        {
            var neighbours = lines[i+1]
                .Split(' ')
                .Select(s => int.Parse(s) - 1); // Convert to 0-based ordering by
            edges.Add(new());
            edges[i].AddRange(neighbours);
        }

        return Graph.FromNeighbourLists(edges);
    }
}