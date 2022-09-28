namespace PortalEscolar.Communication.Response.Aluno.ObterListaExercicios;
public class ResponseQuestoesExercicioParaResolverJson
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaCorreta { get; set; }
    public float Pontos { get; set; }
}
