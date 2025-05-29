namespace damilah_hometask.domain;

public class Subject(
    string name,
    string description,
    int classesPerWeek,
    string? instructorName = null,
    string? syllabusLink = null,
    List<Literature>? literature = null

    ) : BaseEntity
{
    private string Name { get; set; } = name;
    private string Description { get; set; } = description;
    private int ClassesPerWeek { get; set; } = classesPerWeek;
    private string? InstructorName { get; set; } = instructorName;
    private string? SyllabusLink { get; set; } = syllabusLink;
    private List<Literature> RelatedLiterature { get; set; } = literature ?? new List<Literature>();


    public override string ToString()
    {
        return $"{this.Name} - {this.Description} - Classes Per Week: {this.ClassesPerWeek}";
    }

    public void PrintDetails()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Details About: {this.Name}");
        Console.WriteLine("-------------------------");

        if (!string.IsNullOrWhiteSpace(this.InstructorName))
        {

            Console.WriteLine($"Instructor Name: {this.InstructorName}");
        }

        if (!string.IsNullOrWhiteSpace(this.SyllabusLink))
        {
            Console.WriteLine($"Syllabus Link: {this.SyllabusLink}");
        }

        Console.WriteLine($"Description: {this.Description}");
        Console.WriteLine($"Classes Per Week: {this.ClassesPerWeek}");

        Console.WriteLine("Related Literature:");
        foreach (Literature literature in this.RelatedLiterature)
        {
            Console.WriteLine($"        * {literature.ToString()}");
        }
    }
}
