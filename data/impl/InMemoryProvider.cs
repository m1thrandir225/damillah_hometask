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
                literature: new List<Literature>{}
            ),
            new Subject(
                name: "CS50",
                description: "Introduction to Computer Science",
                classesPerWeek: 3,
                instructorName: "David J. Malan",
                syllabusLink: "https://harvard.com/",
                new List<Literature>{}
            ),
            new Subject(
                name: "ECON10 B",
                description: "Principles of Economics",
                classesPerWeek: 2,
                instructorName: "Jason Furman",
                syllabusLink: "https://economics.sas.upenn.edu/sites/default/files/penncourse/Saka%20F20%20Ec%2010%20syllabus.pdf"
            )
        };
        return Task.FromResult(subjects);
    }
}
