using FluentValidation;
using PortalEscolar.Communication.Request;
using PortalEscolar.Exceptions;
using System.Text.RegularExpressions;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public class RegistrarDiretorValidator : AbstractValidator<RequestRegistrarDiretorJson>
{
    public RegistrarDiretorValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_EMAIL_VAZIO);

        RuleFor(c => c.Senha)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_SENHA_VAZIA);       

        RuleFor(c => c.NomeCompleto)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_NOMECOMPLETO_VAZIO)
            .MaximumLength(200).WithMessage(ResourceMensagensDeErro.DIRETOR_NOMECOMPLETO_MAXIMO200CARACTERES);

        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.DataNascimento), () =>
        {
            RuleFor(c => c.DataNascimento).Custom((datanascimento, contexto) =>
            {
                string anoAtual = DateTime.UtcNow.Year.ToString();
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(datanascimento, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(datanascimento), ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_INVALIDO));
                }
            });
        });
            

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceMensagensDeErro.DIRETOR_EMAIL_INVALIDO);
        });
        When(c => !string.IsNullOrWhiteSpace(c.Senha), () =>
        {
            RuleFor(c => c.Senha).MinimumLength(6).WithMessage(ResourceMensagensDeErro.DIRETOR_SENHA_MINIMO_SEIS_CARACTERES);
        });
    }
}
