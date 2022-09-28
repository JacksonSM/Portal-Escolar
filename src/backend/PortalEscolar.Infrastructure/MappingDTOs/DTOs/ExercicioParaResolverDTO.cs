using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Infrastructure.Mapping.DTOs;
public class ExercicioParaResolverDTO
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public long ProfessoraId { get; set; }
    public string NomeProfessora { get; set; }
    public long TurmaId { get; set; }
    public string NomeTurma { get; set; }
    public string Nome { get; set; }
    public DateTime PrazoEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public List<QuestoesExercicioParaResolverDTO> Questoes { get; set; }
}
