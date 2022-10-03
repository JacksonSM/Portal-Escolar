namespace PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioResolvido;
public class QuestoesExercicioResolvidoDoc
{
    public int Ordem { get; set; }
    public string Enunciado { get; set; }
    public List<string> Alternativas { get; set; }
    public string AlternativaSelecionada { get; set; }
    public float Pontos { get; set; }
}
