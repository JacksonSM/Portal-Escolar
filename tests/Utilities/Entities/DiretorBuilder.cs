using Bogus;
using PortalEscolar.Domain.Entities.Diretoria;
using System.Globalization;
using Utilities.Services.Criptografia;

namespace Utilities.Entities;
public class DiretorBuilder
{
    public static (Diretor diretor,string senha) Build()
    {
        var senha = "";
        var diretor = new Faker<Diretor>()
            .RuleFor(c => c.Papel,PortalEscolar.Domain.Enum.Papel.Diretor)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => 
            {
                senha = f.Internet.Password();
                var senhaCriptografada = EncriptadorDeSenhaBuilder.Instance().Criptografar(senha);

                return senhaCriptografada;
            })
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth);

        return (diretor, senha);
    }
}
