﻿using PortalEscolar.Application.UseCases.Diretora.FazerLogin;
using Utilities.Criptografia;
using Utilities.Repositories.Diretor;
using Utilities.Services.Token;
using Xunit;
using FluentAssertions;
using Utilities.Entities;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions.ExceptionsBase;
using PortalEscolar.Exceptions;

namespace UseCase.Test.Diretor.FazerLogin;
public class FazerLoginDiretorUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_TokenENomeValidos()
    {
        (var diretor,var senha) = DiretorBuilder.Build();
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

    private FazerLoginDiretorUseCase UseCaseBuild(PortalEscolar.Domain.Entities.Diretoria.Diretor diretor)
    {
        var repoRead = DiretorReadOnlyRepositoryBuilder.Instance().ObterPorEmailSenha(diretor).Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var token = TokenControllerBuilder.Instance();
        
        var useCase = new FazerLoginDiretorUseCase(repoRead, encriptador, token);
        return useCase;
    }
}


//private readonly IDiretorReadOnlyRepository _repoRead;
//private readonly EncriptadorDeSenha _encriptadorDeSenha;
//private readonly TokenController _tokenController;