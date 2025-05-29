namespace damilah_hometask.domain;

public class BaseEntity
{
    public Guid id { get; set; } = Guid.NewGuid();
}