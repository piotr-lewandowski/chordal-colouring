using Interface;
using Logic;
using System.CommandLine.Builder;
using System.CommandLine.Help;
using System.CommandLine.Parsing;
using System.CommandLine;

var fileArgument = new Argument<FileInfo>(
    name: "filepath",
    description: "File containing the input graph."
);

fileArgument.AddValidator(result =>
{
    var fileInfo = result.GetValueForArgument<FileInfo>(fileArgument);
    if (!fileInfo.Exists)
    {
        result.ErrorMessage = $"File {fileInfo.FullName} does not exist.";
    }
});

var algOption = new Option<Algorithms>(
    new[] { "--algorithm", "-a" },
    description: "Which algorithm to run.",
    getDefaultValue: () => Algorithms.MCS
);

var rootCommand = new RootCommand(
    "Graph colouring of arbitrary graphs using First Fit, Largest First, Smallest Last or Maximum Cardinality Search."
) { fileArgument, algOption };

rootCommand.SetHandler((file, algorithm) =>
{
    var reader = new GraphReader();
    var writer = new ColourWriter();
    var error = Console.Error;

    try
    {
        var graph = reader.ReadSingle(file.FullName);
        var colouring = GetColouring(algorithm);

        var coloured = colouring.Colour(graph);

        var text = writer.Write(coloured);
        Console.Write(text);
    }
    catch (System.Exception)
    {
        error.WriteLine("Input graph was not in a correct format.");
    }

}, fileArgument, algOption);

await rootCommand.InvokeAsync(args);

IColouring GetColouring(Algorithms algorithm) => algorithm switch
{
    Algorithms.LF => new LargestFirst(),
    Algorithms.SL => new SmallestLast(),
    Algorithms.MCS => new MaximumCardinalitySearch(),
    Algorithms.FF => new FirstFit(),
    _ => throw new ArgumentException("Unrecognized algorithm type.")
};

public enum Algorithms
{
    LF, SL, FF, MCS
}