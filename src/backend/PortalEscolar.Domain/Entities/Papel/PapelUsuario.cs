namespace PortalEscolar.Domain.Entities.Papel;
public class PapelUsuario 
{
    public string EmailUsuario { get; set; }
    public long PapelId { get; set; }
    public Papel Papel { get; set; }
}
