// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv;

namespace Tests.CodeOfChaos.Parsers.Csv.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvReaderTests {
    [Test]
    public async Task ReadFromCsv_ShouldReadCsvCorrectly() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = parser.ToList<TestModelWithoutAttribute>(stringReader);

        // Assert
        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(25);
    }

    [Test]
    public async Task ReadFromCsvAsync_ShouldReadCsvCorrectly() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in parser.ToEnumerableAsync<TestModelWithoutAttribute>(stringReader)) {
            result.Add(data);
        }

        // Assert

        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(25);
    }

    [Test]
    public async Task ReadFromCsv_ShouldHandleMissingColumnsGracefully() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string csv = """
            Name;Age
            John;30
            Jane;
            """;

        var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = parser.ToList<TestModelWithoutAttribute>(stringReader);

        // Assert

        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(0);
    }

    [Test]
    public async Task ReadFromCsv_ShouldRespectConfiguration() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ",";
            cfg.IncludeHeader = true;
        });

        const string csv = """
            Name,Age
            John,30
            Jane,25
            """;

        var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = parser.ToList<TestModelWithoutAttribute>(stringReader);

        // Assert
        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(25);
    }

    [Test]
    public async Task ReadFromCsv_ShouldConvertToPropertyTypes() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string csv = """
            Name;Age
            John;30
            Jane;25
            """;

        var stringReader = new StringReader(csv);

        // Act
        List<TestModelWithoutAttribute> result = parser.ToList<TestModelWithoutAttribute>(stringReader);

        // Assert
        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John")
            .And.IsTypeOf<string>();

        await Assert.That(result[0].Age).IsEqualTo(30);

        await Assert.That(result[1].Name).IsEqualTo("Jane");

        await Assert.That(result[1].Age).IsEqualTo(25);
    }

    [Test]
    public async Task ReadFromCsv_ShouldReadFileCorrectly() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = parser.ToList<TestModelWithoutAttribute>(path);

        // Assert
        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(25);
    }

    [Test]
    public async Task ReadFromCsv_ShouldReadFileCorrectlyAsync() {
        // Arrange
        CsvParser parser = CsvParser.FromConfig(cfg => {
            cfg.ColumnSplit = ";";
            cfg.IncludeHeader = true;
        });

        const string path = "Data/TestData.csv";

        // Act
        List<TestModelWithoutAttribute> result = [];
        await foreach (TestModelWithoutAttribute data in parser.ToEnumerableAsync<TestModelWithoutAttribute>(path)) {
            result.Add(data);
        }

        // Assert
        await Assert.That(result).IsNotEmpty()
            .And.HasCount().EqualTo(2);

        await Assert.That(result[0].Name).IsEqualTo("John");
        await Assert.That(result[0].Age).IsEqualTo(30);
        await Assert.That(result[1].Name).IsEqualTo("Jane");
        await Assert.That(result[1].Age).IsEqualTo(25);
    }
}
