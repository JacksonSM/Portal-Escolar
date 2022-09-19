using FluentValidation;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;
using System.Text.RegularExpressions;

namespace PortalEscolar.Application.UseCases.Validator;
public class CamposComumValidator : AbstractValidator<RequestCamposComum>
{
    public CamposComumValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.EMAIL_VAZIO);

        RuleFor(c => c.Senha).SetValidator(new SenhaValidator());

        RuleFor(c => c.NomeCompleto)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.NOMECOMPLETO_VAZIO)
            .MaximumLength(200).WithMessage(ResourceMensagensDeErro.NOMECOMPLETO_MAXIMO200CARACTERES);

        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DATANASCIMENTO_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.DataNascimento), () =>
        {
            RuleFor(c => c.DataNascimento).Custom((datanascimento, contexto) =>
            {
                string anoAtual = DateTime.UtcNow.Year.ToString();
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(datanascimento, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(datanascimento), ResourceMensagensDeErro.DATANASCIMENTO_INVALIDO));
                }
            });
        });
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.EMAIL_INVALIDO);
        });
    }
}
