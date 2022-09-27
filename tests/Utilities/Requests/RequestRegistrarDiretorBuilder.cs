using Bogus;
using PortalEscolar.Communication.Request.Professora;

namespace Utilities.Requests;
public class RequestRegistrarProfessoraBuilder
{
    public static RequestRegistrarProfessoraJson Build(int tamanhoSenha = 10)
    {
        return new Faker<RequestRegistrarProfessoraJson>()
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => f.Internet.Password(tamanhoSenha))
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy"));
    }
}
