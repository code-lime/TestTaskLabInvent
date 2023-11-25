using FileParser.Common.Status;
using FileParser.Interfaces;
using Serilog;
using System.Xml.Serialization;

namespace FileParser.Services;

public class FileParserService : IFileParser
{
    private readonly ILogger _logger;
    public FileParserService(ILogger logger)
        => _logger = logger;

    public IEnumerable<InstrumentStatus> ParseFiles(string directory)
    {
        _logger.Debug("Begin parse files in directory '{directory}'", directory);
        int counter = 0;

        string[] files;
        try
        {
            files = Directory.GetFiles(directory, "*.xml");
        }
        catch (Exception e)
        {
            _logger.Error(e, "Error interaction directory '{directory}'", directory);
            yield break;
        }

        foreach (string fileName in files)
        {
            InstrumentStatus instrument;
            try
            {
                using FileStream file = File.OpenRead(fileName);
                using StreamReader reader = new StreamReader(file);
                XmlSerializer converter = new XmlSerializer(typeof(InstrumentStatus));
                instrument = (InstrumentStatus)converter.Deserialize(reader)!;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error read file {file}", fileName);
                continue;
            }
            yield return instrument;
            counter++;
        }
        _logger.Debug("End parse files in directory '{directory}'. Total parsed files: {files}", directory, counter);
    }
}
