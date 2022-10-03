namespace PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioParaResolver;

public class QuestoesExercicioParaResolverDoc
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaCorreta { get; set; }
    public float Pontos { get; set; }
}