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
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_EMAIL_VAZIO)
            .EmailAddress().WithMessage(ResourceMensagensDeErro.DIRETOR_EMAIL_INVALIDO);

        RuleFor(c => c.Senha)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_SENHA_VAZIA)
            .MinimumLength(6).WithMessage(ResourceMensagensDeErro.DIRETOR_SENHA_MINIMO_SEIS_CARACTERES); 

        RuleFor(c => c.NomeCompleto)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_NOMECOMPLETO_VAZIO);

        RuleFor(c => c.DataNascimento)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_VAZIO)
            .Custom((datanascimento, contexto) =>
            {
                string anoAtual = DateTime.UtcNow.Year.ToString();
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(datanascimento, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(datanascimento), ResourceMensagensDeErro.DIRETOR_DATANASCIMENTO_INVALIDO));
                }
            });
    }
}
