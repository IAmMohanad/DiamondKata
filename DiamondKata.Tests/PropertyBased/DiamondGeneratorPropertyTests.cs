using CsCheck;

namespace DiamondKata.Tests.PropertyBased
{
    public class DiamondGeneratorPropertyTests
    {
        [Fact]
        public void Given_InvalidInput_When_GenerateIsCalled_Then_ThrowsArgumentOutOfRangeException()
        {
            // Generator for invalid characters (outside the alphabetic range)
            // Arrange
            var invalidCharGen = Gen.OneOf(
                Gen.Char[char.MinValue, (char)('A' - 1)],
                Gen.Char[(char)('Z' + 1), char.MaxValue],
                Gen.Char['0', '9'],
                Gen.Char['!', '/'],
                Gen.Char[':', '@'],
                Gen.Char['[', '`'],
                Gen.Char['{', '~']
            );
            invalidCharGen.Sample(c =>
            {
                var writer = new StringWriter();
                var generator = new DiamondGenerator(writer);

                // Act & Assert
                var exception = Assert.Throws<ArgumentOutOfRangeException>(() => generator.Generate(c));

                Assert.Equal(nameof(c), exception.ParamName);
                Assert.Equal(c, exception.ActualValue);
                Assert.Contains("Input must be a letter between a-zA-Z.", exception.Message);
            });
        }
        
        [Fact]
        public void Given_ValidInput_When_GenerateIsCalled_Then_FirstAndLastLinesAreCorrect()
        {
            // Arrange
            var upperCaseLetterGen = Gen.Char['A', 'Z'];

            upperCaseLetterGen.Sample(c =>
            {
                var midpoint = c - 'A';
                var expectedLine = new string(' ', midpoint) + 'A';

                var writer = new StringWriter();
                var generator = new DiamondGenerator(writer);

                // Act
                generator.Generate(c);
                var outputLines = GetOutputLines(writer.ToString());

                // Assert: First and last lines should be the same and contain 'A'
                Assert.Equal(expectedLine, outputLines[0]);
                Assert.Equal(expectedLine, outputLines[^1]);
            });
        }

        [Fact]
        public void Given_ValidInput_When_GenerateIsCalled_Then_DiamondHasCorrectNumberOfLines()
        {
            // Arrange
            var upperCaseLetterGen = Gen.Char['A', 'Z'];

            upperCaseLetterGen.Sample(c =>
            {
                var midpoint = c - 'A' + 1;
                var expectedLineCount = 2 * midpoint - 1;

                var writer = new StringWriter();
                var generator = new DiamondGenerator(writer);

                // Act
                generator.Generate(c);
                var outputLines = GetOutputLines(writer.ToString());

                // Assert
                Assert.Equal(expectedLineCount, outputLines.Count);
            });
        }
        
        [Fact]
        public void Given_ValidInput_When_GenerateIsCalled_Then_MidpointLineContainsCorrectCharacter()
        {
            var upperCaseLetterGen = Gen.Char['A', 'Z'];
            upperCaseLetterGen.Sample(c =>
            {
                // Arrange
                var sw = new StringWriter();
                var generator = new DiamondGenerator(sw);

                // Act
                generator.Generate(c);

                // Assert
                var lines = sw.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var midpoint = char.ToUpper(c) - 'A';
                var midpointLine = lines[midpoint];
                Assert.Contains(char.ToUpper(c).ToString(), midpointLine);
            });
        }

        // Helper method to split the generated output into lines.
        private List<string> GetOutputLines(string output)
        {
            var outputLines = new List<string>();
            using var reader = new StringReader(output);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                outputLines.Add(line);
            }

            return outputLines;
        }
    }
}
