using PortalEscolar.Application.UseCases.Professora.Registrar;
using Utilitario.ParaOsTestes.Mapper;
using Utilitario.ParaOsTestes.Repositorios;
using Utilities.Criptografia;
using Utilities.Repositories.Diretor;
using Utilities.Requests;
using Xunit;
using FluentAssertions;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace UseCase.Test.Professora.Registrar;
public class RegistrarProfessoraUseCaseTest
{
    [Fact]
    public async void UseCase_DadosValidos_DeveRetornaGenericResponseJsonComMensagemDeSucesso()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();
        var useCase = BuildUseCase();
        var result = await useCase.ExecuteAysnc(request);

        result.Should().NotBeNull();
        result.Mensagem.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async void Email_EmailExistente_DeveRetornaMensagemComErroDeEmailExistente()
    {
        var request = RequestRegistrarProfessoraBuilder.Build();
        var useCase = BuildUseCase(request.Email);

        Func<Task> action = async () => { await useCase.ExecuteAysnc(request); };

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

        return new RegistrarProfessoraUseCase(repoRead, repoWrite, mapper, encriptador, unit);
    }
}


//private readonly IProfessoraReadOnlyRepository _professoraRead;
//private readonly IProfessoraWriteOnlyRepository _professoraWrite;
//private readonly IMapper _mapper;
//private readonly EncriptadorDeSenha _encriptadorDeSenha;
//private readonly IUnitOfWork _unitOfWork;