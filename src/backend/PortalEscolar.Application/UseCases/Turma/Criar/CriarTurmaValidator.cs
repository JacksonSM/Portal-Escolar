using FluentValidation;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;

namespace PortalEscolar.Application.UseCases.Turma.Criar;
public class CriarTurmaValidator : AbstractValidator<RequestCriarTurmaJson>
{
    public CriarTurmaValidator()
    {
        RuleFor(c => c.Sala).NotEmpty().WithMessage(ResourceMensagensDeErro.TURMA_SALA_VAZIA);
        RuleFor(c => c.NomeTurma).NotEmpty().WithMessage(ResourceMensagensDeErro.TURMA_NOME_TURMA_VAZIO);
        RuleFor(c => c.Serie).IsInEnum();
        RuleFor(c => c.Turno).IsInEnum();

        When(c => !string.IsNullOrWhiteSpace(c.Sala), () =>
        {
            RuleFor(c => c.Sala).MaximumLength(50).WithMessage(ResourceMensagensDeErro.TURMA_SALA_NOMAXIMO50CARACTERES);
        });
        When(c => !string.IsNullOrWhiteSpace(c.NomeTurma), () =>
        {
            RuleFor(c => c.NomeTurma).MaximumLength(50).WithMessage(ResourceMensagensDeErro.TURMA_NOME_TURMA_NOMAXIMO50CARACTERES);
        });
    }
}