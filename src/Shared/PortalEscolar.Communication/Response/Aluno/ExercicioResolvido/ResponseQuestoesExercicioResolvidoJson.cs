namespace PortalEscolar.Communication.Response.Aluno.ExercicioResolvido;

public class ResponseQuestoesExercicioResolvidoJson
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaSelecionada { get; set; }
    public float Pontos { get; set; }
}