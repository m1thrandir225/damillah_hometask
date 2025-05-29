namespace damilah_hometask.domain;

public class Subject(
    string name,
    string description,
    int classesPerWeek,
    string? instructorName = null,
    string? syllabusLink = null,
    List<Literature>? relatedLiterature = null
    ) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public int ClassesPerWeek { get; private set; } = classesPerWeek;
    public string? InstructorName { get; private set; } = instructorName;
    public string? SyllabusLink { get; private set; } = syllabusLink;
    public List<Literature> RelatedLiterature { get; private set; } = relatedLiterature ?? new List<Literature>();


    public override string ToString()
    {
        return $"{this.Name}";
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

        if (this.RelatedLiterature.Any())
        {
            Console.WriteLine("Related Literature:");
            foreach (Literature literature in this.RelatedLiterature)
            {
                Console.WriteLine($"    * {literature.ToString()}");
            }
        }
        else
        {
            Console.WriteLine("No related literature.");
        }

    }
}
