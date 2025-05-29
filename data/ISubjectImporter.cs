using damilah_hometask.domain;

namespace damilah_hometask.data;

public interface ISubjectImporter
{
    public List<Subject> ImportMultiple();
    public Subject ImportSingle();
}