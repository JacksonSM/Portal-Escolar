using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.Diretoria;
public class Diretor : Usuario
{
    public string NomeCompleto { get; set; } 
    public DateTime DataNascimento { get; set; }
    public Diretor() { }
    public Diretor(string nomeCompleto,DateTime dataNascimento, string email, string senha) :base(email, senha)
    {
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        AtribuirPapel();
    }
    private void AtribuirPapel()
    {
        Papel = Papel.Diretor;
    }
   
}
