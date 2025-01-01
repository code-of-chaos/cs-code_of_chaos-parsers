// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv;

namespace Tests.CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DictionaryCsvParserTest {
    private static CsvParser CreateParser() {
        return CsvParser.FromConfig(config => config.ColumnSplit = ";");
    }

    [Test]
    public async Task FromCsvFile_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        const string filePath = "FromCsvFile_ShouldReturnCorrectData.csv";
        await File.WriteAllTextAsync(filePath, """
        id;name
        1;John
        2;Jane
        """);

        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };


        // Act
        List<Dictionary<string, string?>> result = parser.ToDictionaryList(filePath);

        // Assert
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    public async Task FromCsvString_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        const string data = """
            id;name
            1;John
            2;Jane
            """;

        var stringReader = new StringReader(data);
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        List<Dictionary<string, string?>> result = parser.ToDictionaryList(stringReader);

        // Assert
        await Assert.That(result).IsEquivalentTo(expected);
    }

    [Test]
    public async Task FromCsvFileAsync_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "FromCsvFileAsync_ShouldReturnCorrectData.csv";
        await File.WriteAllTextAsync(filePath, """
        id;name
        1;John
        2;Jane
        """);

        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        IAsyncEnumerable<Dictionary<string, string?>> result = parser.ToDictionaryEnumerableAsync(filePath);

        // Assert
        var actual = new List<Dictionary<string, string?>>();
        await foreach (Dictionary<string, string?> dict in result) {
            actual.Add(dict);
        }

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task FromCsvStringAsync_ShouldReturnCorrectData() {
        // Arrange
        CsvParser parser = CreateParser();
        string data = """
            id;name
            1;John
            2;Jane
            """;

        var reader = new StringReader(data);
        var expected = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        IAsyncEnumerable<Dictionary<string, string?>> result = parser.ToDictionaryEnumerableAsync(reader);

        // Assert
        var actual = new List<Dictionary<string, string?>>();
        await foreach (Dictionary<string, string?> dict in result) {
            actual.Add(dict);
        }

        await Assert.That(actual).IsEquivalentTo(expected);
    }

    [Test]
    public async Task WriteToString_ShouldReturnCorrectCsv() {
        // Arrange
        CsvParser parser = CreateParser();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = parser.ParseToString(data);

        // Assert
        string expected = """
            id;name
            1;John
            2;Jane
            """;

        await Assert.That(result.Trim()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task WriteToStringAsync_ShouldReturnCorrectCsv() {
        // Arrange
        CsvParser parser = CreateParser();
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        // Act
        string result = await parser.ParseToStringAsync(data);

        // Assert
        string expected = """
            id;name
            1;John
            2;Jane
            """;

        await Assert.That(result.Trim()).IsEquivalentTo(expected);
    }

    [Test]
    public async Task WriteToFile_ShouldWriteCorrectCsvToFile() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "WriteToFile_ShouldWriteCorrectCsvToFile.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            parser.ParseToFile(filePath, data);

            // Assert
            string result = await File.ReadAllTextAsync(filePath);
            string expected = """
                id;name
                1;John
                2;Jane
                """;

            await Assert.That(result.Trim()).IsEquivalentTo(expected);
        }
        finally {
            // Clean up
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }

    [Test]
    public async Task WriteToFileAsync_ShouldWriteCorrectCsvToFileAsync() {
        // Arrange
        CsvParser parser = CreateParser();
        string filePath = "WriteToFileAsync_ShouldWriteCorrectCsvToFileAsync.csv";
        var data = new List<Dictionary<string, string?>> {
            new() { { "id", "1" }, { "name", "John" } },
            new() { { "id", "2" }, { "name", "Jane" } }
        };

        try {
            // Act
            await parser.ParseToFileAsync(filePath, data);

            // Assert
            string result = await File.ReadAllTextAsync(filePath);
            string expected = """
                id;name
                1;John
                2;Jane
                """;

            await Assert.That(result.Trim()).IsEquivalentTo(expected);
        }
        finally {
            // Clean up
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}
