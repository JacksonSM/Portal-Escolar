namespace PortalEscolar.Communication.Response.Aluno.Exercicio;
public class ExercicioParaResolverJson
{
    public long ProfessoraId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public int Disciplina { get; set; }
    public List<QuestoesExercicioParaResolverJson> Questoes { get; set; }
}
