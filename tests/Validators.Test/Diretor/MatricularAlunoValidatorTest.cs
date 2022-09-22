using FluentAssertions;
using PortalEscolar.Application.UseCases.Diretora.Matricular;
using PortalEscolar.Exceptions;
using Utilities.Requests;
using Xunit;

namespace Validators.Test.Diretor;
public class MatricularAlunoValidatorTest
{
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void CidadeNascimentoAluno_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.CidadeNascimentoAluno = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_ALUNO_VAZIO));
    }
    [Fact]
    public void CidadeNascimentoAluno_CampoComMais150Caracteres_ErroCampoComMaisDoPermitido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.CidadeNascimentoAluno = "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
            "gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
            "jkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkf";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_ALUNO_NOMAXIMO150CARACTERES));
    }
    [Fact]
    public void NomeCompletoResponsavel_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.NomeCompleto = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.MATRICULA_NOMECOMPLETO_RESPONSAVEL_VAZIO));
    }
    [Fact]
    public void NomeCompletoResponsavel_CampoComMais200Caracteres_ErroCampoComMaisDoPermitido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.NomeCompleto = "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
            "gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
            "jkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkf";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_NOMECOMPLETO_RESPONSAVEL_NOMAXIMO200CARACTERES));
    }
    [Fact]
    public void CidadeResponsavel_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.Cidade = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_RESPONSAVEL_VAZIO));
    }
    [Fact]
    public void CidadeResponsavel_CampoComMais100Caracteres_ErroCampoComMaisDoPermitido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.Cidade = "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
            "gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
            "jkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkddddddddddddddddkkkkkkkkkkkkkkf";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_RESPONSAVEL_NOMAXIMO100CARACTERES));
    }
    [Fact]
    public void CPFResponsavel_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.CPF = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_VAZIO));
    }
    [Fact]
    public void CPFResponsavel_CampoCom15Caracteres_ErroCampoComMaisDoPermitido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.CPF = "12222222222222";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_INVALIDO)).And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_SOMENTE11NUMEROS));
    }
    [Fact]
    public void CPFResponsavel_CampoCom5Caracteres_ErroCampoComMenosDoPermitido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.CPF = "12345";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_INVALIDO)).And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_SOMENTE11NUMEROS));
    }
    [Fact]
    public void CPFResponsavel_CampoComLetras_ErroCampoSomenteNumeros()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.CPF = "5637634106b";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_INVALIDO)).And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_SOMENTE11NUMEROS));
    }
    [Fact]
    public void CPFResponsavel_CampoComCPFInvalido_ErroCPFInvalido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.CPF = "22122334455";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_INVALIDO));
    }
    [Fact]
    public void DataNascimentoRespondavel_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.DataNascimento = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_NASCIMENTO_RESPONSAVEL_VAZIO));
    }
    [Fact]
    public void DataNascimentoRespondavel_CampoInvalido_ErroCampoInvalido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.Responsavel.DataNascimento = "8908/2000";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_NASCIMENTO_RESPONSAVEL_INVALIDO));
    }
    [Fact]
    public void DataInicio_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.DataInicio = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_INICIO_VAZIO));
    }
    [Fact]
    public void DataInicio_CampoInvalido_ErroCampoInvalido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.DataInicio = "1206/2000";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_INICIO_INVALIDO));
    }
    [Fact]
    public void DataTerminio_CampoVazio_ErroCampoVazio()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.DataTerminio = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_TERMINIO_VAZIO));
    }
    [Fact]
    public void DataTerminio_CampoInvalido_ErroCampoInvalido()
    {
        var validator = new MatricularAlunoValidator();

        var request = RequestMatricularAlunoBuilder.Build();
        request.DataTerminio = "12/58/2000";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And
            .Contain(erros => erros.ErrorMessage
            .Equals(ResourceMensagensDeErro.MATRICULA_DATA_TERMINIO_INVALIDO));
    }
}
