using FluentValidation;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;

namespace PortalEscolar.Application.UseCases.Validator;
public class CamposComumValidator : AbstractValidator<RequestCamposComum>
{
    public CamposComumValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_VAZIO);

        RuleFor(c => c.Senha).SetValidator(new SenhaValidator());

        RuleFor(x => x).SetValidator(new InformacoesPessoaisValidator());


        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_INVALIDO);
        });
    }
}
