using Logic;
using Interface;

var reader = new JavaGraphReader();
var graph = reader.ReadSingle(Path.Combine("Data", "java_interference_graphs.txt"));

var writer = new GraphWriter();
var text = writer.Write(graph);

Console.Write(text);
