// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Provides methods for parsing CSV data into generic collections,
///     enumerable sequences, or dictionaries, as well as serializing these structures back into CSV format.
///     Includes synchronous and asynchronous operations for varied data sources and outputs.
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
    /// <param name="reader">The text reader from which to read the CSV data.</param>
    /// <returns>An enumerable collection of objects of type <typeparamref name="T" /> representing the parsed CSV records.</returns>
    IEnumerable<T> ToEnumerable<T>(TextReader reader)
        where T : class, new();

    /// <summary>
    ///     Converts the CSV file located at the specified file path into an enumerable collection of objects of type
    ///     <typeparamref name="T" />.
    ///     The method reads the CSV data from the file and maps it to instances of the specified type.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="filePath">The path of the CSV file to parse.</param>
    /// <returns>An enumerable collection of objects of type <typeparamref name="T" /> representing the parsed records.</returns>
    IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously parses CSV data from a text reader into an enumerable collection of objects of type
    ///     <typeparamref name="T" />.
    ///     Allows efficient and asynchronous processing of CSV records, suitable for handling large data sets.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the objects to create from the CSV records. Must be a class with a parameterless
    ///     constructor.
    /// </typeparam>
    /// <param name="reader">The text reader containing the CSV data to be parsed.</param>
    /// <param name="ct">An optional cancellation token to monitor for cancellation requests during the asynchronous operation.</param>
    /// <returns>
    ///     An asynchronous enumerable collection of objects of type <typeparamref name="T" /> representing the parsed
    ///     data.
    /// </returns>
    IAsyncEnumerable<T> ToEnumerableAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Asynchronously parses a CSV input from a text reader into an enumerable sequence of objects of type
    ///     <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. The type must have a parameterless constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">An optional cancellation token that can be used to cancel the operation.</param>
    /// <returns>An asynchronous enumerable of objects of type <typeparamref name="T" /> representing the parsed CSV records.</returns>
    IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts the data read from a TextReader into an array of a specified type.
    ///     The method parses the CSV data and maps it to objects of the provided type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to create from the CSV data. Must be a reference type with a parameterless constructor.
    /// </typeparam>
    /// <param name="reader">An instance of a TextReader used to read the input CSV data.</param>
    /// <returns>
    ///     An array of objects of type <typeparamref name="T" /> representing the parsed data from the CSV.
    /// </returns>
    T[] ToArray<T>(TextReader reader)
        where T : class, new();

    /// <summary>
    ///     Reads data from a specified file path and converts it into an array of objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to deserialize each row into. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the file containing the CSV data.</param>
    /// <returns>An array of objects of type <typeparamref name="T" /> representing the parsed data from the CSV file.</returns>
    T[] ToArray<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously converts CSV data from the provided <see cref="TextReader" /> into an array of objects of the
    ///     specified type.
    ///     The method parses the CSV input and maps it to an array of objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="reader">The <see cref="TextReader" /> instance containing the CSV data to parse.</param>
    /// <param name="ct">A <see cref="CancellationToken" /> that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A <see cref="ValueTask" /> representing the asynchronous operation, containing an array of objects of type
    ///     <typeparamref name="T" /> created from the parsed CSV data.
    /// </returns>
    ValueTask<T[]> ToArrayAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new();

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
    ///     A task that represents the asynchronous operation, containing an array of objects of type
    ///     <typeparamref name="T" /> parsed from the CSV file.
    /// </returns>
    ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts the CSV data read from the specified text reader into a list of objects of type <typeparamref name="T" />.
    ///     The method processes the data and returns a strongly-typed list of deserialized objects.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to create from the CSV data. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="reader">The text reader used to load and parse the CSV data.</param>
    /// <returns>A list of objects of type <typeparamref name="T" /> created from the CSV data.</returns>
    List<T> ToList<T>(TextReader reader)
        where T : class, new();

    /// <summary>
    ///     Reads a CSV file and converts its content into a list of objects of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the CSV data. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to read and parse.</param>
    /// <returns>A list of objects of type <typeparamref name="T" /> representing the parsed CSV records.</returns>
    List<T> ToList<T>(string filePath)
        where T : class, new();

    /// <summary>
    ///     Asynchronously reads and processes the CSV data from the provided text reader,
    ///     converting it into a list of objects of the specified type.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to be created from the parsed CSV data. The specified type must
    ///     be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="reader">The text reader used to read the CSV data.</param>
    /// <param name="ct">
    ///     The cancellation token that can be used to observe and optionally cancel the asynchronous operation.
    /// </param>
    /// <returns>
    ///     A ValueTask that resolves to a list of objects of type <typeparamref name="T" />
    ///     created from the CSV data.
    /// </returns>
    ValueTask<List<T>> ToListAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Asynchronously parses a CSV file and converts its contents into a list of objects of type <typeparamref name="T" />
    ///     .
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects to create from CSV rows. Must be a class with a parameterless constructor.
    /// </typeparam>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">A token to monitor for cancellation requests.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of objects of type
    ///     <typeparamref name="T" /> populated from the parsed CSV data.
    /// </returns>
    ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new();

    /// <summary>
    ///     Converts CSV data from a text reader into an enumerable collection of dictionaries,
    ///     where each dictionary corresponds to a row in the CSV and maps column headers to their respective values.
    /// </summary>
    /// <param name="reader">
    ///     The text reader object used to read the CSV data. It must be initialized and positioned
    ///     at the beginning of the CSV content.
    /// </param>
    /// <returns>
    ///     An enumerable collection of dictionaries, with each dictionary representing a row where
    ///     the keys are column headers and the values are the corresponding row data.
    /// </returns>
    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(TextReader reader);

    /// <summary>
    ///     Converts data from a file into an enumerable collection of dictionaries,
    ///     where each dictionary represents a row in the CSV file. The dictionary keys correspond
    ///     to column headers, and the values correspond to the respective cell contents.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <returns>
    ///     An enumerable collection of dictionaries. Each dictionary maps column names to the
    ///     contents of the respective cells in a row.
    /// </returns>
    IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath);

    /// <summary>
    ///     Asynchronously parses CSV data from a text reader into an asynchronous sequence of dictionaries.
    ///     Each dictionary represents a row in the CSV file, with column headers as the keys and their
    ///     corresponding row values as the values.
    /// </summary>
    /// <param name="reader">The text reader providing the CSV data to parse.</param>
    /// <param name="ct">A <see cref="CancellationToken" /> to observe while parsing the CSV data asynchronously.</param>
    /// <returns>
    ///     An asynchronous enumerable collection of dictionaries where each dictionary represents a row.
    ///     The keys in the dictionary correspond to the column headers, and the values correspond to the
    ///     respective row values in the CSV file.
    /// </returns>
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(TextReader reader, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously parses a CSV file into an asynchronous enumerable of dictionaries,
    ///     where each dictionary represents a row and maps column headers to their respective values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">An optional cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>
    ///     An asynchronous enumerable of dictionaries, where each dictionary contains the column header as the key and
    ///     the corresponding value from the CSV row.
    /// </returns>
    IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default);

    /// <summary>
    ///     Converts the content from the provided text reader into an array of dictionaries,
    ///     where each dictionary represents a row in the CSV, mapping column names to their corresponding values.
    /// </summary>
    /// <param name="reader">The text reader from which to read the CSV data.</param>
    /// <returns>
    ///     An array of dictionaries, with each dictionary containing mappings from column names to their respective cell
    ///     values.
    /// </returns>
    Dictionary<string, string?>[] ToDictionaryArray(TextReader reader);

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
    /// <param name="reader">The text reader used to read the CSV input.</param>
    /// <param name="ct">A cancellation token used to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing an array of dictionaries. Each dictionary
    ///     represents a row from the CSV with keys as column names and values as field data.
    /// </returns>
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(TextReader reader, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously reads a CSV file from the specified file path and converts its contents into an array of
    ///     dictionaries, where each dictionary represents a row with column names as keys and cell values as values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be read.</param>
    /// <param name="ct">Optional. A CancellationToken to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task representing the asynchronous operation. The task result contains an array of dictionaries, where
    ///     each dictionary represents a single row of the CSV file with column names as keys and corresponding cell values.
    /// </returns>
    ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default);

    /// <summary>
    ///     Converts the input from a TextReader into a list of dictionaries, where each dictionary
    ///     represents a row from the CSV data. Each dictionary's key corresponds to the column header
    ///     and the value corresponds to the respective cell data for that row and column.
    /// </summary>
    /// <param name="reader">
    ///     The TextReader used to read the CSV input.
    /// </param>
    /// <returns>
    ///     A list of dictionaries, where each dictionary represents a single row from the CSV data.
    ///     The keys in the dictionary are the column headers, and the values are the corresponding
    ///     cell data for each row.
    /// </returns>
    List<Dictionary<string, string?>> ToDictionaryList(TextReader reader);

    /// <summary>
    ///     Converts the contents of a CSV file specified by the file path into a list of dictionaries.
    ///     Each dictionary represents a row in the CSV, with the keys being the column headers and the values being the
    ///     corresponding field values in that row.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <returns>
    ///     A list of dictionaries where each dictionary represents a single row of the CSV file.
    ///     The keys in the dictionary correspond to the headers of the CSV file, and the values correspond to the row's values
    ///     for those headers.
    /// </returns>
    List<Dictionary<string, string?>> ToDictionaryList(string filePath);

    /// <summary>
    ///     Asynchronously parses the content read from the specified text reader into a list of dictionaries,
    ///     where each dictionary represents a row in the CSV file, mapping column headers to their corresponding cell values.
    /// </summary>
    /// <param name="reader">The text reader used to read the CSV data.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains a list of dictionaries,
    ///     where each dictionary includes key-value pairs mapping column headers to values from the corresponding row.
    /// </returns>
    ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(TextReader reader, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously parses a CSV file into a list of dictionaries, where each dictionary represents a row in the CSV
    ///     file, with column names as keys and cell values as corresponding values.
    /// </summary>
    /// <param name="filePath">The path to the CSV file to be parsed.</param>
    /// <param name="ct">An optional CancellationToken to signal cancellation of the asynchronous operation.</param>
    /// <returns>
    ///     A ValueTask representing the asynchronous operation, containing a list of dictionaries where each dictionary
    ///     represents a CSV row.
    /// </returns>
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
    ///     The enumerable collection of dictionaries where each dictionary represents a row in the CSV
    ///     and each key-value pair corresponds to a column and its value.
    /// </param>
    /// <returns>A string representing the CSV formatted data derived from the input collection of dictionaries.</returns>
    string ParseToString(IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Asynchronously parses the given collection of objects into a CSV formatted string representation.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects contained in the provided data collection.
    /// </typeparam>
    /// <param name="data">
    ///     The collection of objects to be converted into the CSV format.
    /// </param>
    /// <param name="ct">
    ///     An optional cancellation token to cancel the operation if needed.
    /// </param>
    /// <returns>
    ///     A task representing the asynchronous operation that returns the resulting CSV formatted string.
    /// </returns>
    ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously parses a collection of dictionaries into a CSV formatted string.
    ///     Each dictionary in the input represents a row of CSV data where the keys represent
    ///     column headers and the associated values are the corresponding data entries.
    /// </summary>
    /// <param name="data">
    ///     A collection of dictionaries containing the data to be converted into a CSV formatted string.
    ///     Each dictionary represents a row, with keys as column headers and values as the row data.
    /// </param>
    /// <param name="ct">
    ///     An optional cancellation token to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The result is a string containing the CSV representation of the input data.
    /// </returns>
    ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data, CancellationToken ct = default);

    /// <summary>
    ///     Writes a collection of objects of a specified type to a CSV file at the given file path.
    ///     This method serializes the provided data into CSV format and saves it to the specified location.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of objects in the data collection. Objects of this type will be serialized into CSV format.
    /// </typeparam>
    /// <param name="filePath">
    ///     The path to the file where the serialized CSV data will be written.
    /// </param>
    /// <param name="data">
    ///     The collection of objects to be written to the CSV file. Each object represents a row in the CSV file.
    /// </param>
    void ParseToFile<T>(string filePath, IEnumerable<T> data);

    /// <summary>
    ///     Writes the provided data to a file at the specified file path in CSV format.
    /// </summary>
    /// <param name="filePath">
    ///     The path to the file where the CSV data will be written.
    /// </param>
    /// <param name="data">
    ///     An enumerable collection of dictionaries where each dictionary represents a row of data,
    ///     with keys representing column names and values representing the corresponding cell data.
    /// </param>
    void ParseToFile(string filePath, IEnumerable<Dictionary<string, string?>> data);

    /// <summary>
    ///     Asynchronously writes the provided collection of data to a CSV file at the specified file path.
    ///     The method serializes the given objects of type <typeparamref name="T" /> and creates a CSV file.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the data elements to be written to the CSV file.
    /// </typeparam>
    /// <param name="filePath">
    ///     The path to the file where the CSV data will be written.
    /// </param>
    /// <param name="data">
    ///     The collection of objects of type <typeparamref name="T" /> to be serialized and written to the CSV file.
    /// </param>
    /// <param name="ct">
    ///     A cancellation token that can be used to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation of writing the data to the CSV file.
    /// </returns>
    ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously writes a collection of data in dictionary format to a CSV file at the specified file path.
    /// </summary>
    /// <param name="filePath">The path of the file where the CSV data will be written.</param>
    /// <param name="data">
    ///     An enumerable collection of dictionaries representing the data to be written, where each dictionary
    ///     corresponds to a CSV record.
    /// </param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation. Optional.</param>
    /// <returns>A ValueTask representing the asynchronous write operation.</returns>
    ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data, CancellationToken ct = default);

    /// <summary>
    ///     Writes the provided enumerable collection of data to the specified text writer in CSV format.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of elements in the collection to be written to the CSV output.
    /// </typeparam>
    /// <param name="data">
    ///     The enumerable collection of data elements to be serialized and written in CSV format.
    /// </param>
    /// <param name="writer">
    ///     The text writer instance where the CSV data will be written.
    /// </param>
    void ParseToWriter<T>(IEnumerable<T> data, TextWriter writer);

    /// <summary>
    ///     Writes a collection of dictionaries to a text writer in CSV format.
    ///     Each dictionary represents a row in the CSV, with the keys used as column headers
    ///     and the values as the corresponding row values.
    /// </summary>
    /// <param name="data">
    ///     The collection of dictionaries to write. Each dictionary represents a single CSV row,
    ///     where the keys are the column headers and the values are the cell contents.
    /// </param>
    /// <param name="writer">
    ///     The text writer to which the CSV data will be written.
    /// </param>
    void ParseToWriter(IEnumerable<Dictionary<string, string?>> data, TextWriter writer);

    /// <summary>
    ///     Asynchronously parses a collection of data objects into a CSV format and writes it to the specified text writer.
    /// </summary>
    /// <typeparam name="T">The type of data objects contained in the <paramref name="data" /> collection.</typeparam>
    /// <param name="data">The collection of data objects to be serialized into CSV format.</param>
    /// <param name="writer">The text writer where the CSV formatted data will be written.</param>
    /// <param name="ct">An optional <see cref="CancellationToken" /> to cancel the write operation.</param>
    /// <returns>A task representing the asynchronous operation of writing the data in CSV format.</returns>
    ValueTask ParseToWriterAsync<T>(IEnumerable<T> data, TextWriter writer, CancellationToken ct = default);

    /// <summary>
    ///     Asynchronously parses a collection of dictionaries representing data and writes it to a specified text writer in
    ///     CSV format.
    /// </summary>
    /// <param name="data">
    ///     An enumerable collection of dictionaries where each dictionary represents a row in the CSV data,
    ///     with keys as column headers and values as the corresponding cell contents.
    /// </param>
    /// <param name="writer">
    ///     The text writer to which the generated CSV data will be written.
    /// </param>
    /// <param name="ct">
    ///     An optional cancellation token to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    ///     A <see cref="ValueTask" /> representing the asynchronous operation of writing the data to the output.
    /// </returns>
    ValueTask ParseToWriterAsync(IEnumerable<Dictionary<string, string?>> data, TextWriter writer, CancellationToken ct = default);

    /// <summary>
    ///     Clears any cached data or metadata used by the parser.
    /// </summary>
    void ClearCaches();
}
