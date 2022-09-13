using Bogus;
using PortalEscolar.Domain.Entities.Diretoria;
using Utilities.Criptografia;

namespace Utilities.Entities;
public class DiretorBuilder
{
    public static (Diretor diretor,string senha) Build()
    {
        var senha = "";
        var diretor = new Faker<Diretor>()
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

                return DateTime.Parse(dateFake);
            });

        return (diretor, senha);
    }
}
