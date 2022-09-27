using FluentValidation;
using PortalEscolar.Communication.Request.Exercicio;
using PortalEscolar.Exceptions;
using System.Text.RegularExpressions;

namespace PortalEscolar.Application.UseCases.Professora.EnviarExercicio;
public class EnviarExercicioValidador : AbstractValidator<RequestEnviarExercicioJson>
{
    public EnviarExercicioValidador()
    {
        RuleFor(c => c.PrazoEntrega)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_PRAZOENTREGA_VAZIO);

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_NOME_VAZIO)
            .MaximumLength(200)
            .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_NOME_NOMAXIMO200CARACTERES);

        When(c => !string.IsNullOrWhiteSpace(c.PrazoEntrega), () =>
        {
            RuleFor(c => c.PrazoEntrega).Custom((datanascimento, contexto) =>
            {
                string anoAtual = DateTime.UtcNow.Year.ToString();
                string padraoData = @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4} (2[0-3]|[01]?[0-9]):([0-5]?[0-9])$";

                var isMatch = Regex.IsMatch(datanascimento, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(datanascimento), ResourceMensagensDeErro.ENVIAR_EXERCICIO_DATAENTREGA_INVALIDO));
                }
            });
        });

        RuleFor(c => c.Disciplina)
            .IsInEnum()
            .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_DISCIPLINA_INEXISTENTE);

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
                .Must(altenativa => altenativa.Count() >= 2)
                .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ALTERNATIVAS_INVALIDO);

            questoes.RuleFor(c => c.AlternativaCorreta)
                .NotEmpty()
                .WithMessage(ResourceMensagensDeErro.ENVIAR_EXERCICIO_QUESTAO_ALTERNATIVACORRETA_VAZIO);
        });

    }
}
