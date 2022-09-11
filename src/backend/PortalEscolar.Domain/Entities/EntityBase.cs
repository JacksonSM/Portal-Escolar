namespace PortalEscolar.Domain.Entities;
public class EntityBase
{
    public long Id { get; set; }
    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
}
