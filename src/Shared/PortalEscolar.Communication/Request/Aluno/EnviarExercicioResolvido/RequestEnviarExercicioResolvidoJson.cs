namespace PortalEscolar.Communication.Request.Aluno.EnviarExercicioResolvido;
public class RequestEnviarExercicioResolvidoJson
{
    public string ExercicioParaResolverId { get; set; }
    public List<RequestQuestoesEnviarExercicioParaResolverJson> Questoes { get; set; }

}
