using FluentAssertions;
using PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;
using Utilities.Entities;
using Utilities.Repositories;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
using Utilities.Services.Mapper;
using Utilities.Services.UsuarioLogado;
using Xunit;

namespace UseCase.Test.Diretor.AlterarInfoPessoal;
public class AlterarInfoPessoalDiretorUseCaseTest
{

    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaCamposPreenchido()
    {
        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();

        (var diretor, var _) = DiretorBuilder.Build();
        var useCase = UseCaseBuilder(diretor);

        var reponse = await useCase.ExecuteAsync(request);

        reponse.Should().NotBeNull();
        reponse.Email.Should().NotBeNullOrWhiteSpace();
        reponse.DataNascimento.Should().NotBeNullOrWhiteSpace();
        reponse.NomeCompleto.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async void NomeCompleto_NomeCompletoVazio_DeveRetornaMensagemErroNomeCompletoVazio()
    {
        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();
        request.NomeCompleto = string.Empty;

        (var diretor, var _) = DiretorBuilder.Build();
        var useCase = UseCaseBuilder(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO));
    }

    private static AlterarInfoPessoalDiretorUseCase UseCaseBuilder(PortalEscolar.Domain.Entities.Diretoria.Diretor diretor)
    {
        var repoDiretorWrite = DiretorWriteOnlyRepositoryBuilder.Instance().Build();
        var usuarioLogado = UsuarioLogadoBuilder.Instance().RecuperarDiretor(diretor).Build();
        var unit = UnitOfWorkBuilder.Instance().Build();
        var mapper = AutoMapperBuilder.Instance();

        var useCase = new AlterarInfoPessoalDiretorUseCase(repoDiretorWrite, usuarioLogado, unit,mapper);
        return useCase;
    }
}
