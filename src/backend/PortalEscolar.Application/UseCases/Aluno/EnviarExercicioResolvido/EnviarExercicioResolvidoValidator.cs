using FluentValidation;
using PortalEscolar.Communication.Request.Aluno.EnviarExercicioResolvido;
using PortalEscolar.Exceptions;
using System.Text.RegularExpressions;

namespace PortalEscolar.Application.UseCases.Aluno.EnviarExercicioResolvido;
public class EnviarExercicioResolvidoValidator : AbstractValidator<RequestEnviarExercicioResolvidoJson>
{
    public EnviarExercicioResolvidoValidator()
    {
        RuleFor(c => c.ExercicioParaResolverId)
            .Length(24)
            .WithMessage(ResourceMensagensDeErro.ALUNO_ENVIAR_EXERCICIORESOLVIDO_EXERCICIOID_INVALIDO);

        RuleFor(c => c.Questoes).Custom((questoes, contexto) =>
        {
            var existeRepetida = questoes.GroupBy(c => c.Ordem).Any(g => g.Count() > 1);

            if (existeRepetida)
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(questoes), ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ORDEM_REPETIDAS));
            }
        });


        RuleForEach(c => c.Questoes).ChildRules(questoes =>
        {
            questoes.RuleFor(c => c.Enunciado)
                .NotEmpty()
                .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ENUNCIADO_VAZIO);

            questoes.RuleFor(c => c.Alternativas)
                .Must(altenativa => altenativa.Count >= 2)
                .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ALTERNATIVAS_INVALIDO);
        });
    }
}
