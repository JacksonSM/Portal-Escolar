using PortalEscolar.Domain.Entities.Diretoria.Matricula;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula.AlunoContext;
public class Aluno : Usuario
{
    public string NomeCompleto { get; set; }
    public DateTime DataNascimento { get; set; }
    public List<Matricula> Matricula { get; set; }
    public Aluno() { }
    public Aluno(string nomeCompleto, DateTime dataNascimento, string email, string senha) : base(email, senha)
    {
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        AtribuirPapel();
    }
    private void AtribuirPapel()
    {
        Papel = Papel.Aluno;
    }
}
