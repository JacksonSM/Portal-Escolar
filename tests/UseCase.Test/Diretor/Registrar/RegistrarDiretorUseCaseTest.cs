using PortalEscolar.Application.UseCases.Diretora.Registrar;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
using Utilities.Services.Token;
using Xunit;
using FluentAssertions;
using PortalEscolar.Exceptions.ExceptionsBase;
using PortalEscolar.Exceptions;
using Utilities.Criptografia;

namespace UseCase.Test.Diretor.Registrar;
public class RegistrarDiretorUseCaseTest
{
    [Fact]
    public async void UseCase_ParametrosValidos_EsperadoResponseTokenJsonComTokenENome()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.ExecutarAsync(request);

        result.Should().NotBeNull();    
        result.Token.Should().NotBeNullOrWhiteSpace();
        result.Nome.Should().NotBeNullOrWhiteSpace();
    }
    [Fact]
    public async void ValidacaoEmailExistente_MetodoExistEmailRetornaTrue_ELancadaExceptionEmailExistente()
    {
        var request = RequestRegistrarDiretorBuilder.Build();
        var useCase = CreateUseCase(request.Email);

        Func<Task> action = async () => { await useCase.ExecutarAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.USUARIO_EMAIL_EXISTENTE));
    }



    private RegistrarDiretorUseCase CreateUseCase(string email = "")
    {
        var repoWrite = DiretorWriteOnlyRepositoryBuilder.Instance().Build();
        var repoRead = DiretorReadOnlyRepositoryBuilder.Instance().ExisteEmail(email).Build();
        var mapper = AutoMapperBuilder.Instance();
        var unit = UnitOfWorkBuilder.Instance().Build();
        var tokenController = TokenControllerBuilder.Instance();
        var encriptador = EncriptadorDeSenhaBuilder.Instancia();

        var useCase = new RegistrarDiretorUseCase(repoWrite, mapper, unit, tokenController, repoRead,encriptador);
        return useCase;
    }
}
