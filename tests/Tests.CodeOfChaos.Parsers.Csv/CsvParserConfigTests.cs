// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv;

namespace Tests.CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvParserConfigTests {
    [Test]
    public async Task CsvParserConfig_DefaultConstructor_ShouldSetDefaultValues() {
        // Arrange & Act
        var config = new CsvParserConfig();

        // Assert
        await Assert.That(config.ColumnSplit).IsEqualTo(",");
        await Assert.That(config.UseLowerCaseHeaders).IsFalse();
        await Assert.That(config.IncludeHeader).IsTrue();
    }
}
