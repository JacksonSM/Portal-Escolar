namespace PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
public class ObterListaExerciciosQuery
{
    public long TurmaId { get; set; }
    public int? Disciplina { get; set; }
    public string Nome { get; set; }
    public int? PaginaAtual { get; set; }
    public int? ExerciciosPorPagina { get; set; }


}
