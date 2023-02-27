using Logic;
using System.Text;

namespace Interface;

class GraphWriter : IGraphWriter
{
    public string Write(Graph<Vertex> graph)
    {
        var sb = new StringBuilder();
        foreach(var vertex in graph.Vertices)
        {
            sb.Append($"{vertex.Id} - ");
            sb.AppendJoin(" ", vertex.Neighbours.Select(v => v.Id));
            sb.AppendLine();
        }

        return sb.ToString();
    }
}