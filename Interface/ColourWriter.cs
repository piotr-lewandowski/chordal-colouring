namespace Interface;

using System.Text;
using Logic;

class ColourWriter : IGraphWriter
{
    public string Write(Graph graph)
    {
        var sb = new StringBuilder();

        sb.AppendLine(graph.MaxColour.ToString());
        foreach(var v in graph.Vertices)
        {
            sb.AppendLine($"{v.Id + 1} {v.Colour?.ToString() ?? "?"}");
        }

        return sb.ToString();
    }
}