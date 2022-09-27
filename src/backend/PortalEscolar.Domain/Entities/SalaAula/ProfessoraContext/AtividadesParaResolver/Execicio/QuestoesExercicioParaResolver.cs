namespace PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
public class QuestoesExercicioParaResolver
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaCorreta { get; set; }
    public float Pontos { get; set; }
}
