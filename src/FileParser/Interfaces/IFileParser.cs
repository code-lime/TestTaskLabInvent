using FileParser.Common.Status;

namespace FileParser.Interfaces;

public interface IFileParser
{
    IEnumerable<InstrumentStatus> ParseFiles(string directory);
}
