using damilah_hometask.domain;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace tests;

public class SubjectTests
{
    [Fact]
    public void ToString_RetrunsCorrectFormat()
    {
        var subject = new Subject(
            name: "Subject",
            description: "Example Description",
            classesPerWeek: 2,
            instructorName: "James Brown",
            syllabusLink: "example.com"
        );

        var expected = "Subject";

        var actual = subject.ToString();

        Assert.That(actual, Is.EqualTo(expected));

    }
    [Fact]
    public void PrintDetails_OutputsCorrectInformation()
    {
        var lit1 = new Literature("Literature 1",
            "literature1.com", "Author 1",
            new DateTime(2015, 06, 15));
        var lit2 = new Literature("Literature 2",
            "literature2.com", "Author 2",
            new DateTime(2023, 04, 30));
        var subject = new Subject(
            name: "Subject",
            description: "Example Description",
            classesPerWeek: 2,
            instructorName: "James Brown",
            syllabusLink: "example.com",
            relatedLiterature: new List<Literature>
            {
                lit1,
                lit2
            }
        );

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        subject.PrintDetails();
        var output = stringWriter.ToString();

        Assert.That(output, Does.Contain("Subject"));
        Assert.That(output, Does.Contain("Example Description"));
        Assert.That(output, Does.Contain("2"));
        Assert.That(output, Does.Contain("James Brown"));
        Assert.That(output, Does.Contain("example.com"));
        Assert.That(output, Does.Contain($"    * {lit1.ToString()}"));
        Assert.That(output, Does.Contain($"    * {lit2.ToString()}"));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }

    [Fact]
    public void PrintDetails_WhenNoLiterature()
    {
        var subject = new Subject(
            name: "Subject",
            description: "Example Description",
            classesPerWeek: 2,
            instructorName: "James Brown",
            syllabusLink: "example.com"
        );

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        subject.PrintDetails();
        var output = stringWriter.ToString();

        Assert.That(output, Does.Contain("Subject"));
        Assert.That(output, Does.Contain("Example Description"));
        Assert.That(output, Does.Contain("2"));
        Assert.That(output, Does.Contain("James Brown"));
        Assert.That(output, Does.Contain("example.com"));
        Assert.That(output, Does.Contain("No related literature."));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }

    [Fact]
    public void PrintDetails_NoInstructor()
    {
        var subject = new Subject(
            name: "Subject",
            description: "Example Description",
            classesPerWeek: 2,
            syllabusLink: "example.com"
        );

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        subject.PrintDetails();
        var output = stringWriter.ToString();

        Assert.That(output, Does.Contain("Subject"));
        Assert.That(output, Does.Contain("Example Description"));
        Assert.That(output, Does.Contain("2"));
        Assert.That(output, Does.Not.Contain("Instructor Name"));
        Assert.That(output, Does.Contain("example.com"));
        Assert.That(output, Does.Contain("No related literature."));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);

    }


    [Fact]
    public void PrintDetails_NoSyllabus()
    {
        var subject = new Subject(
            name: "Subject",
            description: "Example Description",
            classesPerWeek: 2
        );

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        subject.PrintDetails();
        var output = stringWriter.ToString();

        Assert.That(output, Does.Contain("Subject"));
        Assert.That(output, Does.Contain("Example Description"));
        Assert.That(output, Does.Contain("2"));
        Assert.That(output, Does.Not.Contain("Syllabus Link"));
        Assert.That(output, Does.Contain("No related literature."));

        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }}