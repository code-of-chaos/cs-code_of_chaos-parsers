// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     Provides functionality to parse CSV files into various collection types.
/// </summary>
public class CsvParser(CsvParserConfig config) : ICsvParser {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Creates an instance of <see cref="CsvParser" /> using the specified configuration action.
    /// </summary>
    /// <param name="configAction">
    ///     An action to configure the <see cref="CsvParserConfig" /> used by the parser.
    ///     The action allows the caller to specify options such as column delimiter, quote character, etc.
    /// </param>
    /// <returns>
    ///     A configured instance of <see cref="CsvParser" /> ready to parse CSV data according to
    ///     the specified configuration.
    /// </returns>
    public static CsvParser FromConfig(Action<CsvParserConfig> configAction) {
        var config = new CsvParserConfig();
        configAction(config);
        return new CsvParser(config);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Input Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region ToEnumerable
    /// <inheritdoc />
    public IEnumerable<T> ToEnumerable<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader);

    /// <inheritdoc />
    public IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new() => FromTextReader<T, StreamReader>(new StreamReader(filePath));

    /// <inheritdoc />
    public IAsyncEnumerable<T> ToEnumerableAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader => FromTextReaderAsync<T, TReader>(reader, ct);

    /// <inheritdoc />
    public IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new() => FromTextReaderAsync<T, StreamReader>(new StreamReader(filePath), ct);
    #endregion
    #region ToArray
    /// <inheritdoc />
    public T[] ToArray<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader).ToArray();

    /// <inheritdoc />
    public T[] ToArray<T>(string filePath)
        where T : class, new() => FromTextReader<T, StreamReader>(new StreamReader(filePath)).ToArray();

    /// <inheritdoc />
    public async ValueTask<T[]> ToArrayAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, TReader>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StreamReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, StreamReader>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToArray();
    }
    #endregion
    #region ToList
    /// <inheritdoc />
    public List<T> ToList<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader => FromTextReader<T, TReader>(reader).ToList();

    /// <inheritdoc />
    public List<T> ToList<T>(string filePath)
        where T : class, new() {
        var reader = new StreamReader(filePath);
        return FromTextReader<T, StreamReader>(reader).ToList();
    }

    /// <inheritdoc />
    public async ValueTask<List<T>> ToListAsync<T, TReader>(TReader reader, CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, TReader>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToList();
    }

    /// <inheritdoc />
    public async ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StringReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T, StringReader>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToList();
    }
    #endregion
    #region ToDictionaryEnumerable
    /// <inheritdoc />
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader);

    /// <inheritdoc />
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath));

    /// <inheritdoc />
    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader => FromTextReaderToDictionaryAsync(reader, ct);

    /// <inheritdoc />
    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default)
        => FromTextReaderToDictionaryAsync(new StreamReader(filePath), ct);
    #endregion
    #region ToDictionaryArray
    /// <inheritdoc />
    public Dictionary<string, string?>[] ToDictionaryArray<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader).ToArray();

    /// <inheritdoc />
    public Dictionary<string, string?>[] ToDictionaryArray(string filePath) => FromTextReaderToDictionary(new StreamReader(filePath)).ToArray();

    /// <inheritdoc />
    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        return results.ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StreamReader(filePath);
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        
        results.TrimExcess();
        return results.ToArray();
    }
    #endregion
    #region ToDictionaryList
    /// <inheritdoc />
    public List<Dictionary<string, string?>> ToDictionaryList<TReader>(TReader reader)
        where TReader : TextReader => FromTextReaderToDictionary(reader).ToList();

    /// <inheritdoc />
    public List<Dictionary<string, string?>> ToDictionaryList(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath)).ToList();

    /// <inheritdoc />
    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync<TReader>(TReader reader, CancellationToken ct = default)
        where TReader : TextReader {
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);

        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        
        results.TrimExcess();
        return results;
    }

    /// <inheritdoc />
    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(string filePath, CancellationToken ct = default) {
        using var reader = new StringReader(filePath);
        var results = new List<Dictionary<string, string?>>(config.InitialCapacity);
        await foreach (Dictionary<string, string?> item in FromTextReaderToDictionaryAsync(reader, ct)) {
            results.Add(item);
        }
        
        results.TrimExcess();
        return results;
    }
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Output
    // -----------------------------------------------------------------------------------------------------------------
    #region ParseToString
    /// <inheritdoc />
    public string ParseToString<T>(IEnumerable<T> data) {
        using var writer = new StringWriter();
        FromDataToTextWriter(writer, data);
        return writer.ToString();
    }

    /// <inheritdoc />
    public string ParseToString(IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StringWriter();
        FromDictionaryToTextWriter(writer, data);
        return writer.ToString();
    }

    /// <inheritdoc />
    public async ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data) {
        await using var writer = new StringWriter();
        await FromDataToTextWriterAsync(writer, data);
        return writer.ToString();
    }

    /// <inheritdoc />
    public async ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StringWriter();
        await FromDictionaryToTextWriterAsync(writer, data);
        return writer.ToString();
    }
    #endregion
    #region ParseToFileAsync
    /// <inheritdoc />
    public void ParseToFile<T>(string filePath, IEnumerable<T> data) {
        using var writer = new StreamWriter(filePath);
        FromDataToTextWriter(writer, data);
    }

    /// <inheritdoc />
    public void ParseToFile(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        using var writer = new StreamWriter(filePath);
        FromDictionaryToTextWriter(writer, data);
    }

    /// <inheritdoc />
    public async ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data) {
        await using var writer = new StreamWriter(filePath);
        await FromDataToTextWriterAsync(writer, data);
    }

    /// <inheritdoc />
    public async ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data) {
        await using var writer = new StreamWriter(filePath);
        await FromDictionaryToTextWriterAsync(writer, data);
    }
    #endregion
    #region ParseToWriter
    /// <inheritdoc />
    public void ParseToWriter<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter => FromDataToTextWriter(writer, data);

    /// <inheritdoc />
    public void ParseToWriter<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter => FromDictionaryToTextWriter(writer, data);

    /// <inheritdoc />
    public async ValueTask ParseToWriterAsync<T, TWriter>(IEnumerable<T> data, TWriter writer)
        where TWriter : TextWriter => await FromDataToTextWriterAsync(writer, data);

    /// <inheritdoc />
    public async ValueTask ParseToWriterAsync<TWriter>(IEnumerable<Dictionary<string, string?>> data, TWriter writer)
        where TWriter : TextWriter => await FromDictionaryToTextWriterAsync(writer, data);
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Actual Parsers
    // -----------------------------------------------------------------------------------------------------------------
    #region Generic Type Parsing
    private IEnumerable<T> FromTextReader<T, TReader>(TReader reader)
        where T : class, new()
        where TReader : TextReader {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<T>(config.BatchSize);

        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);
                var obj = new T();
                SetPropertyFromCsvColumn(obj, headerColumns, values);
                batch.Add(obj);
            }

            foreach (T item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private async IAsyncEnumerable<T> FromTextReaderAsync<T, TReader>(TReader reader, [EnumeratorCancellation] CancellationToken ct = default)
        where T : class, new()
        where TReader : TextReader {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<T>(config.BatchSize);

        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (!ct.IsCancellationRequested) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);
                var obj = new T();

                SetPropertyFromCsvColumn(obj, headerColumns, values);
                batch.Add(obj);
            }

            foreach (T item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private void SetPropertyFromCsvColumn<T>(T? value, string[] headerColumns, string[] values) where T : class, new() {
        if (value is null) return;

        foreach (PropertyInfo prop in value.GetType().GetProperties()) {
            int columnIndex = Attribute.GetCustomAttribute(prop, typeof(CsvColumnAttribute)) is CsvColumnAttribute attribute
                ? Array.IndexOf(headerColumns, attribute.Name)
                : Array.IndexOf(headerColumns, prop.Name);

            if (columnIndex == -1) continue;

            try {
                object propertyValue = Convert.ChangeType(values[columnIndex], prop.PropertyType);
                prop.SetValue(value, propertyValue);
            }
            catch (Exception) {
                if (!config.LogErrors) return;

                throw;
            }
        }
    }
    #endregion
    #region Dictionary Parsing
    private IEnumerable<Dictionary<string, string?>> FromTextReaderToDictionary<TReader>(TReader reader)
        where TReader : TextReader {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (reader.ReadLine() is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = reader.ReadLine()) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);

                var dict = new Dictionary<string, string?>();
                for (int j = 0; j < headerColumns.Length; j++) {
                    string value = values[j];
                    dict[headerColumns[j]] = value.IsNotNullOrEmpty() ? value : null;
                }
                batch.Add(dict);
            }

            foreach (Dictionary<string, string?> item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }

    private async IAsyncEnumerable<Dictionary<string, string?>> FromTextReaderToDictionaryAsync<TReader>(TReader reader, [EnumeratorCancellation] CancellationToken ct = default)
        where TReader : TextReader {
        string[] headerColumns = [];
        int batchSize = config.BatchSize;
        var batch = new List<Dictionary<string, string?>>();
        if (await reader.ReadLineAsync(ct) is {} lineFull) {
            headerColumns = lineFull.Split(config.ColumnSplit);
        }

        while (true) {
            string? line = null;
            for (int i = 0; i < batchSize && (line = await reader.ReadLineAsync(ct)) != null; i++) {
                string[] values = line.Split(config.ColumnSplit);

                var dict = new Dictionary<string, string?>();
                for (int j = 0; j < headerColumns.Length; j++) {
                    string value = values[j];
                    dict[headerColumns[j]] = value.IsNotNullOrEmpty() ? value : null;
                }
                batch.Add(dict);
            }

            foreach (Dictionary<string, string?> item in batch) {
                yield return item;
            }

            batch.Clear();
            if (line == null) break;
        }
    }
    #endregion

    #region Generic Type Writer
    private void FromDataToTextWriter<T, TWriter>(TWriter writer, IEnumerable<T> data)
        where TWriter : TextWriter {
        // Write header row
        T[] array = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(array.FirstOrDefault());// Dirty but it will work

        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in array) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }
    }

    private async Task FromDataToTextWriterAsync<T, TWriter>(TWriter writer, IEnumerable<T> data)
        where TWriter : TextWriter {
        // Write header row
        T[] array = data as T[] ?? data.ToArray();
        PropertyInfo[] propertyInfos = GetCsvProperties(array.FirstOrDefault());

        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders(propertyInfos).ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in array) {
            string[] values = GetCsvValues(obj, propertyInfos).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
        }
    }


    private static PropertyInfo[] GetCsvProperties<T>(T? obj) => obj?
            .GetType()
            .GetProperties()
            .ToArray()
        ?? [];

    private IEnumerable<string> GetCsvHeaders(PropertyInfo[] propertyInfos) {
        return propertyInfos
            .Select(p => {
                if (p.GetCustomAttribute<CsvColumnAttribute>() is not {} attribute)
                    return config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return config.UseLowerCaseHeaders
                    ? attribute.NameLowerInvariant
                    : attribute.Name;
            });
    }

    private static IEnumerable<string> GetCsvValues<T>(T? obj, PropertyInfo[] propertyInfos) {
        if (obj is null) return [];

        PropertyInfo[] properties = propertyInfos.Length != 0
            ? propertyInfos
            : obj.GetType().GetProperties();

        return properties
            .Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
    }
    #endregion
    #region Dictionary Writer
    private void FromDictionaryToTextWriter<TWriter>(TWriter writer, IEnumerable<Dictionary<string, string?>> data)
        where TWriter : TextWriter {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            writer.WriteLine(string.Join(config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            writer.WriteLine(string.Join(config.ColumnSplit, values));
        }
    }

    private async Task FromDictionaryToTextWriterAsync<TWriter>(TWriter writer, IEnumerable<Dictionary<string, string?>> data)
        where TWriter : TextWriter {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, headers));
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, values));
        }
    }
    #endregion
}
