using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioResolvido;
public class ExercicioResolvidoDoc
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public long ProfessoraId { get; set; }
    public string ExercicioParaResolverId { get; set; }
    public long AlunoId { get; set; }
    public long TurmaId { get; set; }
    public string Nome { get; set; }
    public DateTime DataEntrega { get; set; }
    public Disciplina Disciplina { get; set; }
    public float Nota { get; set; }
    public List<QuestoesExercicioResolvidoDoc> Questoes { get; set; }
}
