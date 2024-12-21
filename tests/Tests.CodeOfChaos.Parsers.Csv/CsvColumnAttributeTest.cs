// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Parsers.Csv.Attributes;

namespace Tests.CodeOfChaos.Parsers.Csv;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CsvColumnAttributeTests {
    [Test]
    public async Task CsvColumnAttribute_ConstructorShouldSetNameProperty() {
        // Arrange
        const string columnName = "TestColumn";

        // Act
        var attribute = new CsvColumnAttribute(columnName);

        // Assert
        await Assert.That(attribute.Name).IsEqualTo(columnName);
    }

    [Test]
    public async Task CsvColumnAttribute_NamePropertyShouldBeCaseInsensitive() {
        // Arrange
        string columnName = "TestColumn".ToLowerInvariant();
        var attributeWithUppercase = new CsvColumnAttribute("TESTCOLUMN");
        var attributeWithLowercase = new CsvColumnAttribute("testcolumn");

        // Act & Assert
        await Assert.That(attributeWithUppercase.NameLowerInvariant).IsEqualTo(columnName);
        await Assert.That(attributeWithLowercase.NameLowerInvariant).IsEqualTo(columnName);
    }

    [Test]
    public void CsvColumnAttribute_ConstructorShouldThrowNullReferenceException_WhenNameIsNull() {
        // Act & Assert
        #pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<NullReferenceException>(() => {
            _ = new CsvColumnAttribute(null);
        });
        #pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    [Test]
    public void CsvColumnAttribute_ConstructorShouldThrowArgumentException_WhenNameIsEmpty() {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => {
            _ = new CsvColumnAttribute(string.Empty);
        });
    }
}
