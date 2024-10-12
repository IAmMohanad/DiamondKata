namespace DiamondKata;

public class Program
{
    public static void Main(string[] args)
    {
        // Example usage
        var diamondGenerator = new DiamondGenerator(Console.Out);
        // diamondGenerator.Generate(args[0][0]);
        diamondGenerator.Generate('z');
    }
}