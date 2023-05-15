namespace Logic;

public interface IColouring
{
    Graph Colour(Graph graph);

    string Name => this.GetType().Name;
}