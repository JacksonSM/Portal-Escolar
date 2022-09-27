using PortalEscolar.Application.UseCases.Professora.Login;
using PortalEscolar.Communication.Request;
using Utilities.Entities;
using Utilities.Services.Token;
using FluentAssertions;
using Xunit;
using PortalEscolar.Exceptions.ExceptionsBase;
using PortalEscolar.Exceptions;
using Utilities.Services.Criptografia;
using Utilities.Repositories.Professora;

namespace UseCase.Test.Professora.Login;
public class LoginProfessoraUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaToken()
    {
        (var professora, var senha) = ProfessoraBuilder.Build();

        var request = new RequestUsuarioLoginJson { Email = professora.Email, Senha = senha };

        var useCase = UseCaseBuilder(professora);

        var response = await useCase.ExecuteAsync(request);

        response.Should().NotBeNull();
        response.Nome.Should().NotBeNullOrWhiteSpace();
        response.Token.Should().NotBeNullOrWhiteSpace();

    }
    [Fact]
    public async void Email_EmailInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var professora, var senha) = ProfessoraBuilder.Build();

        var request = new RequestUsuarioLoginJson 
        {
            Email = "tes@gmail.com",
            Senha = senha 
        };

        var useCase = UseCaseBuilder(professora);              

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }
    [Fact]
    public async void Senha_SenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var professora, var senha) = ProfessoraBuilder.Build();

        var request = new RequestUsuarioLoginJson
        {
            Email = professora.Email,
            Senha = "35456fg"
        };

        var useCase = UseCaseBuilder(professora);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }
    [Fact]
    public async void EmailESenha_EmailESenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var professora, var senha) = ProfessoraBuilder.Build();

        var request = new RequestUsuarioLoginJson
        {
            Email = "testebi@gmail.com",
            Senha = "fjdjhd3445"
        };

        var useCase = UseCaseBuilder(professora);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));

    }

    private LoginProfessoraUseCase UseCaseBuilder(PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.Professora professora)
    {
        var repo = ProfessoraReadOnlyRepositoryBuilder
            .Instance()
            .ObterPorEmailESenhaAsync(professora).Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var tokenController = TokenControllerBuilder.Instance();
        
        var useCase = new LoginProfessoraUseCase(repo,encriptador,tokenController);
        return useCase;
    }
}
