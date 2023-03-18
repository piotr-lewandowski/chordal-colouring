using Logic;
using Interface;

var reader = new GraphReader();
var graph = reader.ReadSingle(Path.Combine("Data", "docs_example.txt"));

var writer = new ColourWriter();
var text = writer.Write(graph);
Console.Write(text);

var algorithm = new MaximumCardinalitySearch();
var coloured = algorithm.Colour(graph);

text = writer.Write(coloured);
Console.Write(text);
