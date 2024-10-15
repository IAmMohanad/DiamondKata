# Diamond Generator

The `DiamondGenerator` class generates a diamond shaped pattern based on a given character input. 

The character is the midpoint and the diamond is symmetrical.

## Usage

### Example

```csharp
var writer = new StringWriter();
var diamondGenerator = new DiamondGenerator(writer);

// Generate a diamond with 'E' as the midpoint
diamondGenerator.Generate('E');

// Output the generated diamond
Console.WriteLine(writer.ToString());
```

or

```csharp
> diamond.exe A
  A

> diamond.exe B
   A
  B B
   A

> diamond.exe C
    A
   B B
  C   C
   B B
```