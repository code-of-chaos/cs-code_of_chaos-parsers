// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Contracts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICsvWriter<T> {
    public string WriteToString(IEnumerable<T> data);
    public Task<string> WriteToStringAsync(IEnumerable<T> data);
    public void WriteToFile(string filePath, IEnumerable<T> data);
    public Task WriteToFileAsync(string filePath, IEnumerable<T> data);
}
