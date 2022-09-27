using Bogus;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.SalaAula.AlunoContext;
using System.Globalization;
using Utilities.Services.Criptografia;

namespace Utilities.Entities;
public class AlunoBuilder
{
    public static (Aluno aluno,string senha) Build()
    {
        var senha = "";
        var aluno = new Faker<Aluno>()
            .RuleFor(c => c.Papel,PortalEscolar.Domain.Enum.Papel.Aluno)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Senha, f => 
            {
                senha = f.Internet.Password();
                var senhaCriptografada = EncriptadorDeSenhaBuilder.Instance().Criptografar(senha);

                return senhaCriptografada;
            })
            .RuleFor(c => c.NomeCompleto, f => f.Name.FullName())
            .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth);

        return (aluno, senha);
    }
}
