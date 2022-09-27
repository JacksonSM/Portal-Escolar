using Bogus;
using PortalEscolar.Communication.Request.Diretor;

namespace Utilities.Requests;
public class RequestAlterarInfoPessoalDiretorBuilder
{
    public static RequestAlterarInfoPessoalDiretorJson Build()
    {
        return new Faker<RequestAlterarInfoPessoalDiretorJson>()
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy"));
    }
}
