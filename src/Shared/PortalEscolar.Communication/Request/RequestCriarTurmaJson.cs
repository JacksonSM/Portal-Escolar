using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Communication.Request;
public class RequestCriarTurmaJson
{
    public long ProfessoraId { get; set; }
    public string Sala { get; set; }
    public string NomeTurma { get; set; }
    public Serie Serie { get; set; }
    public Turno Turno { get; set; }
}
