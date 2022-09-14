using PortalEscolar.Application.UseCases.Diretora.Registrar;
using Utilities.Requests;
using FluentAssertions;
using Xunit;
using PortalEscolar.Exceptions;

namespace Validators.Test.Diretor.Registrar;
public class RegistrarDiretorValidatorTest
{
    //Oque esta sendo testado_Descricao do cenario de teste_O resultado esperado
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void CampoEmail_CampoEmBranco_ErroEmailVazio()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.Email = "";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_EMAIL_VAZIO));
    }
    [Fact]
    public void CampoEmail_EmailInvalido_ErroEmailInvalido()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.Email = "errogmail.com";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_EMAIL_INVALIDO));
    }
    [Fact]
    public void CampoSenha_SenhaVazia_ErroSenhaVazia()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.Senha = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_SENHA_VAZIA));
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void CampoSenha_SenhaComMenosDe6Caracteres_ErroSenhaInvalida(int tamanhoSenha)
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build(tamanhoSenha);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_SENHA_MINIMO_SEIS_CARACTERES));
    }
    [Fact]
    public void CampoNomeCompleto_NomeCompletoVazio_ErroNomeCompletoVazio()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.NomeCompleto = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_NOMECOMPLETO_VAZIO));
    }
    [Fact]
    public void CampoNomeCompleto_NomeCompletoComMaisde200Caracteres_ErroNomeCompletoNoMaximo200Caracteres()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.NomeCompleto = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_NOMECOMPLETO_MAXIMO200CARACTERES));
    }
    [Fact]
    public void CampoDataNascimento_DataNascimentoVazio_DataNascimentoVazio()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.DataNascimento = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_VAZIO));
    }
    [Fact]
    public void CampoDataNascimento_DataNascimentoInvalido_DataNascimentoInvalido()
    {
        var validator = new RegistrarDiretorValidator();

        var request = RequestRegistrarDiretorBuilder.Build();
        request.DataNascimento = "23/05/200";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_INVALIDO));
    }
}
