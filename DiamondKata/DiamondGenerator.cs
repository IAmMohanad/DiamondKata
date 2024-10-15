namespace DiamondKata;

using System.IO;

public class DiamondGenerator
{
    private readonly TextWriter _writer;
    
    public DiamondGenerator(TextWriter writer)
    {
        _writer = writer;
    }

    // Generates a diamond pattern with the given character as the midpoint.
    public void Generate(char c)
    {
        // If c is not alphabetic nothing happens.
        var input = char.ToUpper(c);

        if (input is < 'A' or > 'Z')
        {
            throw new ArgumentOutOfRangeException(nameof(c), c, "Input must be a letter between a-zA-Z.");
        }
        
        var midpoint = input - 'A';

        // Calculate top half (including midpoint)
        for (var currentLine = 0; currentLine <= midpoint; currentLine++)
        {
            _writer.WriteLine(GetLine(currentLine, midpoint));
        }
        // Calculate bottom half (mirror of top half)
        for (var currentLine = midpoint - 1; currentLine >= 0; currentLine--)
        {
            _writer.WriteLine(GetLine(currentLine, midpoint));
        }
    }

    // Generates a single line of the diamond based on the current line number.
    private static string GetLine(int currentLine, int midpoint)
    {
        var currentChar = (char)('A' + currentLine);
        var leadingSpaces = midpoint - currentLine;
        var innerSpaces = currentLine == 0 ? 0 : currentLine * 2 - 1;

        var line = new string(' ', leadingSpaces) + currentChar;

        // Only add inner spaces if not on first line.
        if (innerSpaces > 0)
        {
            line += new string(' ', innerSpaces) + currentChar;
        }

        return line;
    }
}
