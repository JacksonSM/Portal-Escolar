using FluentAssertions;
using PortalEscolar.Application.Responses;
using PortalEscolar.Application.UseCases.Turma.Criar;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Repositories;
using Utilities.Repositories.Professora;
using Utilities.Repositories.Turma;
using Utilities.Requests;
using Utilities.Services.Mapper;
using Xunit;

namespace UseCase.Test.Turma.Criar;
public class CriarTurmaUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaGenericResponseJsonComMensagemDeSucesso()
    {
        var request = RequestCriarTurmaJsonBuilder.Build();
        var useCase = BuildUseCase(request.ProfessoraId);
        var result = await useCase.ExecuteAsync(request);

        result.Should().NotBeNull();
        result.Mensagem.Should().NotBeNullOrWhiteSpace();
        result.Mensagem.Should().Contain(ResourceRespostasUseCases.TURMA_CRIADA_COM_SUCESSO);
    }
    [Fact]
    public async void ProfessoraId_IdInexistente_DeveLancarExceptionProfessoraInexistente()
    {
        var request = RequestCriarTurmaJsonBuilder.Build();
        var useCase = BuildUseCase();

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(erro => erro.MensagensDeErro.Count == 1 && 
                erro.MensagensDeErro.Contains(ResourceMensagensDeErro.PROFESSORA_NAO_ENCONTRADA));
    }

    private CriarTurmaUseCase BuildUseCase(long id = 0)
    {
        var turmaWriteRepo = TurmaWriteOnlyRepositoryBuilder.Instance().Build();
        var professoraReadRepo = ProfessoraReadOnlyRepositoryBuilder.Instance().ExisteProfessora(id).Build();
        var mapper = AutoMapperBuilder.Instance();
        var unit = UnitOfWorkBuilder.Instance().Build();


        var useCase = new CriarTurmaUseCase(turmaWriteRepo,unit,mapper,professoraReadRepo);
        return useCase;
    }
}
