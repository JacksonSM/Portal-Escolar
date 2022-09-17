using Bogus;
using PortalEscolar.Domain.Entities.SalaAula;
using Utilities.Criptografia;

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
            .RuleFor(c => c.DataNascimento, f => 
            {
                var dateFake = f.Person.DateOfBirth.Date.ToString("dd/MM/yyyy");

                return DateTime.ParseExact(dateFake, "dd/MM/yyyy", null);
            });

        return (professora, senha);
    }
}
