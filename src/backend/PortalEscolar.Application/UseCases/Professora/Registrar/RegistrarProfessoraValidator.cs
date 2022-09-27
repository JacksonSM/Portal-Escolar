using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request.Professora;

namespace PortalEscolar.Application.UseCases.Professora.Registrar;
public class RegistrarProfessoraValidator : AbstractValidator<RequestRegistrarProfessoraJson>
{
    public RegistrarProfessoraValidator()
    {
        RuleFor(x => x).SetValidator(new CamposComumValidator());
    }
}
