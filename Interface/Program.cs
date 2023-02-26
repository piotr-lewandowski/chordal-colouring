// See https://aka.ms/new-console-template for more information

using Logic;

var vertices = new List<Vertex>();

for (var i = 0; i < 6; i++)
{
    vertices.Add(new Vertex(i));
}
vertices[0].Neighbours = new Vertex[] { vertices[5], vertices[1]};
for (var i = 1; i < 6; i++)
{
    var neighbours = new Vertex[] { vertices[(i-1)%6], vertices[(i+1)%6]}; 
    vertices[i].Neighbours = neighbours;
}

var graph = new Graph<Vertex>(vertices);

var coloured = new LargestFirst().Colour(graph);

foreach (var vertex in coloured.Vertices)
{
    Console.WriteLine($"{vertex.Id} - {vertex.Colour.Id}");
}