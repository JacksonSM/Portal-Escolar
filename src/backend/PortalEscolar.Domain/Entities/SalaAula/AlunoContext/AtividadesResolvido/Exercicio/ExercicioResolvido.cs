using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio;
public class ExercicioResolvido : EntityBase
{
    public long ProfessoraId { get; set; }
    public long AlunoId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public DateTime DataEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public float Nota { get; set; }
    public List<QuestoesExercicioResolvido> Questoes { get; set; }
}
