using Bogus;
using PortalEscolar.Communication.Request.Exercicio;
using PortalEscolar.Domain.Enum;

namespace Utilities.Requests;
public class RequestEnviarExercicioJsonBuilder
{
    public static RequestEnviarExercicioJson Build()
    {
        var exercicioFake = new Faker<RequestEnviarExercicioJson>()
            .RuleFor(c => c.PrazoEntrega, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy HH:mm"))
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .RuleFor(c => c.Disciplina, f => f.PickRandom<Disciplina>()).Generate();

        var questoesFakes = new Faker<QuestoesExercicioJson>()
            .RuleFor(c => c.Ordem, f => f.IndexFaker+1)
            .RuleFor(c => c.Enunciado, f => f.Lorem.Paragraph())
            .RuleFor(c => c.Alternativas, f => f.Make(4, () => f.Lorem.Lines()))
            .RuleFor(c => c.AlternativaCorreta, f => f.Lorem.Letter()).Generate(5);

        exercicioFake.Questoes = questoesFakes;

        return exercicioFake;
    }
}
