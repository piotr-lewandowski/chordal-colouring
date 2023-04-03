using Logic;
using Interface;

var reader = new GraphReader();
var graph = reader.ReadSingle(Path.Combine("Data", "docs_example.txt"));

var writer = new ColourWriter();
var text = writer.Write(graph);
Console.Write(text);

var algorithm = new MaximumCardinalitySearch();
var coloured = algorithm.Colour(graph);

var order = algorithm.OrderVertices(graph);
var isPerfect = new PerfectEliminationOrdering().IsPerfectElimination(order.ToList(), graph);

Console.WriteLine(isPerfect);

text = writer.Write(coloured);
Console.Write(text);
