using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Communication.Response.Aluno.ExercicioResolvido;
public class ResponseExercicioResolvidoJson
{
    public string Nome { get; set; }
    public Disciplina Disciplina { get; set; }
    public float Nota { get; set; }
    public DateTime DataEntrega { get; set; }
    public List<ResponseQuestoesExercicioResolvidoJson> Questoes { get; set; }
}
