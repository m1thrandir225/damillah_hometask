using damilah_hometask.data;
using damilah_hometask.data.impl;
using damilah_hometask.domain;

namespace damilah_hometask.presentation;

public class App
{
    private List<Subject> subjects = new List<Subject>();

    private void ListSubjects()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("Available Subjects: ");
        Console.WriteLine("-------------------------");
        foreach (var item in this.subjects.Select((value, index) => new { index, value }))
        {
            Console.WriteLine($"{item.index + 1}. {item.value.ToString()}");
        }
    }

    private ISubjectProvider SelectProvider()
    {
        var availableProviders = new string[] { "In-Memory", "JSON" };
        Console.WriteLine("Select from where to insert subjects: ");
        foreach (var item in availableProviders.Select((value, index) => new { index, value }))
        {
            Console.WriteLine($"{item.index + 1}.{item.value}");
        }
        while (true)
        {
            Console.Write("Enter your choice: ");
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int chosenProvider))
            {
                var providerIndex = chosenProvider - 1;

                if (availableProviders[providerIndex] == null)
                {
                    Console.WriteLine("The choicen provider does not exist. Please try again");
                    Console.ReadLine();
                }
                else
                {
                    var provider = availableProviders[providerIndex];
                    switch (provider)
                    {
                        case "JSON":
                            {
                                return new JsonProvider();
                            }
                        case "In-Memory":
                            {
                                return new InMemoryProvider();
                            }
                    }
                }
            }

        }
    }

    public void MainLoop()
    {
        string? userInput;
        while (true)
        {
            Console.Clear();
            ListSubjects();

            Console.Write("Enter the subject ID to view details, or type 'Exit' to quit: ");

            userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("\nInvalid input. Please enter a subject ID or 'Exit'.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                continue;
            }

            if (userInput.Trim().Equals("Exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Written By: Sebastijan Zindl");
                Console.WriteLine("\nExiting application. Goodbye!");
                break;
            }

            if (int.TryParse(userInput, out int subjectId))
            {
                int subjectIndexInList = subjectId - 1;

                if (subjectIndexInList >= 0 && subjectIndexInList < this.subjects.Count)
                {
                    Console.Clear();
                    var subject = this.subjects[subjectIndexInList];
                    subject.PrintDetails();
                    Console.WriteLine("---------------------------------------------");
                    Console.Write("Press Enter to return to the list of subjects...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nInvalid subject ID: {subjectId}. Please choose a number between 1 and {this.subjects.Count}.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"\nInvalid input: '{userInput}'. Please enter a valid subject ID (number) or type 'Exit'.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    public async Task Run()
    {
        Console.WriteLine("School Subjects Information System");
        Console.WriteLine("================================");

        var provider = SelectProvider();

        try
        {
            subjects = await provider.GetSubjectsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while fetching subjects: {ex.Message}");
            return;
        }

        MainLoop();

    }
}