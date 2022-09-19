using PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
using Utilities.Requests;
using FluentAssertions;
using Xunit;
using PortalEscolar.Exceptions;

namespace Validators.Test.Diretor;
public class AlterarSenhaDiretorValidatorTest
{
    [Fact]
    public void Validator_DadosValidos_DeveEstaValido()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();

        var validator = new AlterarSenhaDiretorValidator();

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeTrue();
    }
    [Fact]
    public void Senha_SenhaNovaVazia_DeveRetornaErroSenhaVazia()
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build();
        request.SenhaNova = string.Empty;

        var validator = new AlterarSenhaDiretorValidator();

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_VAZIA));
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Senha_SenhaNovaComMenos6Caracteres_DeveRetornaErroSenhaNoMinimo6Caracteres(int tamanhoSenhaNova)
    {
        var request = RequestAlterarSenhaUsuarioBuilder.Build(tamanhoSenhaNova);

        var validator = new AlterarSenhaDiretorValidator();

        var validationResult = validator.Validate(request);

        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_MINIMO_SEIS_CARACTERES));
    }
}
