namespace PortalEscolar.Communication.Response.Aluno.ObterListaExercicios;
public class ResponseExercicioParaResolverJson
{
    public string Id { get; set; }
    public string NomeProfessora { get; set; }
    public string NomeTurma { get; set; }
    public string Nome { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public int Disciplina { get; set; }
    public List<ResponseQuestoesExercicioParaResolverJson> Questoes { get; set; }
}
