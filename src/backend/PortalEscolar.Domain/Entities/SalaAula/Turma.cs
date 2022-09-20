using PortalEscolar.Domain.Entities.Diretoria.Matricula;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula;
public class Turma : EntityBase
{
    public long ProfessoraId { get; set; }
    public Professora Professora { get; set; }
    public string Sala { get; set; }
    public string NomeTurma { get; set; }
    public Serie Serie { get; set; }
    public Turno Turno { get; set; }
    public bool Ativo { get; set; }
    public List<Aluno> Alunos { get; set; }
    public List<Matricula> Matricula { get; set; }
}
