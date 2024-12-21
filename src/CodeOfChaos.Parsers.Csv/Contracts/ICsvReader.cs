// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Contracts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICsvReader<T> {
    public IEnumerable<T> FromCsvFile(string filePath);
    public IEnumerable<T> FromCsvString(string data);
    public IAsyncEnumerable<T> FromCsvFileAsync(string filePath, CancellationToken ct = default);
    public IAsyncEnumerable<T> FromCsvStringAsync(string data, CancellationToken ct = default);
}
