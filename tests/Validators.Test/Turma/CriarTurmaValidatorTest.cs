using FluentAssertions;
using PortalEscolar.Application.UseCases.Turma.Criar;
using PortalEscolar.Domain.Enum;
using PortalEscolar.Exceptions;
using Utilities.Requests;
using Xunit;

namespace Validators.Test.Turma;
public class CriarTurmaValidatorTest
{
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void CampoSala_SalaVazia_ErroSalaVazia()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.Sala = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.TURMA_SALA_VAZIA));
    }
    [Fact]
    public void CampoSala_SalaComMais50Caracteres_ErroSalaNoMaximo50Caracteres()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.Sala = "gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
            "gggggggggggggggggggggtttttttttttttttttttttttttttttttttttttttttttttttttggggggggggggggggg";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.TURMA_SALA_NOMAXIMO50CARACTERES));
    }
    [Fact]
    public void CampoNomeTurma_NomeTurmaVazia_ErroNomeTurmaVazia()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.NomeTurma = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.TURMA_NOME_TURMA_VAZIO));
    }
    [Fact]
    public void CampoNomeTurma_NomeTurmaComMais50Caracteres_ErroNomeTurmaNoMaximo50Caracteres()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.NomeTurma = "gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
            "gggggggggggggggggggggtttttttttttttttttttttttttttttttttttttttttttttttttggggggggggggggggg";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.TURMA_NOME_TURMA_NOMAXIMO50CARACTERES));
    }
    [Fact]
    public void CampoSerie_AtribuirUmaSerieInexistente_ErroSerieInexistente()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.Serie = (Serie)11111111;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.SERIE_INEXISTENTE));
    }
    [Fact]
    public void CampoTurno_AtribuirUmTurnoInexistente_ErroTurnoInexistente()
    {
        var validator = new CriarTurmaValidator();

        var request = RequestCriarTurmaJsonBuilder.Build();
        request.Turno = (Turno)222222;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .And.Contain(x => x.ErrorMessage.Equals(ResourceMensagensDeErro.TURNO_INEXISTENTE));
    }
}
