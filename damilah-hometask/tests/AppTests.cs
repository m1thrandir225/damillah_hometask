using damilah_hometask.data.providers;
using damilah_hometask.domain;
using damilah_hometask.presentation;
using tests.mocks;

using NUnit.Framework;
namespace tests;

public class AppTests
{
    private readonly MockConsoleService _mockConsole;
    private readonly MockSubjectProvider _mockInMemoryProvider;
    private readonly MockSubjectProvider _mockJsonProvider;
    private readonly Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)> _testProviderFactories;
    private readonly App _app;

    public AppTests()
    {
        _mockConsole= new MockConsoleService();
        _mockInMemoryProvider = new MockSubjectProvider();
        _mockJsonProvider = new MockSubjectProvider();
        _testProviderFactories = new Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)>
        {
            { "1", ("Test In-Memory", () => _mockInMemoryProvider) },
            { "2", ("Test JSON", () => _mockJsonProvider) }
        };

        _app = new App(_mockConsole, _testProviderFactories);
    }

    [Test]
    public async Task RunAsync_WelcomeUserAndShowProviderSection()
    {
        _mockInMemoryProvider.ResetSubjectsCallCount();
        _mockConsole.AddExpectedInput("0");

        await _app.Run();

        Assert.That(_mockConsole.OutputHistory, Does.Contain("School Subjects Information System"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Select from where to insert subjects: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("1. Test In-Memory"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("2. Test JSON"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("0. Exit"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Enter your choice: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Exiting data source selection."));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("No subject provider selected. Exiting application."));
        _mockConsole.ClearHistory();
    }

    [Test]
    public async Task RunAsync_SelectsProvider_FetchAndDisplaySubjects_AndExists()
    {
        _mockInMemoryProvider.ResetSubjectsCallCount();
        _mockConsole.AddExpectedInput("1");
        _mockInMemoryProvider.SubjectsToReturn =
        [
            new Subject("Math Test", "Desc", 3, "Prof A", "SyllabusA", new List<Literature>()),
            new Subject("Art Test", "Desc", 2, "Prof B", "SyllabusB", new List<Literature>())
        ];
        _mockConsole.AddExpectedInput("Exit");

        await _app.Run();

        Assert.That(_mockConsole.OutputHistory, Does.Contain("Using Test In-Memory"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("CONSOLE_CLEARED"));
        Assert.AreEqual(_mockInMemoryProvider.GetSubjectsCallCount, 1);
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Available Subjects: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("1. Math Test"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("2. Art Test"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Enter the subject ID to view details, or type 'Exit' to quit: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("\nExiting application. Goodbye!"));
        _mockConsole.ClearHistory();
    }

    [Test]
    public async Task RunAsync_HandlesProviderReturningNoSubjects()
    {
        _mockInMemoryProvider.ResetSubjectsCallCount();
        _mockConsole.AddExpectedInput("1");
        _mockInMemoryProvider.SubjectsToReturn = new List<Subject>();

        await _app.Run();

        Assert.That(_mockConsole.OutputHistory, Does.Contain("Using Test In-Memory"));
        Assert.AreEqual(_mockInMemoryProvider.GetSubjectsCallCount, 1);
        Assert.That(_mockConsole.OutputHistory, Does.Contain("No subjects available from the selected course."));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Exiting application. Goodbye!"));
        _mockConsole.ClearHistory();
    }

    [Test]
    public async Task RunAsync_HandlesProviderThrowingException()
    {
        _mockJsonProvider.ResetSubjectsCallCount();
        _mockConsole.AddExpectedInput("2");
        _mockJsonProvider.ExceptionToThrow = new System.IO.FileNotFoundException("Test Error");

        await _app.Run();

        Assert.That(_mockConsole.OutputHistory, Does.Contain("Using Test JSON"));
        Assert.AreEqual(_mockJsonProvider.GetSubjectsCallCount, 1);
        Assert.That(_mockConsole.OutputHistory, Does.Contain("An error occured while fetching subjects: Test Error"));
        _mockConsole.ClearHistory();
    }

    [Test]
    public async Task RunAsync_PrintSubjectDetails_ThenExit()
    {
        _mockInMemoryProvider.ResetSubjectsCallCount();
        _mockConsole.AddExpectedInputs("1", "1", "3", "Exit");
        var mathSubject = new Subject("Math", "Desc", 3,"Prof A", "SyllabusA", new List<Literature>());

        _mockInMemoryProvider.SubjectsToReturn = new List<Subject> { mathSubject };

        await _app.Run();

        Assert.That(_mockConsole.OutputHistory, Does.Contain("Using Test In-Memory"));
        Assert.AreEqual(_mockInMemoryProvider.GetSubjectsCallCount, 1);
        Assert.That(_mockConsole.OutputHistory, Does.Contain($"1. {mathSubject.Name}"));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Enter the subject ID to view details, or type 'Exit' to quit: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Press Enter to return to the list of subjects..."));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("Available Subjects: "));
        Assert.That(_mockConsole.OutputHistory, Does.Contain("\nExiting application. Goodbye!"));
        _mockConsole.ClearHistory();
    }
}