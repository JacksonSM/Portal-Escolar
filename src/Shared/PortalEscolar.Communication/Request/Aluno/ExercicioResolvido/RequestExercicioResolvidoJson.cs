using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Communication.Request.Aluno.ExercicioResolvido;
public class RequestExercicioResolvidoJson
{
    public string ExercicioParaResolverId { get; set; }
    public long ProfessoraId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public string DataEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<RequestQuestoesExercicioParaResolverJson> Questoes { get; set; }
}
