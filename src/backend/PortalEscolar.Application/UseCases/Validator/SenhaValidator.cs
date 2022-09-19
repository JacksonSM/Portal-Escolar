using FluentValidation;
using PortalEscolar.Exceptions;

namespace PortalEscolar.Application.UseCases.Validator;

public class SenhaValidator : AbstractValidator<string>
{
    public SenhaValidator()
    {
        RuleFor(senha => senha).NotEmpty().WithMessage(ResourceMensagensDeErro.SENHA_VAZIA);
        When(senha => !string.IsNullOrWhiteSpace(senha), () =>
        {
            RuleFor(senha => senha.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMensagensDeErro.SENHA_MINIMO_SEIS_CARACTERES);
        });
    }
}
