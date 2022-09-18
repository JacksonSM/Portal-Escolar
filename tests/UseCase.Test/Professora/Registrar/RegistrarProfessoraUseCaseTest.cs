using PortalEscolar.Application.UseCases.Professora.Registrar;
using Utilities.Requests;
using Xunit;
using FluentAssertions;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Repositories;
using Utilities.Repositories.Professora;
using Utilities.Services.Criptografia;
using Utilities.Services.Mapper;

namespace UseCase.Test.Professora.Registrar;
public class RegistrarProfessoraUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaGenericResponseJsonComMensagemDeSucesso()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();
        var useCase = BuildUseCase();
        var result = await useCase.ExecuteAsync(request);

        result.Should().NotBeNull();
        result.Mensagem.Should().NotBeNullOrWhiteSpace();
        result.Mensagem.Should().Contain(ResourceMensagensDeErro.REGISTRAR_PROFESSORA_SUCESSO);
    }

    [Fact]
    public async void Email_EmailExistente_DeveRetornaMensagemComErroDeEmailExistente()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();
        var useCase = BuildUseCase(request.Email);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_EXISTENTE));

    }


    private RegistrarProfessoraUseCase BuildUseCase(string email = "")
    {
        var repoRead = ProfessoraReadOnlyRepositoryBuilder.Instance().ExisteEmail(email).Build();
        var repoWrite = ProfessoraWriteOnlyRepositoryBuilder.Instance().Build();
        var mapper = AutoMapperBuilder.Instance();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var unit = UnitOfWorkBuilder.Instance().Build();

        return new RegistrarProfessoraUseCase(repoRead, repoWrite,mapper, encriptador, unit);
    }
}