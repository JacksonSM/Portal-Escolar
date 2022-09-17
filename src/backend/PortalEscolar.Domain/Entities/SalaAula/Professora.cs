using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula;
public class Professora : Usuario
{
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    
    public Professora(string nomeCompleto, DateTime dataNascimento, string email, string senha) : base(email, senha)
    {
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        AtribuirPapel();
    }
    private void AtribuirPapel()
    {
        Papel = Papel.Professora;
    }
}
