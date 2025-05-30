using damilah_hometask.data.services;

namespace tests;

public class RealConsoleServiceTests
{
    IConsoleService consoleService;

    public RealConsoleServiceTests()
    {
        consoleService = new RealConsoleService();
    }
    [Test]
    public void TestReadLine_WithInput()
    {
        var input = "fake input data";
        Console.SetIn(new StringReader(input));
        var result = consoleService.ReadLine();
        Assert.NotNull(result);
        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void TestReadLine_WithoutInput()
    {
        var input = "fake input data";
        Console.SetIn(new StringReader(""));
        var result = consoleService.ReadLine();
        Assert.Null(result);
    }

    [Test]
    public void TestWriteLine()
    {
        var input = "fake input data";
        var stringWriter = new StringWriter();

        Console.SetOut(stringWriter);

        consoleService.WriteLine(input);

        var output = stringWriter.ToString();

        Assert.NotNull(output);
        Assert.That(output, Is.EqualTo(input+"\n"));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);


    }


    public void TestWrite()
    {
        var input = "fake input data";
        var stringWriter = new StringWriter();

        Console.SetOut(stringWriter);

        consoleService.Write(input);

        var output = stringWriter.ToString();

        Assert.NotNull(output);
        Assert.That(output, Is.EqualTo(input));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }
}