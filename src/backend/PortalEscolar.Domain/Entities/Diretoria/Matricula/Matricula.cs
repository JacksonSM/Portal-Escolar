using PortalEscolar.Domain.Entities.SalaAula;
using PortalEscolar.Domain.Entities.SalaAula.AlunoContext;

namespace PortalEscolar.Domain.Entities.Diretoria.Matricula;
public class Matricula : EntityBase
{
    public string NomeCompletoAluno { get; set; }
    public DateTime DataNascimentoAluno { get; set; }
    public string CidadeNascimentoAluno { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataTerminio { get; set; }
    public Responsavel Responsavel { get; set; }
    public Aluno Aluno { get; set; }
    public long AlunoId { get; set; }
    public Turma Turma { get; set; }
    public long TurmaId { get; set; }
}
