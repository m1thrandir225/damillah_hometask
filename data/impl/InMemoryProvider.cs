using damilah_hometask.domain;

namespace damilah_hometask.data.impl;

public class InMemoryProvider : ISubjectProvider
{
    public Task<List<Subject>> GetSubjectsAsync()
    {
        var subjects = new List<Subject>
        {
            new Subject(
                name:"MATH22",
                description:"Multivariable Calculus and Linear Algebra",
                classesPerWeek: 4,
                instructorName: "Giovanni Brown",
                syllabusLink: "https://math22.com/syllabus",
                relatedLiterature: new List<Literature>{
                  new Literature("Multivariable Calculus 8th Edition", "https://www.amazon.com/Multivariable-Calculus-James-Stewart", "James Stewart", new DateTime(2015, 06, 15)),
                  new Literature("Introduction to Linear Algebra 6th Edition", "https://www.amazon.com/Introduction-Linear-Algebra-Gilbert-Strang", "Gilbert Strang", new DateTime(2023, 04, 30)),
                }
            ),
            new Subject(
                name: "CS50",
                description: "Introduction to Computer Science",
                classesPerWeek: 3,
                instructorName: "David J. Malan",
                syllabusLink: "https://harvard.com/"
            ),
            new Subject(
                name: "ECON10 B",
                description: "Principles of Economics",
                classesPerWeek: 2,
                instructorName: "Jason Furman",
                syllabusLink: "https://economics.sas.upenn.edu/sites/default/files/penncourse/Saka%20F20%20Ec%2010%20syllabus.pdf",
                relatedLiterature: new List<Literature>{
                  new Literature("Principles of Economics 8th Edition", "https://www.amazon.com/Principles-Economics-N-Gregory-Mankiw/dp/1305585127", "N. Makiw", new DateTime(2017, 01, 1))
                }
            )
        };
        return Task.FromResult(subjects);
    }
}
