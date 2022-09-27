using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Communication.Request.Exercicio;
public class RequestEnviarExercicioJson
{
    public long ProfessoraId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public string PrazoEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<QuestoesExercicioJson> Questoes { get; set; }
}
