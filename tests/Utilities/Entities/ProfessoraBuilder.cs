using Bogus;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext;
using System.Globalization;
using Utilities.Services.Criptografia;

namespace Utilities.Entities;
public class ProfessoraBuilder
{
    public static (Professora professora,string senha) Build()
    {
        var senha = "";
        var professora = new Faker<Professora>()
            .RuleFor(c => c.Papel, PortalEscolar.Domain.Enum.Papel.Professora)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => 
            {
                senha = f.Internet.Password();
                var senhaCriptografada = EncriptadorDeSenhaBuilder.Instance().Criptografar(senha);

                return senhaCriptografada;
            })
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth);

        return (professora, senha);
    }
}
