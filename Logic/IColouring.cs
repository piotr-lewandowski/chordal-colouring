namespace Logic;

public interface IColouring
{
    Graph<ColouredVertex> Colour(Graph<Vertex> graph);
}