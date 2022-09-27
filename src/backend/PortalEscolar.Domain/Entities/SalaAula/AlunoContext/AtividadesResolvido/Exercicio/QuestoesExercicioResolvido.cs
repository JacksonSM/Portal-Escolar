namespace PortalEscolar.Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio;
public class QuestoesExercicioResolvido
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaSelecionada { get; set; }
    public float Pontos { get; set; }
}
