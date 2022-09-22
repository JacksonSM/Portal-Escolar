using Bogus;
using PortalEscolar.Communication.Request.Matricula;

namespace Utilities.Requests;
public class RequestMatricularAlunoBuilder
{
    public static RequestMatricularAlunoJson Build()
    {
        var matricularFake = new Faker<RequestMatricularAlunoJson>()
            .RuleFor(c => c.CidadeNascimentoAluno, f => f.Address.City())
            .RuleFor(c => c.DataInicio, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy"))
            .RuleFor(c => c.TurmaId, f => f.Random.Number(5)).Generate();

        var dataInicio = DateTime.Parse(matricularFake.DataInicio);
        matricularFake.DataTerminio = dataInicio.AddYears(1).ToString("dd/MM/yyyy");

        var dadosAluno = new Faker<DadosAluno>()
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password())
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy")).Generate();

        var dadosResponsavel = new Faker<DadosResponsavel>()
            .RuleFor(c => c.NomeCompleto, f => f.Person.FullName)
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy"))
            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
            .RuleFor(c => c.CPF, "56376341063")
            .RuleFor(c => c.Cidade, f => f.Address.City()).Generate();

        matricularFake.Aluno = dadosAluno;
        matricularFake.Responsavel = dadosResponsavel;

        return matricularFake;
    }
}
