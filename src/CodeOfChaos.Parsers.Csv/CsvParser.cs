// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------

// ReSharper disable MemberCanBePrivate.Global
/// <summary>
///     Provides functionality to parse CSV files into various collection types.
/// </summary>
public class CsvParser(CsvParserConfig config) : ICsvParser {
    protected readonly ConcurrentDictionary<Type, string[]> HeaderCache = new();
    protected readonly ConcurrentDictionary<Type, PropertyInfo[]> PropertyCache = new();
    /// <inheritdoc />
    public void ClearCaches() {
        PropertyCache.Clear();
        HeaderCache.Clear();
    }
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
    /// <summary>
    ///     Creates an instance of <see cref="CsvParser" /> using the default configuration.
    /// </summary>
    /// <returns>
    ///     A new instance of <see cref="CsvParser" /> initialized with the default settings, ready
    ///     to parse CSV data.
    /// </returns>
    public static CsvParser FromDefaultConfig() => new(new CsvParserConfig());
    protected PropertyInfo[] GetCsvProperties<T>() {
        Type type = typeof(T);
        if (PropertyCache.TryGetValue(type, out PropertyInfo[]? propertyInfos)) return propertyInfos;

        PropertyInfo[] propertyInfosArray = type.GetProperties().ToArray();
        PropertyCache[type] = propertyInfosArray;
        return propertyInfosArray;
    }
    protected string[] GetCsvHeaders<T>() {
        Type type = typeof(T);
        if (HeaderCache.TryGetValue(type, out string[]? headers)) return headers;

        string[] headersArray = GetCsvProperties<T>()
            .Select(p => {
                if (p.GetCustomAttribute<CsvColumnAttribute>() is not {} attribute)
                    return config.UseLowerCaseHeaders ? p.Name.ToLowerInvariant() : p.Name;

                return config.UseLowerCaseHeaders
                    ? attribute.NameLowerInvariant
                    : attribute.Name;
            })
            .ToArray();

        HeaderCache[type] = headersArray;
        return headersArray;
    }
    protected IEnumerable<string> GetCsvValues<T>(T? obj) {
        if (obj is null) return [];

        return GetCsvProperties<T>()
            .Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------

    // -----------------------------------------------------------------------------------------------------------------
    // Input Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region ToEnumerable
    /// <inheritdoc />
    public IEnumerable<T> ToEnumerable<T>(TextReader reader)
        where T : class, new() => FromTextReader<T>(reader);

    /// <inheritdoc />
    public IEnumerable<T> ToEnumerable<T>(string filePath)
        where T : class, new() => FromTextReader<T>(new StreamReader(filePath));

    /// <inheritdoc />
    public IAsyncEnumerable<T> ToEnumerableAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new() => FromTextReaderAsync<T>(reader, ct);

    /// <inheritdoc />
    public IAsyncEnumerable<T> ToEnumerableAsync<T>(string filePath, CancellationToken ct = default)
        where T : class, new() => FromTextReaderAsync<T>(new StreamReader(filePath), ct);
    #endregion
    #region ToArray
    /// <inheritdoc />
    public T[] ToArray<T>(TextReader reader)
        where T : class, new() => FromTextReader<T>(reader).ToArray();

    /// <inheritdoc />
    public T[] ToArray<T>(string filePath)
        where T : class, new() => FromTextReader<T>(new StreamReader(filePath)).ToArray();

    /// <inheritdoc />
    public async ValueTask<T[]> ToArrayAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new() {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToArray();
    }

    /// <inheritdoc />
    public async ValueTask<T[]> ToArrayAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StreamReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToArray();
    }
    #endregion
    #region ToList
    /// <inheritdoc />
    public List<T> ToList<T>(TextReader reader)
        where T : class, new() => FromTextReader<T>(reader).ToList();

    /// <inheritdoc />
    public List<T> ToList<T>(string filePath)
        where T : class, new() {
        var reader = new StreamReader(filePath);
        return FromTextReader<T>(reader).ToList();
    }

    /// <inheritdoc />
    public async ValueTask<List<T>> ToListAsync<T>(TextReader reader, CancellationToken ct = default)
        where T : class, new() {
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToList();
    }

    /// <inheritdoc />
    public async ValueTask<List<T>> ToListAsync<T>(string filePath, CancellationToken ct = default) where T : class, new() {
        using var reader = new StringReader(filePath);
        var results = new List<T>(config.InitialCapacity);

        await foreach (T item in FromTextReaderAsync<T>(reader, ct)) {
            results.Add(item);
        }

        results.TrimExcess();
        return results.ToList();
    }
    #endregion
    #region ToDictionaryEnumerable
    /// <inheritdoc />
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(TextReader reader)
        => FromTextReaderToDictionary(reader);

    /// <inheritdoc />
    public IEnumerable<Dictionary<string, string?>> ToDictionaryEnumerable(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath));

    /// <inheritdoc />
    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(TextReader reader, CancellationToken ct = default)
        => FromTextReaderToDictionaryAsync(reader, ct);

    /// <inheritdoc />
    public IAsyncEnumerable<Dictionary<string, string?>> ToDictionaryEnumerableAsync(string filePath, CancellationToken ct = default)
        => FromTextReaderToDictionaryAsync(new StreamReader(filePath), ct);
    #endregion
    #region ToDictionaryArray
    /// <inheritdoc />
    public Dictionary<string, string?>[] ToDictionaryArray(TextReader reader)
        => FromTextReaderToDictionary(reader).ToArray();

    /// <inheritdoc />
    public Dictionary<string, string?>[] ToDictionaryArray(string filePath) => FromTextReaderToDictionary(new StreamReader(filePath)).ToArray();

    /// <inheritdoc />
    public async ValueTask<Dictionary<string, string?>[]> ToDictionaryArrayAsync(TextReader reader, CancellationToken ct = default) {
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
    public List<Dictionary<string, string?>> ToDictionaryList(TextReader reader)
        => FromTextReaderToDictionary(reader).ToList();

    /// <inheritdoc />
    public List<Dictionary<string, string?>> ToDictionaryList(string filePath)
        => FromTextReaderToDictionary(new StreamReader(filePath)).ToList();

    /// <inheritdoc />
    public async ValueTask<List<Dictionary<string, string?>>> ToDictionaryListAsync(TextReader reader, CancellationToken ct = default) {
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
    public async ValueTask<string> ParseToStringAsync<T>(IEnumerable<T> data, CancellationToken ct = default) {
        await using var writer = new StringWriter();
        await FromDataToTextWriterAsync(writer, data, ct);
        return writer.ToString();
    }

    /// <inheritdoc />
    public async ValueTask<string> ParseToStringAsync(IEnumerable<Dictionary<string, string?>> data, CancellationToken ct = default) {
        await using var writer = new StringWriter();
        await FromDictionaryToTextWriterAsync(writer, data, ct);
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
    public async ValueTask ParseToFileAsync<T>(string filePath, IEnumerable<T> data, CancellationToken ct = default) {
        await using var writer = new StreamWriter(filePath);
        await FromDataToTextWriterAsync(writer, data, ct);
    }

    /// <inheritdoc />
    public async ValueTask ParseToFileAsync(string filePath, IEnumerable<Dictionary<string, string?>> data, CancellationToken ct = default) {
        await using var writer = new StreamWriter(filePath);
        await FromDictionaryToTextWriterAsync(writer, data, ct);
    }
    #endregion
    #region ParseToWriter
    /// <inheritdoc />
    public void ParseToWriter<T>(IEnumerable<T> data, TextWriter writer)
        => FromDataToTextWriter(writer, data);

    /// <inheritdoc />
    public void ParseToWriter(IEnumerable<Dictionary<string, string?>> data, TextWriter writer)
        => FromDictionaryToTextWriter(writer, data);

    /// <inheritdoc />
    public async ValueTask ParseToWriterAsync<T>(IEnumerable<T> data, TextWriter writer, CancellationToken ct = default)
        => await FromDataToTextWriterAsync(writer, data, ct);

    /// <inheritdoc />
    public async ValueTask ParseToWriterAsync(IEnumerable<Dictionary<string, string?>> data, TextWriter writer, CancellationToken ct = default)
        => await FromDictionaryToTextWriterAsync(writer, data, ct);
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Actual Parsers
    // -----------------------------------------------------------------------------------------------------------------
    #region Generic Type Parsing
    private IEnumerable<T> FromTextReader<T>(TextReader reader)
        where T : class, new() {
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

    private async IAsyncEnumerable<T> FromTextReaderAsync<T>(TextReader reader, [EnumeratorCancellation] CancellationToken ct = default)
        where T : class, new() {
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
            ct.ThrowIfCancellationRequested();// After a batch is done, check if the cancellation token was requested
            if (line == null) break;
        }
    }

    private void SetPropertyFromCsvColumn<T>(T? value, string[] headerColumns, string[] values) where T : class, new() {
        if (value is null) return;

        foreach (PropertyInfo prop in GetCsvProperties<T>()) {
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
    private IEnumerable<Dictionary<string, string?>> FromTextReaderToDictionary(TextReader reader) {
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

    private async IAsyncEnumerable<Dictionary<string, string?>> FromTextReaderToDictionaryAsync(TextReader reader, [EnumeratorCancellation] CancellationToken ct = default) {
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
            ct.ThrowIfCancellationRequested();// After a batch is done, check if the cancellation token was requested
            if (line == null) break;
        }
    }
    #endregion
    #region Generic Type Writer
    private void FromDataToTextWriter<T>(TextWriter writer, IEnumerable<T> data) {
        // Write header row
        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders<T>().ToArray();
            for (int i = 0; i < headers.Length; i++) {
                writer.Write(headers[i]);
                if (i < headers.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }

        // Write data rows
        foreach (T? obj in data) {
            string[] values = GetCsvValues(obj).ToArray();
            for (int i = 0; i < values.Length; i++) {
                writer.Write(values[i]);
                if (i < values.Length - 1) writer.Write(config.ColumnSplit);
            }

            writer.Write(Environment.NewLine);
        }
    }

    private async Task FromDataToTextWriterAsync<T>(TextWriter writer, IEnumerable<T> data, CancellationToken ct = default) {
        // Write header row
        if (config.IncludeHeader) {
            string[] headers = GetCsvHeaders<T>().ToArray();
            for (int i = 0; i < headers.Length; i++) {
                await writer.WriteAsync(headers[i]);
                if (i < headers.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
            ct.ThrowIfCancellationRequested();
        }

        // Write data rows
        foreach (T? obj in data) {
            string[] values = GetCsvValues(obj).ToArray();
            for (int i = 0; i < values.Length; i++) {
                await writer.WriteAsync(values[i]);
                if (i < values.Length - 1) await writer.WriteAsync(config.ColumnSplit);
            }

            await writer.WriteAsync(Environment.NewLine);
            ct.ThrowIfCancellationRequested();
        }
    }
    #endregion
    #region Dictionary Writer
    private void FromDictionaryToTextWriter(TextWriter writer, IEnumerable<Dictionary<string, string?>> data) {
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

    private async Task FromDictionaryToTextWriterAsync(TextWriter writer, IEnumerable<Dictionary<string, string?>> data, CancellationToken ct = default) {
        IDictionary<string, string?>[] records = data as IDictionary<string, string?>[] ?? data.ToArray<IDictionary<string, string?>>();
        if (records.Length == 0) return;

        // Write header row
        if (config.IncludeHeader) {
            IDictionary<string, string?> firstDictionary = records.First();
            IEnumerable<string> headers = firstDictionary.Keys;
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, headers));

            ct.ThrowIfCancellationRequested();
        }

        // Write data rows
        foreach (IDictionary<string, string?> dictionary in records) {
            IEnumerable<string> values = dictionary.Values.Select(value => value?.ToString() ?? string.Empty);
            await writer.WriteLineAsync(string.Join(config.ColumnSplit, values));

            ct.ThrowIfCancellationRequested();
        }
    }
    #endregion

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
}
