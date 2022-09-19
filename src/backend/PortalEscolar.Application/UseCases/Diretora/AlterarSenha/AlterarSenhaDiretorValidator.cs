using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarSenha;
public class AlterarSenhaDiretorValidator : AbstractValidator<RequestAlterarSenhaUsuarioJson>
{
    public AlterarSenhaDiretorValidator()
    {
        RuleFor(c => c.SenhaNova).SetValidator(new SenhaValidator());
    }
}
