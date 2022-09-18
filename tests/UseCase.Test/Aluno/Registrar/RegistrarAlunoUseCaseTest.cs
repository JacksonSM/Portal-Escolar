using FluentAssertions;
using PortalEscolar.Application.UseCases.Aluno.Registrar;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilities.Criptografia;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
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
        result.Mensagem.Should().NotBeNullOrWhiteSpace();
        result.Mensagem.Should().Contain(ResourceMensagensDeErro.REGISTRAR_ALUNO_SUCESSO);
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
