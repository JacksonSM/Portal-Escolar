using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public class RegistrarDiretorValidator : AbstractValidator<RequestRegistrarDiretorJson>
{
    public RegistrarDiretorValidator()
    {
        RuleFor(x => x).SetValidator(new CamposComumValidator());
    }
}
