namespace DiamondKata.Tests;

public class DiamondGeneratorTests : IDisposable
{
    private readonly TextWriter _writer;
    private readonly DiamondGenerator _generator;
    public DiamondGeneratorTests()
    {
        _writer = new StringWriter();
        _generator = new DiamondGenerator(_writer);
    }
    
    public void Dispose()
    {
        _writer.Dispose();
    }
    
    // Happy Cases
    [Fact]
    public void Given_A_When_Generate_Then_WritesSingleLineDiamond()
    {
        // Arrange
        var expectedOutput = "A" + Environment.NewLine;

        // Act
        _generator.Generate('A');

        // Assert
        Assert.Equal(expectedOutput, _writer.ToString());
    }

    [Fact]
    public void Given_B_When_Generate_Then_WritesCorrectDiamond()
    {
        // Arrange
        var expectedOutput =
            " A" + Environment.NewLine +
            "B B" + Environment.NewLine +
            " A" + Environment.NewLine;

        // Act
        _generator.Generate('B');

        // Assert
        Assert.Equal(expectedOutput, _writer.ToString());
    }

    [Fact]
    public void Given_C_When_Generate_Then_WritesCorrectDiamond()
    {
        // Arrange
        var expectedOutput =
            "  A" + Environment.NewLine +
            " B B" + Environment.NewLine +
            "C   C" + Environment.NewLine +
            " B B" + Environment.NewLine +
            "  A" + Environment.NewLine;

        // Act
        _generator.Generate('C');

        // Assert
        Assert.Equal(expectedOutput, _writer.ToString());
    }

    [Fact]
    public void Given_D_When_Generate_Then_WritesCorrectDiamond()
    {
        // Arrange
        var expectedOutput =
            "   A" + Environment.NewLine +
            "  B B" + Environment.NewLine +
            " C   C" + Environment.NewLine +
            "D     D" + Environment.NewLine +
            " C   C" + Environment.NewLine +
            "  B B" + Environment.NewLine +
            "   A" + Environment.NewLine;

        // Act
        _generator.Generate('D');

        // Assert
        Assert.Equal(expectedOutput, _writer.ToString());
    }
    
    [Fact]
    public void Given_LowerCaseLetter_When_Generate_Then_WritesCorrectDiamond()
    {
        // Arrange
        var expectedOutput =
            "  A" + Environment.NewLine +
            " B B" + Environment.NewLine +
            "C   C" + Environment.NewLine +
            " B B" + Environment.NewLine +
            "  A" + Environment.NewLine;

        // Act
        _generator.Generate('c');

        // Assert
        Assert.Equal(expectedOutput, _writer.ToString());
    }

    [Fact]
    public void Given_ZInput_When_Generate_Then_WritesCorrectDiamond()
    {
        // Act
        _generator.Generate('Z');
        var output = _writer.ToString();
        var outputLines = output!.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        // Assert
        // Diamond for Z is too long to manually write...
        // number of lines
        Assert.Equal(51, outputLines.Length);
        // first line
        Assert.Equal(new string(' ', 25) + 'A', outputLines[0]);
        // last line
        Assert.Equal(new string(' ', 25) + 'A', outputLines[50]);
    }

    // Unhappy Cases
    [Theory]
    [InlineData('1')]
    [InlineData('#')]
    [InlineData(' ')]
    [InlineData('[')]
    [InlineData('`')]
    public void Given_InvalidCharacter_When_Generate_Then_ThrowsArgumentOutOfRangeException(char invalidChar)
    {
        // Arrange
        // Act & Assert
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _generator.Generate(invalidChar));
        Assert.Equal(invalidChar, exception.ActualValue);
        Assert.Contains("Input must be a letter between a-zA-Z.", exception.Message);
    }
}