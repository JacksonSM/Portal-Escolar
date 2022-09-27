using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
public class ExercicioParaResolver 
{
    public string Id { get; set; }
    public long ProfessoraId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<QuestoesExercicioParaResolver> Questoes { get; set; }
}
