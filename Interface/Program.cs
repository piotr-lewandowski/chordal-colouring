using Logic;
using Interface;

var reader = new GraphReader();
var graph = reader.ReadSingle(Path.Combine("Data", "docs_example.txt"));

var writer = new GraphWriter();
var text = writer.Write(graph);

Console.Write(text);
