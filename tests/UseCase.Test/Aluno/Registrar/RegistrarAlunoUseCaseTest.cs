using FluentAssertions;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Repositories;
using Utilities.Repositories.Aluno;
using Utilities.Requests;
using Utilities.Services.Criptografia;
using Utilities.Services.Mapper;
using Xunit;

namespace UseCase.Test.Aluno.Registrar;
public class RegistrarAlunoUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaGenericResponseJsonComMensagemDeSucesso()
    {
        var request = RequestRegistrarAlunoBuilder.Build();
        var useCase = BuildUseCase();
        var result = await useCase.ExecuteAsync(request);

        result.Should().NotBeNull();
        result.NomeCompleto.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async void Email_EmailExistente_DeveRetornaMensagemComErroDeEmailExistente()
    {
        var request = RequestRegistrarAlunoBuilder.Build();
        var useCase = BuildUseCase(request.Email);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.EMAIL_EXISTENTE));
    }



    private RegistrarAlunoUseCase BuildUseCase(string email = "")
    {
        var repoAlunoWrite = AlunoWriteOnlyRepositoryBuilder.Instance().Build();
        var repoAlunoRead = AlunoReadOnlyRepositoryBuilder.Instance().ExisteEmail(email).Build();
        var mapper = AutoMapperBuilder.Instance();
        var unit = UnitOfWorkBuilder.Instance().Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();

        var useCase = new RegistrarAlunoUseCase(repoAlunoRead, repoAlunoWrite, mapper, encriptador, unit);
        return useCase;
    }
}
