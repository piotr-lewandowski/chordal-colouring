using Logic;

namespace Interface;

interface IGraphReader
{
    Graph<Vertex> ReadSingle(string path);
}
