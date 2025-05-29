using damilah_hometask.data.services;

namespace tests.mocks;

public class MockConsoleService : IConsoleService
{
    private readonly Queue<string?> _inputs = new Queue<string?>();
    public List<string> OutputHistory { get; } = new List<string>();

    public void AddExpectedInput(string? input)
    {
        _inputs.Enqueue(input);
    }

    public void AddExpectedInputs(params string?[] inputs)
    {
        foreach (var input in inputs)
        {
            _inputs.Enqueue(input);
        }
    }


    public string? ReadLine()
    {
        if (_inputs.Any())
        {
            string? input = _inputs.Dequeue();
            return input;
        }

        return null;
    }

    public void WriteLine(string s)
    {
        OutputHistory.Add(s);
    }

    public void Write(string s)
    {
        OutputHistory.Add(s);
    }

    public void Clear()
    {
       OutputHistory.Add("CONSOLE_CLEARED");
    }

    public void ClearHistory()
    {
        OutputHistory.Clear();
    }
}