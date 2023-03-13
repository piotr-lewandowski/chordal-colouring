using Logic;
using System.Text;

namespace Interface;

class NeighbourWriter : IGraphWriter
{
    public string Write(Graph graph)
    {
        var sb = new StringBuilder();
        sb.AppendLine(graph.Vertices.Count().ToString());
        foreach(var vertex in graph.Vertices)
        {
            sb.AppendJoin(" ", vertex.Neighbours.Select(v => v.Id + 1));
            sb.AppendLine();
        }

        return sb.ToString();
    }
}