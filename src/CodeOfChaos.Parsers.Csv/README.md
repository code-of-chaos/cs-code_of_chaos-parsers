# CodeOfChaos.Parsers.Csv

`CodeOfChaos.Parsers.Csv` is a lightweight library for parsing CSV files in .NET with an API inspired by conventional
XML parsing. It supports handling CSV data in multiple formats such as objects, dictionaries, and enumerable
collections, with both synchronous and asynchronous processing capabilities.

---

## Features

### CSV Parsing

Transform CSV content into multiple output formats:

- **Object Mapping** – Convert CSV rows directly into objects.
- **Dynamic Parsing** – Parse CSV data into dictionaries for flexible usage.
- **Enumerable Support** – Process rows in batches or streams to handle large files efficiently.
- **Header Mapping** – Automatically map CSV headers to object properties using configurable behavior.

---

### Writing CSV

Create CSV files or strings from .NET objects:

- Write objects directly to CSV, with easy-to-use serialization.
- Support for both synchronous and asynchronous writing.

---

### Configuration Options

Customize CSV parsing and writing:

- **Delimiters** – Support different separators like `,`, `;`, or custom characters.
- **Header Handling** – Include or exclude headers.
- **Error Logging** – Log and handle errors gracefully.
- **Batch Processing** – Manage large files by processing data in chunks.

---

### Attribute-Based Column Mapping

Map CSV columns explicitly to object properties using attributes:

- `[CsvColumn("ColumnName")]` maps specific CSV headers to class properties.
- Useful for handling CSVs with non-standard or verbose field names.

---

## Installation

This library targets `.NET 9.0` and requires C# 13.0. Ensure your project meets these requirements before using.

Add the dependency to your project via NuGet:

```bash
dotnet add package CodeOfChaos.Parsers.Csv --version 2.0.0-preview.0
```

---

## Usage

Here’s how you can leverage the `CodeOfChaos.Parsers.Csv` library:

### Configuring the Parser

You can create and configure a `CsvParser` instance using the `CsvParser.FromConfig` method. For example:

```csharp
var parser = CsvParser.FromConfig(cfg => {
    cfg.ColumnSplit = ";";        // Use `;` as the delimiter
    cfg.IncludeHeader = true;     // Parse CSV with a header row
    cfg.BatchSize = 100;          // Process data in smaller batches
});
```

---

### Parsing CSV to Objects

```csharp
using CodeOfChaos.Parsers.Csv;

class Person {
    public string Name { get; set; } = default!;
    public int Age { get; set; }
}

var parser = CsvParser.FromConfig(cfg => cfg.ColumnSplit = ",");
var people = parser.ToList<Person>("path/to/file.csv");

foreach (var person in people) {
    Console.WriteLine($"{person.Name} is {person.Age} years old.");
}
```

---

### Parsing CSV to Dictionary

If you need more dynamic handling and don't want to use object mapping, you can parse CSV data into a dictionary:

```csharp
using CodeOfChaos.Parsers.Csv;

var parser = CsvParser.FromConfig(cfg => cfg.ColumnSplit = ",");
IEnumerable<Dictionary<string, string?>> rows = parser.ToDictionaryEnumerable("path/to/file.csv");

foreach (var row in rows) {
    Console.WriteLine($"Name: {row["Name"]}, Age: {row["Age"]}");
}
```

---

### Handling Large Files with Enumeration

For large files, you can use `ToEnumerable` for lazy loading and processing of rows:

```csharp
var parser = CsvParser.FromConfig(cfg => cfg.ColumnSplit = ",");
await foreach (var person in parser.ToEnumerableAsync<Person>("path/to/large-file.csv")) {
    Console.WriteLine($"{person.Name} is {person.Age} years old.");
}
```

---

### Writing Objects to CSV

Write a collection of objects back to a CSV file:

```csharp
using CodeOfChaos.Parsers.Csv;

var people = new[] {
    new Person { Name = "John", Age = 30 },
    new Person { Name = "Jane", Age = 25 }
};

var parser = CsvParser.FromConfig(cfg => cfg.ColumnSplit = ",");
parser.ParseToFile("output.csv", people);
```

---

### Using Attributes for Column Mapping

Use the `[CsvColumn]` attribute to map CSV columns to object properties:

```csharp
using CodeOfChaos.Parsers.Csv;

class User {
    [CsvColumn("username")]
    public string Name { get; set; } = default!;

    [CsvColumn("age")]
    public int Age { get; set; }
}

var parser = CsvParser.FromConfig(cfg => cfg.ColumnSplit = ",");
var users = parser.ToList<User>("path/to/file.csv");

// Input CSV example:
// username,age
// John,30
// Jane,25

foreach (var user in users) {
    Console.WriteLine($"{user.Name} is {user.Age} years old.");
}
```

---

## Contributing

Feel free to fork and contribute to the project by submitting pull requests. When contributing, ensure your changes
align with the project’s coding standards.