// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Provides an interface for parsing CSV data from various sources and outputting CSV data to different destinations.
/// </summary>
public interface ICsvParser {
    /// <summary>
    ///     Converts data from a text reader into an enumerable collection of a specified type.
    ///     The method reads from the provided text reader and maps the data to objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <typeparam name="TReader">The type of text reader used to read the input. Must derive from <see cref="TextReader" />.</typeparam>
    /// <param name="reader">The text reader from which to read the CSV data.</param>
    /// <returns>An enumerable collection of objects of type <typeparamref name="T" /> representing the parsed CSV records.</returns>
    IEnumerable<T> ToEnumerable<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Converts the CSV file specified by the file path into an enumerable collection of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to be created from the CSV. Must be a class with a parameterless constructor.</typeparam>
    /// <param name="filePath">The path of the CSV file to be parsed.</param>
    /// <return>An enumerable collection of type T containing the parsed CSV data.</return>
    IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously parses CSV data from a text reader into an enumerable collection of objects of type
    ///     <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to be created from the CSV data.</typeparam>
    /// <typeparam name="TReader">The type of text reader being used to read the CSV data.</typeparam>
    /// <param name="reader">The text reader containing the CSV data to process.</param>
    /// <param name="ct">A cancellation token to observe while awaiting the asynchronous operation.</param>
    /// <returns>An asynchronous enumerable of objects of type <typeparamref name="T" /> parsed from the CSV data.</returns>
    IAsyncEnumerable<T> ToEnumerableAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously parses a CSV file specified by the file path into an enumerable sequence of objects of type
    ///     <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. The type must have a parameterless
    ///     constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">An optional cancellation token that can be used to cancel the operation.</param>
    /// <returns>An asynchronous enumerable of objects of type <typeparamref name="T" /> parsed from the CSV file.</returns>
    IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts the data read from a given TextReader into an array of a specified type.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to create from the CSV data. Must be a reference type with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <typeparam name="TReader">The type of TextReader to read data from.</typeparam>
    /// <param name="reader">An instance of a TextReader from which to read the CSV data.</param>
    /// <returns>An array of objects of type T, representing the CSV data.</returns>
    T[] ToArray<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Reads data from a specified file path and converts it into an array of objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to deserialize each line into. Must be a class with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to be processed.</param>
    /// <returns>An array of objects of type <typeparamref name="T" /> representing the data from the CSV file.</returns>
    T[] ToArray<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously converts the contents of the provided TextReader to an array of objects of type T.
    ///     This operation can be cancelled using the provided CancellationToken.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <typeparam name="TReader">The type of TextReader used to read the CSV data.</typeparam>
    /// <param name="reader">The TextReader instance to read data from.</param>
    /// <param name="ct">A CancellationToken used to cancel the asynchronous operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing an array of objects of type T.</returns>
    ValueTask<T[]> ToArrayAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously parses a CSV file and converts the data into an array of objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">A CancellationToken that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A task representing the asynchronous operation that contains an array of objects of type
    ///     <typeparamref name="T" /> parsed from the CSV file.
    /// </returns>
    ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts the CSV data read by the specified TextReader into a list of objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to create from the CSV data. Must be a class with a parameterless constructor.</typeparam>
    /// <typeparam name="TReader">The type of the reader to use, which must inherit from TextReader.</typeparam>
    /// <param name="reader">The TextReader instance to read CSV data from.</param>
    /// <returns>A list of objects of type T created from the CSV data.</returns>
    List<T> ToList<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Reads a CSV file and converts its content into a list of objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of the objects to create from each row of the CSV.</typeparam>
    /// <param name="filePath">The path to the CSV file to read and parse.</param>
    /// <returns>A list of objects of type T, each representing a row in the CSV file.</returns>
    List<T> ToList<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously processes a CSV file using the specified <paramref name="reader" /> and converts the data into a
    ///     list of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to be created from the CSV data.</typeparam>
    /// <typeparam name="TReader">
    ///     The type of the reader used to parse the CSV data, which must be derived from
    ///     <see cref="TextReader" />.
    /// </typeparam>
    /// <param name="reader">The reader used to read the CSV data.</param>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A ValueTask representing the asynchronous operation, containing a list of objects of type
    ///     <typeparamref name="T" /> created from the CSV data.
    /// </returns>
    ValueTask<List<T>> ToListAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously parses a CSV file and converts its contents into a list of objects of type <typeparamref name="T" />
    ///     .
    /// </summary>
    /// <typeparam name="T">The type of objects to create from CSV rows. Must be a class with a parameterless constructor.</typeparam>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">A token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of objects of type
    ///     <typeparamref name="T" /> populated from the parsed CSV data.
    /// </returns>
    ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts the CSV data from a text reader into an enumerable collection of dictionaries,
    ///     with each dictionary representing a row in the CSV file, where the keys are column headers
    ///     and the values are the corresponding row data.
    /// </summary>
    /// <typeparam name="TReader">The type of the text reader.</typeparam>
    /// <param name="reader">
    ///     The text reader from which to read the CSV data. It should not be null and must be positioned at
    ///     the start of the data.
    /// </param>
    /// <returns>
    ///     An enumerable of dictionaries, where each dictionary maps column names to their respective values for each row
    ///     in the CSV.
    /// </returns>
    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable<TReader>(TReader reader)
        where TReader : TextReader;

    /// <summary>
    ///     Converts the contents of a CSV file at the specified file path into a sequence of dictionaries.
    ///     Each dictionary represents a row in the CSV, with keys being column headers and values being the cell contents.
    /// </summary>
    /// <param name="filePath">The file path to the CSV file to be parsed.</param>
    /// <returns>An IEnumerable of dictionaries, where each dictionary maps column headers to cell values.</returns>
    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath);

    /// <summary>
    ///     Asynchronously parses a CSV data source provided by a <see cref="TextReader" />
    ///     into an enumerable sequence of dictionaries. Each dictionary contains key-value
    ///     pairs where keys are column headers and values are the corresponding row values.
    /// </summary>
    /// <typeparam name="TReader">The type of the text reader, constrained to <see cref="TextReader" />.</typeparam>
    /// <param name="reader">An instance of the <typeparamref name="TReader" /> to read CSV data from.</param>
    /// <param name="ct">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An async enumerable sequence of dictionaries, each representing a row from the CSV.</returns>
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously parses a CSV file into an asynchronous enumerable of dictionaries,
    ///     where each dictionary represents a row and maps column headers to their respective values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to parse.</param>
    /// <param name="ct">An optional cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>An asynchronous enumerable of dictionaries, each representing a CSV row.</returns>
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default);

    /// <summary>
    ///     Converts the content from the provided text reader into an array of dictionaries,
    ///     where each dictionary represents a row in the CSV, mapping column names to their corresponding values.
    /// </summary>
    /// <param name="reader">The text reader from which to read the CSV data.</param>
    /// <typeparam name="TReader">The type of the text reader, which must inherit from TextReader.</typeparam>
    /// <returns>
    ///     An array of dictionaries, with each dictionary containing mappings from column names to their respective cell
    ///     values.
    /// </returns>
    Dictionary<string, string?>[] ToDictionaryArray<TReader>(TReader reader)
        where TReader : TextReader;

    /// <summary>
    ///     Converts the contents of a CSV file specified by the file path into an array of dictionaries,
    ///     where each dictionary represents a row and maps column headers to row values.
    /// </summary>
    /// <param name="filePath">The file path of the CSV file to be parsed.</param>
    /// <returns>
    ///     An array of dictionaries, each dictionary containing key-value pairs
    ///     where the keys are column headers and the values are the corresponding row values.
    /// </returns>
    Dictionary<string, string?>[] ToDictionaryArray(string filePath);

    /// <summary>
    ///     Asynchronously parses a CSV input from a reader into an array of dictionaries, where each dictionary's
    ///     keys represent the column names and values represent the corresponding field values for a row.
    /// </summary>
    /// <typeparam name="TReader">The type of the text reader used to read the CSV input.</typeparam>
    /// <param name="reader">An instance of <typeparamref name="TReader" /> that provides access to the CSV data.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing an array of dictionaries. Each dictionary
    ///     entry corresponds to a row from the CSV, with keys as column names and values as field values.
    /// </returns>
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously reads a CSV file from the specified file path and converts its contents into an array of
    ///     dictionaries,
    ///     where each dictionary represents a row with column names as keys and cell values as values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be read.</param>
    /// <param name="ct">Optional. A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an array of dictionaries, each
    ///     representing a CSV row.
    /// </returns>
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default);

    /// <summary>
    ///     Converts the input from a TextReader into a list of dictionaries where each dictionary
    ///     represents a row from the CSV file. Each dictionary's key is the column header and the
    ///     value is the corresponding cell data for that row and column.
    /// </summary>
    /// <param name="reader">The TextReader to read the CSV data from.</param>
    /// <typeparam name="TReader">The type of the TextReader.</typeparam>
    /// <returns>A list of dictionaries, where each dictionary represents a row from the CSV data.</returns>
    List<Dictionary<string, string?>> ToDictionaryList<TReader>(TReader reader)
        where TReader : TextReader;

    /// <summary>
    ///     Converts the contents of a CSV file specified by the file path into a list of dictionaries.
    ///     Each dictionary represents a row in the CSV, with the keys being the column headers and the values being the
    ///     corresponding field values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <returns>
    ///     A list where each element is a dictionary. Each dictionary maps column headers to their corresponding values
    ///     in the CSV file.
    /// </returns>
    List<Dictionary<string, string?>> ToDictionaryList(string filePath);

    /// <summary>
    ///     Asynchronously parses the content read by the specified reader into a list of dictionaries,
    ///     where each dictionary represents a row in the CSV file and maps column headers to their corresponding cell values.
    /// </summary>
    /// <typeparam name="TReader">The type of the reader which must inherit from TextReader.</typeparam>
    /// <param name="reader">The TextReader used to read the CSV content.</param>
    /// <param name="ct">A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of dictionaries,
    ///     where each dictionary contains key-value pairs mapping column headers to their current row cell values.
    /// </returns>
    ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader;

    /// <summary>
    ///     Asynchronously parses a CSV file into a list of dictionaries, where each dictionary represents a row in the CSV
    ///     file,
    ///     with column names as keys and cell values as corresponding values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to parse.</param>
    /// <param name="ct">An optional CancellationToken to cancel the operation.</param>
    /// <returns>A ValueTask representing the asynchronous operation, containing the list of dictionaries from the CSV file.</returns>
    ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(string filePath, CancellationToken ct = default);
    
    /// <summary>
    ///     Converts the given enumerable collection of data into a CSV formatted string.
    /// </summary>
    /// <typeparam name="T">The type of elements in the data collection.</typeparam>
    /// <param name="data">The collection of data to be parsed into a CSV string.</param>
    /// <returns>A string representing the CSV formatted version of the input data.</returns>
    string ParseToString<T>(IEnumerable<T> data);

    /// <summary>
    ///     Converts an enumerable collection of dictionaries into a CSV formatted string.
    /// </summary>
    /// <param name="data">
    ///     The enumerable collection of dictionaries where each dictionary represents a row in the CSV and each
    ///     key/value pair represents a column and its corresponding value.
    /// </param>
    /// <returns>A string representing the CSV formatted data derived from the input collection of dictionaries.</returns>
    string ParseToString(IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Asynchronously parses a given collection of data into a CSV formatted string.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the data collection to be parsed.</typeparam>
    /// <param name="data">The collection of data to be parsed into a CSV string.</param>
    /// <returns>A task representing the asynchronous operation, with a result of the CSV formatted string.</returns>
    ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data);

    /// <summary>
    ///     Asynchronously parses a collection of dictionaries into a CSV formatted string.
    /// </summary>
    /// <param name="data">
    ///     A collection of dictionaries where each dictionary represents a row of CSV data.
    ///     The keys in the dictionary represent the column headers, and the associated values
    ///     represent the corresponding data entries for that row.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation, with a string result that represents
    ///     the CSV formatted data derived from the input collection.
    /// </returns>
    ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Writes a collection of data to a CSV file specified by the file path.
    /// </summary>
    /// <typeparam name="T">The type of objects contained in the data collection.</typeparam>
    /// <param name="filePath">The path to the file where the data will be written.</param>
    /// <param name="data">The collection of data objects to be written to the file.</param>
    void ParseToFile<T>(string filePath, IEnumerable<T> data);

    /// <summary>
    ///     Writes the provided data to a file at the specified file path in CSV format.
    /// </summary>
    /// <param name="filePath">The path to the file where the CSV data will be written.</param>
    /// <param name="data">
    ///     An enumerable collection of dictionaries where each dictionary represents a row of data,
    ///     with keys representing column names and values representing the corresponding cell data.
    /// </param>
    void ParseToFile(string filePath, IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Asynchronously writes the provided data to a CSV file at the specified file path.
    /// </summary>
    /// <typeparam name="T">The type of the data elements to be written to the file.</typeparam>
    /// <param name="filePath">The path to the file where the data should be written.</param>
    /// <param name="data">The collection of data elements to be written to the CSV file.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data);

    /// <summary>
    ///     Asynchronously writes a collection of data in dictionary format to a CSV file.
    /// </summary>
    /// <param name="filePath">The path of the file where the data will be written.</param>
    /// <param name="data">An enumerable collection of dictionaries representing the CSV data to be written.</param>
    /// <returns>A ValueTask that represents the asynchronous write operation.</returns>
    ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Writes the provided enumerable data to the specified text writer in CSV format.
    /// </summary>
    /// <typeparam name="T">The type of elements in the data collection.</typeparam>
    /// <typeparam name="TWriter">The type of the writer, which must derive from TextWriter.</typeparam>
    /// <param name="data">The enumerable collection of data elements to be written.</param>
    /// <param name="writer">The text writer to which the CSV formatted data will be written.</param>
    void ParseToWriter<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter;

    /// <summary>
    ///     Writes a collection of dictionaries to a text writer in CSV format.
    ///     Each dictionary represents a row in the CSV, where the keys are column headers
    ///     and the values are the corresponding entries for that row.
    /// </summary>
    /// <typeparam name="TWriter">The type of the writer, which must inherit from <see cref="TextWriter" />.</typeparam>
    /// <param name="data">
    ///     The enumerable collection of dictionaries to be written. Each dictionary contains key-value pairs
    ///     representing a CSV row.
    /// </param>
    /// <param name="writer">The writer to which the CSV data will be written.</param>
    void ParseToWriter<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter;

    /// <summary>
    ///     Asynchronously parses a collection of data objects into a CSV format and writes it to the specified text writer.
    /// </summary>
    /// <typeparam name="T">The type of data objects contained in the IEnumerable.</typeparam>
    /// <typeparam name="TWriter">The type of the text writer to which the CSV data will be written.</typeparam>
    /// <param name="data">The collection of data objects to be serialized into CSV format.</param>
    /// <param name="writer">The text writer where the CSV formatted data will be written.</param>
    /// <returns>A task that represents the asynchronous write operation.</returns>
    ValueTask ParseToWriterAsync<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter;

    /// <summary>
    ///     Asynchronously parses the given enumerable of data dictionaries and writes it to the specified text writer in CSV
    ///     format.
    /// </summary>
    /// <typeparam name="TWriter">The type of the text writer which must inherit from TextWriter.</typeparam>
    /// <param name="data">
    ///     An enumerable collection of dictionaries representing the data to be written to the writer. Each
    ///     dictionary entry corresponds to a CSV row.
    /// </param>
    /// <param name="writer">The text writer where the CSV content will be written.</param>
    /// <returns>A ValueTask that represents the asynchronous write operation.</returns>
    ValueTask ParseToWriterAsync<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter;

}
