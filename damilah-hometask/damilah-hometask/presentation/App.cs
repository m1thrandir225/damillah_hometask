using damilah_hometask.data;
using damilah_hometask.data.providers;
using damilah_hometask.data.providers.impl;
using damilah_hometask.data.services;
using damilah_hometask.domain;

namespace damilah_hometask.presentation;

public class App
{
    private readonly IConsoleService _console;
    private readonly Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)> _providerFactories;

    public App(IConsoleService console,
        Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)> providerFactories)
    {
        _console = console;
        _providerFactories = providerFactories;
    }

    public App(IConsoleService console) : this(console, GetDefaultProviderFactories())
    {
    }

    private static Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)>
        GetDefaultProviderFactories()
    {
        return new Dictionary<string, (string DisplayName, Func<ISubjectProvider> Factory)>
        {
            {
                "1", ("In-Memory", () => new InMemoryProvider())
            },
            {
                "2", ("JSON", () => new JsonProvider())
            }
        };
    }

    private void ListSubjects(List<Subject> subjects)
    {
        _console.WriteLine("-------------------------");
        _console.WriteLine("Available Subjects: ");
        _console.WriteLine("-------------------------");
        foreach (var item in subjects.Select((value, index) => new { index, value }))
        {
            _console.WriteLine($"{item.index + 1}. {item.value.ToString()}");
        }
    }

    private ISubjectProvider SelectProvider()
    {
        _console.WriteLine("Select from where to insert subjects: ");
        foreach (var entry in _providerFactories)
        {
            _console.WriteLine($"{entry.Key}. {entry.Value.DisplayName}");
        }
        _console.WriteLine("0. Exit");
        while (true)
        {
            _console.Write("Enter your choice: ");
            string? choice = _console.ReadLine();

            if (choice == "0")
            {
                _console.WriteLine("Exiting data source selection.");
                return null;
            }

            if (_providerFactories.TryGetValue(choice ?? string.Empty, out var provider))
            {
                _console.WriteLine($"Using {provider.DisplayName}");
                return provider.Factory();
            }
            _console.WriteLine("Invalid Choice. Please select a valid option");
        }
    }

    public void MainLoop(List<Subject> subjects)
    {
        string? userInput;
        while (true)
        {
            _console.Clear();

            ListSubjects(subjects);

            _console.Write("Enter the subject ID to view details, or type 'Exit' to quit: ");

            userInput = _console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                _console.WriteLine("\nInvalid input. Please enter a subject ID or 'Exit'.");
                _console.WriteLine("Press Enter to continue...");
                _console.ReadLine();
                continue;
            }

            if (userInput.Trim().Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                _console.WriteLine("Written By: Sebastijan Zindl");
                _console.WriteLine("\nExiting application. Goodbye!");
                break;
            }

            if (int.TryParse(userInput, out int subjectId))
            {
                int subjectIndexInList = subjectId - 1;

                if (subjectIndexInList >= 0 && subjectIndexInList < subjects.Count)
                {
                    Console.Clear();
                    var subject = subjects[subjectIndexInList];
                    subject.PrintDetails();
                    _console.WriteLine("---------------------------------------------");
                    _console.Write("Press Enter to return to the list of subjects...");
                    _console.ReadLine();
                }
                else
                {
                    _console.WriteLine($"\nInvalid subject ID: {subjectId}. Please choose a number between 1 and {subjects.Count}.");
                    _console.WriteLine("Press Enter to continue...");
                    _console.ReadLine();
                }
            }
            else
            {
                _console.WriteLine($"\nInvalid input: '{userInput}'. Please enter a valid subject ID (number) or type 'Exit'.");
                _console.WriteLine("Press Enter to continue...");
                _console.ReadLine();
            }
        }
    }

    public async Task Run()
    {
        _console.WriteLine("School Subjects Information System");
        _console.WriteLine("================================");
        ISubjectProvider? subjectProvider = SelectProvider();

        if (subjectProvider == null)
        {
            _console.WriteLine("No subject provider selected. Exiting application.");
            return;
        }

        List<Subject> subjects;
        try
        {
            subjects = await subjectProvider.GetSubjectsAsync();
        }
        catch (Exception ex)
        {
            _console.WriteLine($"An error occured while fetching subjects: {ex.Message}");
            return;
        }

        if (!subjects.Any())
        {
            _console.WriteLine("No subjects available from the selected course.");
            _console.WriteLine("Exiting application. Goodbye!");
            return;
        }

        MainLoop(subjects);

    }
}