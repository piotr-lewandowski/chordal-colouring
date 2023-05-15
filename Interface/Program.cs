using Interface;
using Logic;
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

var directoryArg = new Argument<DirectoryInfo>(
    name: "directory",
    description: "Directory containing input files"
);

directoryArg.AddValidator(result =>
{
    var dirInfo = result.GetValueForArgument<DirectoryInfo>(directoryArg);
    if (!dirInfo.Exists)
    {
        result.ErrorMessage = $"Directory {dirInfo.FullName} does not exist.";
    }
});

var generateCommand = new Command("generate",
    "Generate csv data for the data set in a given directory."
) { directoryArg };

generateCommand.SetHandler(dir =>
{
    var reader = new MultiGraphReader();

    var algs = Enum.GetValues<Algorithms>();

    foreach (var alg in algs)
    {
        reader.GenerateDataForAllFiles(GetColouring(alg), dir);
    }
}, directoryArg);

rootCommand.Add(generateCommand);

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