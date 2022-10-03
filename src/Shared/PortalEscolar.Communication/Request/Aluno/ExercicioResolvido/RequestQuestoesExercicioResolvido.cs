namespace PortalEscolar.Communication.Request.Aluno.ExercicioResolvido;
public class RequestQuestoesExercicioParaResolverJson
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaSelecionada { get; set; }
}
