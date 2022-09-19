using FluentAssertions;
using PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
using PortalEscolar.Exceptions;
using Utilities.Requests;
using Xunit;

namespace Validators.Test.Diretor;
public class AlterarInfoPessoalDiretorValidatorTest
{
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void NomeCompleto_NomeCompletoVazio_ErroNomeCompletoVazio()
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();
        request.NomeCompleto = string.Empty;

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO));
    }
    [Fact]
    public void NomeCompleto_NomeCompletoComMaisDe200Caracteres_ErroNomeCompletoNumeroMaximoCaracteres()
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();
        request.NomeCompleto = "ssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" +
            "sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" +
            "sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss" +
            "sssssssssssssssssss";

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.NOMECOMPLETO_MAXIMO200CARACTERES));
    }
    [Fact]
    public void DataNascimento_DataNascimentoVazio_ErroDataNascimentoVazio()
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();
        request.DataNascimento = string.Empty;

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DATANASCIMENTO_VAZIO));
    }
    [Fact]
    public void DataNascimento_DataNascimentoInvalido_ErroDataNascimentoInvalido()
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var request = RequestAlterarInfoPessoalDiretorBuilder.Build();
        request.DataNascimento = "43/01/2000";

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DATANASCIMENTO_INVALIDO));
    }
}
