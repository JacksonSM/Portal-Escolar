using FluentAssertions;
using PortalEscolar.Application.UseCases.Aluno.Login;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Entities;
using Utilities.Repositories.Aluno;
using Utilities.Services.Criptografia;
using Utilities.Services.Token;
using Xunit;

namespace UseCase.Test.Aluno.Login;
public class LoginAlunoUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_TokenENomeValidos()
    {
        (var aluno, var senha) = AlunoBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = aluno.Email,
            Senha = senha
        };

        var useCase = UseCaseBuild(aluno);
        var response = await useCase.ExecuteAsync(request);

        response.Should().NotBeNull();
        response.Nome.Should().NotBeNullOrWhiteSpace();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async void Email_EmailVazio_DeveLancarExceptionLoginInvalido()
    {
        (var aluno, var senha) = AlunoBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "",
            Senha = senha
        };

        var useCase = UseCaseBuild(aluno);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Email_EmailInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var aluno, var senha) = AlunoBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "test@gmail.com",
            Senha = senha
        };

        var useCase = UseCaseBuild(aluno);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Senha_SenhaVazia_DeveLancarExceptionLoginInvalido()
    {
        (var aluno, var senha) = AlunoBuilder.Build();

        var request = new RequestUsuarioLoginJson
        {
            Email = aluno.Email,
            Senha = string.Empty
        };

        var useCase = UseCaseBuild(aluno);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void Senha_SenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var aluno, var senha) = AlunoBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = aluno.Email,
            Senha = "tes@12t"
        };

        var useCase = UseCaseBuild(aluno);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }
    [Fact]
    public async void SenhaEEmail_OsDadosDeEmailESenhaInvalido_DeveLancarExceptionLoginInvalido()
    {
        (var aluno, var senha) = AlunoBuilder.Build();
        var request = new RequestUsuarioLoginJson
        {
            Email = "test@gmail.com.br",
            Senha = "tes@12t"
        };

        var useCase = UseCaseBuild(aluno);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<LoginInvalidoException>()
             .Where(exception => exception.Message.Equals(ResourceMensagensDeErro.LOGIN_INVALIDO));
    }

    private LoginAlunoUseCase UseCaseBuild(PortalEscolar.Domain.Entities.SalaAula.Aluno aluno)
    {
        var repoRead = AlunoReadOnlyRepositoryBuilder.Instance().ObterPorEmailSenha(aluno).Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var token = TokenControllerBuilder.Instance();

        var useCase = new LoginAlunoUseCase(repoRead, encriptador, token);
        return useCase;
    }
}


