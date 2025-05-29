using damilah_hometask.data;
using damilah_hometask.domain;

namespace damilah_hometask.presentation;

public class App
{
    private List<Subject> subjects = new List<Subject>();

    public void Import(string method)
    {
        switch (method)
        {
            case "REST":
            {
                break;
            }
            case "DB":
            {
                break;
            }
            default:
                throw new Exception("Invalid method.");
        }
    }

    private void SetupInitialData()
    {
        var mathSubject = new Subject(
            "Math 22",
            "Multivariable Calculus and Linear Algebra",
            4
        );
        var mathLiterature1 = new Literature(
            "Multivariable Calculus",
            "https://open.umn.edu/opentextbooks/textbooks/780",
            "James Stewart",
            new DateTime(1989, 6, 15)
        );
        var mathLiterature2 = new Literature(
            "",
            "",
            "",
            new DateTime());

        mathSubject.AddLiterature(mathLiterature1);
        mathSubject.AddLiterature(mathLiterature2);

        var csSubject = new Subject(
            "CS50",
            "Introduction to Computer Science",
            4
            );

        var aiSubject = new Subject("" +
                                    "CS1060",
            "Software Engineering & Generative AI",
            3
            );

        this.subjects.Add(mathSubject);
        this.subjects.Add(aiSubject);
        this.subjects.Add(csSubject);
    }

    private bool PromptImport()
    {
        while (true)
        {
            Console.WriteLine("Would you like to import subjects from an external source? ");
            Console.WriteLine("Y/N");
            var insertFromExternal = Console.ReadLine();
            if (insertFromExternal == null)
            {
                Console.WriteLine("No input given. Assuming 'N'");
                return false;
            }

            string userInput = insertFromExternal.Trim().ToUpper();

            if (userInput == "Y") return true;
            if (userInput == "N") return false;

            Console.WriteLine("Invalid input. Please enter either 'Y' or 'N'.");

        }
    }

    private void PromptSubjectDetails()
    {
        Console.Write("Enter the subject ID to view it's details: ");
        var subjectIndex = Convert.ToInt32(Console.ReadLine());
        var subject = this.subjects[subjectIndex-1];
        subject.PrintDetails();
        Console.Write("To list all subjects <Back>: ");
    }
    private void ListSubjects()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("Available Subjects: ");
        Console.WriteLine("-------------------------");
        foreach (var  item in this.subjects.Select((value, index) => new {  index, value }))
        {
            Console.WriteLine($"{item.index + 1}. {item.value.ToString()}");
        }
    }

    public void Run()
    {
        SetupInitialData();
        Console.WriteLine("School Subjects Information System");
        Console.WriteLine("================================");

        var shouldExternallyImport = this.PromptImport();
        if (shouldExternallyImport)
        {
            Console.WriteLine("\nFrom where would you like to import subjects? (Enter the number)");

            var currentStrategies = new string[] { "REST", "DB" };

            for (var i = 0; i < currentStrategies.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {currentStrategies[i]}");
            }
            Console.Write("Enter your choice: ");
            string importChoiceInput = Console.ReadLine();

            if (int.TryParse(importChoiceInput, out int chosenImportMethodIndex) &&
                chosenImportMethodIndex > 0 && chosenImportMethodIndex <= currentStrategies.Length)
            {
                try
                {
                    this.Import(currentStrategies[chosenImportMethodIndex - 1]);
                    Console.WriteLine("Import process finished.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during import: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid import choice. Skipping external import.");
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        string userInput;
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
}