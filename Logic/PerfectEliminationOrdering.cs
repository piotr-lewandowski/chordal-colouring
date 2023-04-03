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

            var all = previous.Append(current).Select(v => v.Id);
            var allSet = new SortedSet<int>(all);
            var relevantEdges = edges.Count(e => allSet.Contains(e.Start.Id) && allSet.Contains(e.End.Id));

            var n = all.Count();
            if (relevantEdges < (n * (n-1)) / 2)
                return false;
        }

        return true;
    }
}