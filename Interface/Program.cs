using Logic;
using Interface;

var reader = new JavaGraphReader();
var graphs = reader.ReadFile(Path.Combine("Data", "java_interference_graphs.txt"));

var writer = new GraphWriter();
var text = writer.Write(graphs.First());

Console.Write(text);
