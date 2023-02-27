using Logic;

namespace Interface;

class JavaGraphReader
{
    public IEnumerable<Graph<Vertex>> ReadFile(string path)
    {
        var lines = File.ReadAllLines(path);

        var current = 0;

        while (current < lines.Length)
        {
            ParseGraph();
        }

        Graph<Vertex> ParseGraph()
        {
            // Label
            current++;
            // Vertex count
            var countText = lines[current].Split('=')[1];
            var count = int.Parse(countText);
            current++;

            var edges = new List<List<int>>();
            while (lines[current].Contains("-->"))
            {

            }
        }
    }

}