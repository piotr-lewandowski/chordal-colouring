using Logic;

namespace Interface;

class JavaGraphReader
{
    public IEnumerable<Graph<Vertex>> ReadFile(string path)
    {
        var lines = File.ReadAllLines(path);

        var current = 0;

        var list = new List<Graph<Vertex>>();
        while (current < lines.Length)
        {
            var g = ParseGraph();
            list.Add(g);
        }

        return list;

        Graph<Vertex> ParseGraph()
        {
            // Label
            current++;

            // Vertex count
            var countText = lines[current].Split('=')[1];
            var count = int.Parse(countText);
            current++;

            var edges = new List<List<int>>();

            // Create the clique
            for (int i = 0; i < count; i++)
            {
                edges.Add(new List<int>());
                for (int j = 0; j < count; j++)
                {
                    if(i != j)
                        edges[i].Add(j);
                }
            }

            // Random empty vertices
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
                while(from >= edges.Count)
                {
                    edges.Add(new());
                }
                edges[from].AddRange(to);
                current++;
            }

            // Recommended edges we don't use
            if(current < lines.Length && lines[current].Contains("<->"))
                current++;

            return Graph.FromEdges(edges);
        }
    }

}