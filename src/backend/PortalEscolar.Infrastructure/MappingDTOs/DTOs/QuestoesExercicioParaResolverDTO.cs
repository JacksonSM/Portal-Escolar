namespace PortalEscolar.Infrastructure.Mapping.DTOs;

public class QuestoesExercicioParaResolverDTO
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaCorreta { get; set; }
    public float Pontos { get; set; }
}