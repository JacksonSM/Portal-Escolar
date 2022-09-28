namespace PortalEscolar.Communication.Request.Aluno;
public class RequestObterListaExerciciosQuery
{
    public int? Disciplina { get; set; }
    public string Nome { get; set; }
    public int? PaginaAtual { get; set; }
    public int? ExerciciosPorPagina { get; set; }
}
