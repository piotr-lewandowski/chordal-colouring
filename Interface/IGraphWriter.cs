namespace Interface;

using Logic;

interface IGraphWriter 
{
    string Write(Graph<Vertex> graph);
}