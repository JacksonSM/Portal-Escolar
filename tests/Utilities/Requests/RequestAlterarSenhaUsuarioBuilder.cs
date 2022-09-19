using Bogus;
using PortalEscolar.Communication.Request;

namespace Utilities.Requests;
public class RequestAlterarSenhaUsuarioBuilder
{
    public static RequestAlterarSenhaUsuarioJson Build(int tamanhoSenhaNova = 10)
    {
        return new Faker<RequestAlterarSenhaUsuarioJson>()
            .RuleFor(c => c.SenhaAtual, f => f.Internet.Password())
            .RuleFor(c => c.SenhaNova, f => f.Internet.Password(tamanhoSenhaNova));
    }
}
