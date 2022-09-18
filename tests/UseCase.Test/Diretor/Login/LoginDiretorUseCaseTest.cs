using Utilities.Repositories.Diretor;
using Utilities.Services.Token;
using Xunit;
using FluentAssertions;
using Utilities.Entities;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions.ExceptionsBase;
using PortalEscolar.Exceptions;
using PortalEscolar.Application.UseCases.Diretora.Login;
using Utilities.Services.Criptografia;

namespace UseCase.Test.Diretor.Login;
public class LoginDiretorUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_TokenENomeValidos()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = diretor.Email,
            Senha = senha
        };

        var useCase = UseCaseBuild(diretor);
        var response = await useCase.ExecuteAsync(request);

        response.Should().NotBeNull();
        response.Nome.Should().NotBeNullOrWhiteSpace();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async void Email_EmailVazio_DeveLancarExceptionLoginInvalido()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "",
            Senha = senha
        };

        var useCase = UseCaseBuild(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Email_EmailInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "test@gmail.com",
            Senha = senha
        };

        var useCase = UseCaseBuild(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Senha_SenhaVazia_DeveLancarExceptionLoginInvalido()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = diretor.Email,
            Senha = string.Empty
        };

        var useCase = UseCaseBuild(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Senha_SenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = diretor.Email,
            Senha = "tes@12t"
        };

        var useCase = UseCaseBuild(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void SenhaEEmail_OsDadosDeEmailESenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var diretor, var senha) = DiretorBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "test@gmail.com.br",
            Senha = "tes@12t"
        };

        var useCase = UseCaseBuild(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    private LoginDiretorUseCase UseCaseBuild(PortalEscolar.Domain.Entities.Diretoria.Diretor diretor)
    {
        var repoRead = DiretorReadOnlyRepositoryBuilder.Instance().ObterPorEmailSenha(diretor).Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var token = TokenControllerBuilder.Instance();

        var useCase = new LoginDiretorUseCase(repoRead, encriptador, token);
        return useCase;
    }
}