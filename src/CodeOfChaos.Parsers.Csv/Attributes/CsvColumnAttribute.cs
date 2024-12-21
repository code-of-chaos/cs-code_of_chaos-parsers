// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.Parsers.Csv.Attributes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class CsvColumnAttribute : Attribute {

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public CsvColumnAttribute(string name) {
        if (name == string.Empty) throw new ArgumentException("Name cannot be empty", nameof(name));

        Name = name;
        NameLowerInvariant = name.ToLowerInvariant();
    }
    public string Name { get; }
    public string NameLowerInvariant { get; }
}
