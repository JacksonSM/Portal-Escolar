using FluentValidation;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;

namespace PortalEscolar.Application.UseCases.Turma.Criar;
public class CriarTurmaValidator : AbstractValidator<RequestCriarTurmaJson>
{
    public CriarTurmaValidator()
    {
        RuleFor(c => c.Sala).NotEmpty().WithMessage(ResourceMensagensDeErro.TURMA_SALA_VAZIA)
            .MaximumLength(50).WithMessage(ResourceMensagensDeErro.TURMA_SALA_NOMAXIMO50CARACTERES);

        RuleFor(c => c.NomeTurma).NotEmpty().WithMessage(ResourceMensagensDeErro.TURMA_NOME_TURMA_VAZIO)
            .MaximumLength(50).WithMessage(ResourceMensagensDeErro.TURMA_NOME_TURMA_NOMAXIMO50CARACTERES);

        RuleFor(c => c.Serie).IsInEnum().WithMessage(ResourceMensagensDeErro.SERIE_INEXISTENTE);
        RuleFor(c => c.Turno).IsInEnum().WithMessage(ResourceMensagensDeErro.TURNO_INEXISTENTE);
    }
}