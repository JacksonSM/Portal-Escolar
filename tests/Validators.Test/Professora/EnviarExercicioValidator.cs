using FluentAssertions;
using PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
using PortalEscolar.Domain.Enum;
using PortalEscolar.Exceptions;
using Utilities.Requests;
using Xunit;

namespace Validators.Test.Professora;
public class EnviarExercicioValidator
{
    [Fact]
    public void Objeto_ParametrosValidos_ObjetoValido()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void PrazoEntrega_CampoVazio_ErroCampoVazio()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.PrazoEntrega = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_PRAZOENTREGA_VAZIO));
    }
    [Fact]
    public void PrazoEntrega_CampoInvalido_ErroCampoInvalido()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.PrazoEntrega = "12/366/2009 15:69";

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_DATAENTREGA_INVALIDO));
    }
    [Fact]
    public void PrazoEntrega_CampoComDadoValido_CampoValido()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void Nome_CampoVazio_ErroCampoVazio()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.Nome = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_NOME_VAZIO));
    }
    [Fact]
    public void Nome_CampoComMais200Caracteres_ErroComMais200Caracteres()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.Nome = string.Join("" ,Enumerable.Repeat("a", 203)); 

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_NOME_NOMAXIMO200CARACTERES));
    }
    [Fact]
    public void Disciplina_CampoInvalido_ErroCampoInvalido()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.Disciplina =(Disciplina) 9999;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_DISCIPLINA_INEXISTENTE));
    }
    [Fact]
    public void Questoes_QuestoesComOrdemRepetidas_ErroQuestoesComOrdemRepetidas()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();

        request.Questoes[0].Ordem = 1;
        request.Questoes[1].Ordem = 1;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ORDEM_REPETIDAS));
    }
    [Fact]
    public void Enunciado_CampoVazio_ErroCampoVazio()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();
        request.Questoes[0].Enunciado = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ENUNCIADO_VAZIO));
    }
    [Fact]
    public void Alternativas_CampoComMenos2Alternativas_ErroCampoComMenos2Alternativas()
    {
        var validator = new EnviarExercicioValidador();

        var request = RequestEnviarExercicioJsonBuilder.Build();

        request.Questoes[0].Alternativas.RemoveRange(1, request.Questoes[0].Alternativas.Count - 1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle().And
            .Contain(erros => erros.ErrorMessage.Equals(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ALTERNATIVAS_INVALIDO));
    }
}
