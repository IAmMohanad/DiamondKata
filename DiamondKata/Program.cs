namespace DiamondKata;

public class Program
{
    public static void Main(string[] args)
    {
        var diamondGenerator = new DiamondGenerator(Console.Out);
        diamondGenerator.Generate(args[0][0]);
        // diamondGenerator.Generate('e');
        Environment.Exit(0);
    }
}