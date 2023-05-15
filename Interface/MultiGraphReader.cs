namespace Interface;

using System.Text;
using Logic;

public class MultiGraphReader
{
    public List<Graph> ReadMany(string path)
    {
        var lines = File.ReadAllLines(path);

        var splitLines = new List<List<string>>();
        splitLines.Add(new());
        foreach (var line in lines)
        {
            if (line == "")
            {
                splitLines.Add(new());
            }
            else
            {
                splitLines.Last().Add(line);
            }
        }

        var reader = new GraphReader();
        return splitLines.Where(s => s.Count > 0).Select(s => reader.ReadSingle(s.ToArray())).ToList();
    }

    public virtual string GenerateDataForFile(string path, IColouring colouring)
    {
        var sb = new StringBuilder();

        var graphs = ReadMany(path);
        var info = new FileInfo(path);
        var knownStr = info.Name.Split('_')[1].Split('.')[0];

        var known = int.Parse(knownStr);

        foreach (var graph in graphs)
        {
            var coloured = colouring.Colour(graph);

            sb.AppendLine($"{known},{coloured.MaxColour},{coloured.Vertices.Count()}");
        }

        return sb.ToString();
    }

    public void GenerateDataForAllFiles(IColouring colouring, DirectoryInfo directory)
    {
        var sb = new StringBuilder();
        sb.AppendLine("known,achieved,vertices");

        var fileInfos = directory.GetFiles();
        foreach (var file in fileInfos)
        {
            var result = GenerateDataForFile(file.FullName, colouring);
            sb.Append(result);
        }

        Directory.CreateDirectory($"./Results/{directory.Name}");
        using var newFile = File.CreateText($"./Results/{directory.Name}/{colouring.Name}.csv");
        newFile.Write(sb.ToString());
    }
}