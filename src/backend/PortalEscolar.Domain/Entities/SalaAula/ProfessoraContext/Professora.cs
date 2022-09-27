using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext;
public class Professora : Usuario
{
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<Turma> Turma { get; set; }

    public Professora() { }
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
