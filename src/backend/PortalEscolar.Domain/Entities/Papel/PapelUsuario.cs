namespace PortalEscolar.Domain.Entities.Papel;
public class PapelUsuario 
{
    //TODO - Alterar Email como chave da relação, e coloca id usuario.  
    public string EmailUsuario { get; set; }
    public long PapelId { get; set; }
    public Papel Papel { get; set; }
}
