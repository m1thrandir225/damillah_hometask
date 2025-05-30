using damilah_hometask.data.providers.impl;
using NUnit.Framework;

namespace tests;

public class InMemorySubjectProviderTests
{
    [Test]
    public async Task GetSubjectsAsync_ReturnsPredefinedSubjects()
    {
        var provider = new InMemoryProvider();

        var subjects = await provider.GetSubjectsAsync();

        Assert.NotNull(subjects);

        Assert.True(subjects.Count >= 3);

        var mathSubject = subjects.FirstOrDefault(s => s.Name == "MATH22");

        Assert.NotNull(mathSubject);
        Assert.That(mathSubject.Description, Is.EqualTo("Multivariable Calculus and Linear Algebra"));
        Assert.That(mathSubject.ClassesPerWeek, Is.EqualTo(4));
        Assert.That(mathSubject.InstructorName, Is.EqualTo("Giovanni Brown"));
        Assert.That(mathSubject.SyllabusLink, Is.EqualTo("https://math22.com/syllabus"));
        Assert.True(mathSubject.RelatedLiterature.Count >= 2);

        var mathLiterature =
            mathSubject.RelatedLiterature.FirstOrDefault(s => s.Name == "Multivariable Calculus 8th Edition");

        Assert.NotNull(mathLiterature);
        Assert.That(mathLiterature.Url, Is.EqualTo("https://www.amazon.com/Multivariable-Calculus-James-Stewart"));
        Assert.That(mathLiterature.Author, Is.EqualTo("James Stewart"));
        Assert.That(mathLiterature.PublishDate.ToShortDateString(), Is.EqualTo(new DateTime(2015, 06, 15).ToShortDateString()));
    }


}