namespace PortalEscolar.Communication.Request.Aluno.EnviarExercicioResolvido;
public class RequestQuestoesEnviarExercicioParaResolverJson
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaSelecionada { get; set; }
}
