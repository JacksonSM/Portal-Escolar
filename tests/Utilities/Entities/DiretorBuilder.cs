using Bogus;
using PortalEscolar.Domain.Entities.Diretoria;
using Utilities.Criptografia;

namespace Utilities.Entities;
public class DiretorBuilder
{
    public static Diretor Build()
    {
        return new Faker<Diretor>()
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => 
            {
                var senha = f.Internet.Password();
                var senhaCriptografada = EncriptadorDeSenhaBuilder.Instancia().Criptografar(senha);

                return senhaCriptografada;
            })
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => 
            {
                var dateFake = f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy");

                return DateTime.Parse(dateFake);
            });
    }
}
