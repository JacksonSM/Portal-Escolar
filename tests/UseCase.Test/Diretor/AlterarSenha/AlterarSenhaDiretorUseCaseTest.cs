using PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
using Utilities.Entities;
using Utilities.Repositories;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
using Utilities.Services.Criptografia;
using Utilities.Services.UsuarioLogado;
using FluentAssertions;
using Xunit;
using PortalEscolar.Application.Responses;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace UseCase.Test.Diretor.AlterarSenha;
public class AlterarSenhaDiretorUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValido_DeveRetornaMensagemSenhaAlteradaComSucesso()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();

        (var diretor,var _) = DiretorBuilder.Build(request.SenhaAtual);
        var useCase = UseCaseBuilder(diretor);

        var reponse = await useCase.ExecuteAsync(request);

        reponse.Should().NotBeNull();
        reponse.Mensagem.Should().Contain(ResourceRespostasUseCases.SENHA_ALTERADA_COM_SUCESSO);
    }
    [Fact]
    public async void SenhaAtual_SenhaAtualInvalida_DeveRetornaMensagemSenhaAtualInvalida()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();

        (var diretor, var _) = DiretorBuilder.Build();
        var useCase = UseCaseBuilder(diretor);

        Func<Task> action = async () => { await useCase.ExecuteAsync(request); };

        await action.Should().ThrowAsync<ErrosDeValidacaoException>()
            .Where(exception => exception.MensagensDeErro.Count == 1 &&
                exception.MensagensDeErro.Contains(ResourceMensagensDeErro.SENHAATUAL_INVALIDA));
    }

    private static AlterarSenhaDiretorUseCase UseCaseBuilder(PortalEscolar.Domain.Entities.Diretoria.Diretor diretor)
    {
        var repoDiretorWrite = DiretorWriteOnlyRepositoryBuilder.Instance().Build();
        var encriptador = EncriptadorDeSenhaBuilder.Instance();
        var usuarioLogado = UsuarioLogadoBuilder.Instance().RecuperarDiretor(diretor).Build();
        var unit = UnitOfWorkBuilder.Instance().Build();

        var useCase = new AlterarSenhaDiretorUseCase(repoDiretorWrite, encriptador, usuarioLogado, unit);
        return useCase;
    }

}