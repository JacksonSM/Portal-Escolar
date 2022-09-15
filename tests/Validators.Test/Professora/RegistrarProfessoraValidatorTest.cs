using FluentAssertions;
using PortalEscolar.Application.UseCases.Professora.Registrar;
using PortalEscolar.Exceptions;
using Utilities.Requests;
using Xunit;


namespace Validators.Test.Professora;
public class RegistrarProfessoraValidatorTest
{
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void CampoEmail_CampoEmBranco_ErroEmailVazio()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.Email = "";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_VAZIO));
    }
    [Fact]
    public void CampoEmail_EmailInvalido_ErroEmailInvalido()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.Email = "errogmail.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.EMAIL_INVALIDO));
    }
    [Fact]
    public void CampoSenha_SenhaVazia_ErroSenhaVazia()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.Senha = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_VAZIA));
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void CampoSenha_SenhaComMenosDe6Caracteres_ErroSenhaInvalida(int tamanhoSenha)
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build(tamanhoSenha);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.SENHA_MINIMO_SEIS_CARACTERES));
    }
    [Fact]
    public void CampoNomeCompleto_NomeCompletoVazio_ErroNomeCompletoVazio()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.NomeCompleto = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO));
    }
    [Fact]
    public void CampoNomeCompleto_NomeCompletoComMaisde200Caracteres_ErroNomeCompletoNoMaximo200Caracteres()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.NomeCompleto = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.NOMECOMPLETO_MAXIMO200CARACTERES));
    }
    [Fact]
    public void CampoDataNascimento_DataNascimentoVazio_DataNascimentoVazio()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.DataNascimento = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DATANASCIMENTO_VAZIO));
    }
    [Fact]
    public void CampoDataNascimento_DataNascimentoInvalido_DataNascimentoInvalido()
    {
        var validator = new RegistrarProfessoraValidator();

        var request = RequestRegistrarProfessoraBuilder.Build();
        request.DataNascimento = "23/05/200";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DATANASCIMENTO_INVALIDO));
    }
}
