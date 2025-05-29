using damilah_hometask.domain;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace tests;

public class LiteratureTests
{
    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        var date = new DateTime();
        var literature = new Literature("Book1", "example.com", "James Brown", date);

        var expected = $"Name: Book1, Url: example.com, Author: James Brown, PublishDate: {date.ToShortDateString()}";

        var actual = literature.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }
}