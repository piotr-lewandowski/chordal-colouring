using Logic;

namespace Interface;

interface IGraphReader
{
    Graph<Vertex> ReadFile(string path);
}
