using System.Text;
using Logic;

namespace Interface;

class JavaGraphReader : IGraphReader
{

    public Graph ReadSingle(string path)
    {
        var lines = File.ReadAllLines(path);
        var current = 0;

        return ParseGraph(lines, ref current);
    }

    public IEnumerable<Graph> ReadList(string path)
    {
        var lines = File.ReadAllLines(path);
        var current = 0;

        var list = new List<Graph>();
        while (current < lines.Length)
        {
            var g = ParseGraph(lines, ref current);
            list.Add(g);
        }

        return list;
    }

    Graph ParseGraph(string[] lines, ref int current)
    {
        // Label
        current++;

        // Vertex count
        var countText = lines[current].Split('=')[1];
        var count = int.Parse(countText);
        current++;

        var edges = new List<List<int>>();

        // Isolated vertices, we ignore the starting clique, but want 
        for (int i = count; i < 32; i++)
        {
            edges.Add(new());
        }

        // Read edges
        while (current < lines.Length && lines[current].Contains("-->"))
        {
            var splitLine = lines[current].Split(' ');
            var from = int.Parse(splitLine[0]);
            var to = splitLine[2..^1].Select(s => int.Parse(s)).ToList();
            while (from >= edges.Count)
            {
                edges.Add(new());
            }
            edges[from].AddRange(to);
            current++;
        }

        // Recommended edges we don't use
        if (current < lines.Length && lines[current].Contains("<->"))
            current++;

        // Find a possible reordering of vertices
        edges = edges.Select(neighbours => neighbours.Where(n => n > 31).ToList()).ToList();
        var nonIsolated = edges.Select(neighbours => neighbours.Count > 0).ToList();
        var currentIndex = 0;
        var mapping = new Dictionary<int, int>();
        for (int i = 0; i < nonIsolated.Count(); ++i)
        {
            if (nonIsolated[i])
            {
                mapping.Add(i, currentIndex);
                currentIndex++;
            }
        }

        // Drop isolated vertices and reorder
        edges = edges
            .Where(neigbours => neigbours.Count > 0)
            .Select(neighbours => neighbours.Select(n => mapping[n]).Order().ToList())
            .ToList();

        return Graph.FromNeighbourLists(edges);
    }

}