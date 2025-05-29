using damilah_hometask.domain;

namespace damilah_hometask.data.providers;

public interface ISubjectProvider
{
    Task<List<Subject>> GetSubjectsAsync();
}