namespace damilah_hometask.domain;

public class Subject(string name, string description, int classesPerWeek) : BaseEntity
{
    private string Name { get; set; } = name;
    private string Description { get; set; } = description;
    private int ClassesPerWeek { get; set; } = classesPerWeek;
    private List<Literature>  RelatedLiterature { get; set; } = new List<Literature>();

    public List<Literature>  AddLiterature(Literature newLiterature)
    {
        this.RelatedLiterature.Add(newLiterature);

        return this.RelatedLiterature;
    }

    public List<Literature> RemoveLiterature(string name)
    {
        this.RelatedLiterature.RemoveAll(l => l.Name == name);
        return this.RelatedLiterature;
    }

    public override string ToString()
    {
        return $"{this.Name} - {this.Description} - Classes Per Week: {this.ClassesPerWeek}";
    }

    public void PrintDetails()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Subject: {this.Name}");
        Console.WriteLine("-------------------------");
        Console.WriteLine($"Description: {this.Description}");
        Console.WriteLine($"Classes Per Week: {this.ClassesPerWeek}");
        Console.WriteLine("Related Literature:");
        foreach (Literature literature in this.RelatedLiterature)
        {
            Console.WriteLine($"        * {literature.ToString()}");
        }
    }
}
