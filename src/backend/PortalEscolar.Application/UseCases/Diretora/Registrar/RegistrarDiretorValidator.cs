using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request.Diretor;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public class RegistrarDiretorValidator : AbstractValidator<RequestRegistrarDiretorJson>
{
    public RegistrarDiretorValidator()
    {
        RuleFor(x => x).SetValidator(new CamposComumValidator());
    }
}
