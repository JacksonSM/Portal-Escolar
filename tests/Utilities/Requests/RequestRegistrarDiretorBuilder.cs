using Bogus;
using PortalEscolar.Communication.Request;

namespace Utilities.Requests;
public class RequestRegistrarDiretorBuilder
{
    public static RequestRegistrarDiretorJson Build(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarDiretorJson>()
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy"));
    }
}
