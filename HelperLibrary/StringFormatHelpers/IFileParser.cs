namespace HelperLibrary.StringFormatHelpers
{
    public interface IFileParser
    {
        IEnumerable<string> ParseFile(string path);
    }
}