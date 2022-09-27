using Bogus;
using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Domain.Enum;

namespace Utilities.Requests;
public class RequestCriarTurmaJsonBuilder
{
    public static RequestCriarTurmaJson Build()
    {
        return new Faker<RequestCriarTurmaJson>()
            .RuleFor(c => c.ProfessoraId, f => f.Random.Number(5))
            .RuleFor(c => c.Sala, f => f.Person.FirstName)
            .RuleFor(c => c.NomeTurma, f => f.Person.FirstName)
            .RuleFor(c => c.Serie, f => f.PickRandom<Serie>())
            .RuleFor(c => c.Turno, f => f.PickRandom<Turno>());
    }
}
