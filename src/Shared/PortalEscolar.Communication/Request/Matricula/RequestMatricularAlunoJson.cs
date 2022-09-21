namespace PortalEscolar.Communication.Request.Matricula;
public class RequestMatricularAlunoJson
{
    public string CidadeNascimentoAluno { get; set; }
    public string DataInicio { get; set; }
    public string DataTerminio { get; set; }
    public long TurmaId { get; set; }
    public DadosAluno Aluno { get; set; }
    public DadosResponsavel Responsavel { get; set; }
}
