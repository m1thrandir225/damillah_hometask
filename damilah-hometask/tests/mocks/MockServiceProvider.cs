using damilah_hometask.data.providers;
using damilah_hometask.domain;

namespace tests.mocks;

public class MockServiceProvider : ISubjectProvider
{
    public List<Subject> SubjectsToReturn { get; set; } = new List<Subject>();
    public Exception? ExceptionToThrow { get; set; }
    public int GetSubjectsCallCount { get; set; } = 0;

    public Task<List<Subject>> GetSubjectsAsync()
    {
        GetSubjectsCallCount++;
        if (ExceptionToThrow != null)
        {
            return Task.FromException<List<Subject>>(ExceptionToThrow);
        }
        return Task.FromResult(SubjectsToReturn);
    }

}