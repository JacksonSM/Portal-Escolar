namespace PortalEscolar.Domain.Entities.Papel;
public class Papel : EntityBase
{
    public string Nome { get; set; }
    public string NomeNormalizado { get; set; }
    public Papel() { }
    public Papel(string nome,long id)
    {
        this.Id = id;
        Nome = nome;
        NomeNormalizado = nome.ToUpper();
    }
}
