using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Infrastructure.Mapping.DTOs;
public class ExercicioParaResolverDTO
{
    public long ProfessoraId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<QuestoesExercicioParaResolverDTO> Questoes { get; set; }
}
