using FluentAssertions;
using PortalEscolar.Application.UseCases.Diretora.Registrar;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Repositories;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
using Utilities.Services.Criptografia;
using Utilities.Services.Mapper;
using Utilities.Services.Token;
using Xunit;

namespace UseCase.Test.Diretor.Registrar;
public class RegistrarDiretorUseCaseTest
{
    [Fact]
    public async void UseCase_ParametrosValidos_EsperadoResponseTokenJsonComTokenENome()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.ExecuteAsync(request);

        result.Should().NotBeNull();    
        result.Token.Should().NotBeNullOrWhiteSpace();
        result.Nome.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async void ValidacaoEmailExistente_MetodoExistEmailRetornaTrue_ELancadaExceptionEmailExistente()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_EXISTENTE));
    }
    [Fact]
    public async void ValidacaoEmailVazio_EmailVazio_ELancadaExceptionEmailVazio()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        var useCase = CreateUseCase();
        request.Email = string.Empty;

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_VAZIO));
    }



    private RegistrarDiretorUseCase CreateUseCase(string email = "")
    {
        var repoDiretorWrite = DiretorWriteOnlyRepositoryBuilder.Instance().Build();
        var repoRead = DiretorReadOnlyRepositoryBuilder.Instance().ExisteEmail(email).Build();
        var mapper = AutoMapperBuilder.Instance();
        var unit = UnitOfWorkBuilder.Instance().Build();
        var tokenController = TokenControllerBuilder.Instance();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();

        var useCase = new RegistrarDiretorUseCase(repoDiretorWrite, mapper, unit, tokenController, repoRead,encriptador);
        return useCase;
    }
}
