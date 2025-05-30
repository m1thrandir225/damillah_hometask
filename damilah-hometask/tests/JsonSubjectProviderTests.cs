using damilah_hometask.data.providers.impl;
using NUnit.Framework;

namespace tests;

public class JsonSubjectProviderTests
{
    private const string TestFilesDir = "TestJsonData";

    public JsonSubjectProviderTests()
    {
        if (!Directory.Exists(TestFilesDir))
        {
            Directory.CreateDirectory(TestFilesDir);
        }

        File.WriteAllTextAsync(Path.Combine(TestFilesDir, "valid_subjects_data.json"), @"[  {
    ""Name"": ""Subject"",
    ""Description"": ""Subject Description"",
    ""ClassesPerWeek"": 2,
    ""InstructorName"": ""James Brown"",
    ""SyllabusLink"": ""example.com""
  }]");

        File.WriteAllTextAsync(Path.Combine(TestFilesDir, "malformed_subjects_data.json"), @"
  {
    ""Name"": ""Example""
    ""Description"": ""Example of malformed subjects."",
  },
        ");
        File.WriteAllTextAsync(Path.Combine(TestFilesDir, "empty_subjects_data.json"), @"[]");
    }

    [Test]
    public async Task GetSubjectsAsync_WithValidJsonFile_ReturnsSubjects()
    {

        var filePath = Path.Combine(TestFilesDir, "valid_subjects_data.json");
        var provider = new JsonProvider(filePath);

        var subjects = await provider.GetSubjectsAsync();

        Assert.NotNull(subjects);
        Assert.True(subjects.Count == 1);

        var subject = subjects.First();
        Assert.That(subject.Name, Is.EqualTo("Subject"));
        Assert.That(subject.Description, Is.EqualTo("Subject Description"));
        Assert.That(subject.ClassesPerWeek, Is.EqualTo(2));
        Assert.That(subject.InstructorName, Is.EqualTo("James Brown"));
        Assert.That(subject.SyllabusLink, Is.EqualTo("example.com"));
        Assert.That(subject.RelatedLiterature.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetSubjectsAsync_WithEmptyJsonFile_ReturnsEmptySubjects()
    {
        var filePath = Path.Combine(TestFilesDir, "empty_subjects_data.json");
        var provider = new JsonProvider(filePath);
        var subjects = await provider.GetSubjectsAsync();
        Assert.NotNull(subjects);
        Assert.IsEmpty(subjects);
    }

    [Test]
    public async Task GetSubjectsAsync_WithMalformedJsonFile_ReturnsEmptySubjects()
    {
        var filePath = Path.Combine(TestFilesDir, "malformed_subjects_data.json");
        var provider = new JsonProvider(filePath);

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var subjects = await provider.GetSubjectsAsync();

        Assert.NotNull(subjects);
        Assert.IsEmpty(subjects);
        var output = stringWriter.ToString();
        Assert.That(output, Does.Contain("Error reading JSON file"));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }

    [Test]
    public async Task GetSubjectsAsync_WithNotExistingJsonFile_ReturnsEmptySubjects()
    {
        var filePath = Path.Combine(TestFilesDir, "non-existent-data.json");
        var provider = new JsonProvider(filePath);

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var subjects = await provider.GetSubjectsAsync();

        Assert.NotNull(subjects);
        Assert.IsEmpty(subjects);
        var output = stringWriter.ToString();
        Assert.That(output, Does.Contain("JSON file not found at path"));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }

}