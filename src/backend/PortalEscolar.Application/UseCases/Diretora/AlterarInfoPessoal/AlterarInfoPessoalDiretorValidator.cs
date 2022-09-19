using FluentValidation;
using PortalEscolar.Application.UseCases.Validator;
using PortalEscolar.Communication.Request;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
public class AlterarInfoPessoalDiretorValidator : AbstractValidator<RequestAlterarInfoPessoalDiretorJson>
{
    public AlterarInfoPessoalDiretorValidator()
    {
        RuleFor(c => c).SetValidator(new InformacoesPessoaisValidator());
    }
}