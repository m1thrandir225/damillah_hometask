namespace damilah_hometask.data.services;

public class RealConsoleService : IConsoleService
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }

    public void Write(string s)
    {
        Console.Write(s);
    }

    public void Clear()
    {
        Console.Clear();
    }
}