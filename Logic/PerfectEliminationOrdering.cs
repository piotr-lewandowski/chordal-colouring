namespace Logic;

public class PerfectEliminationOrdering
{
    public bool IsPerfectElimination(List<Vertex> ordered, Graph graph)
    {
        var edges = graph.Edges;
        for(int i=0; i<ordered.Count; ++i)
        {
            var current = ordered[i];
            var previous = ordered.Take(i).Where(u => u.Neighbours.Contains(current));

            var all = previous.Append(current);
            var relevantEdges = edges.Count(e => all.Contains(e.Start) && all.Contains(e.End));

            var n = all.Count();
            if (relevantEdges < (n * (n-1)) / 2)
                return false;
        }

        return true;
    }
}