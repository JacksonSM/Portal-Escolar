using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request;

namespace PortalEscolar.Application.UseCases.Aluno.Registrar;
public class RegistrarAlunoValidator : AbstractValidator<RequestRegistrarAlunoJson>
{
    public RegistrarAlunoValidator()
    {
        RuleFor(x => x).SetValidator(new CamposComumValidator());
    }
}
