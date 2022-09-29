using PortalEscolar.Communication.Request.Aluno;

namespace PortalEscolar.Communication.Request.Matricula;
public class DadosAluno : RequestRegistrarAlunoJson
{
    public long TurmaId { get; set; }
}
