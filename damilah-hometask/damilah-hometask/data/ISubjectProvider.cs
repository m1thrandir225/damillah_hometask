using damilah_hometask.domain;

namespace damilah_hometask.data;

public interface ISubjectProvider
{
    Task<List<Subject>> GetSubjectsAsync();
}