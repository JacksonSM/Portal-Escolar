using FluentValidation;
using PortalEscolar.Communication.Request.Matricula;
using PortalEscolar.Exceptions;
using System.Text.RegularExpressions;

namespace PortalEscolar.Application.UseCases.Diretora.Matricular;
public class MatricularAlunoValidator : AbstractValidator<RequestMatricularAlunoJson>
{
    public MatricularAlunoValidator()
    {
        RuleFor(c => c.CidadeNascimentoAluno)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_ALUNO_VAZIO)
            .MaximumLength(150)
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_ALUNO_NOMAXIMO150CARACTERES);

        RuleFor(c => c.Responsavel.NomeCompleto)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.MATRICULA_NOMECOMPLETO_RESPONSAVEL_VAZIO)
            .MaximumLength(200)
            .WithMessage(ResourceMensagensDeErro.MATRICULA_NOMECOMPLETO_RESPONSAVEL_NOMAXIMO200CARACTERES);

        RuleFor(c => c.Responsavel.Cidade)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_RESPONSAVEL_VAZIO)
            .MaximumLength(100)
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CIDADENASCIMENTO_RESPONSAVEL_NOMAXIMO100CARACTERES);


        ValidarCampoCPF();
        ValidarDataInicio();
        ValidarDataTerminio();
        ValidarDataNascimentoResponsavel();
        ValidarTelefoneResponsavel();
    }

    private void ValidarCampoCPF()
    {
        RuleFor(c => c.Responsavel.CPF)
            .NotEmpty()
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_VAZIO);



        When(c => !string.IsNullOrWhiteSpace(c.Responsavel.CPF), () =>
        {
            RuleFor(c => c.Responsavel.CPF).Matches("[0-9]{11}")
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_SOMENTE11NUMEROS);
        });
        When(c => !string.IsNullOrWhiteSpace(c.Responsavel.CPF), () =>
        {
            RuleFor(c => c.Responsavel.CPF).Length(11)
            .WithMessage(ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_SOMENTE11NUMEROS);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Responsavel.CPF), () =>
        {
            RuleFor(c => c.Responsavel.CPF).Custom((cpf, contexto) =>
            {
                var isValido = ValidarCPF(cpf);

                if (!isValido)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure
                        (nameof(cpf), ResourceMensagensDeErro.MATRICULA_CPF_RESPONSAVEL_INVALIDO));
                }
            });
        });
    }
    private static bool ValidarCPF(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;

        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }
    private void ValidarDataTerminio()
    {
        RuleFor(c => c.DataTerminio)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.MATRICULA_DATA_TERMINIO_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.DataTerminio), () =>
        {
            RuleFor(c => c.DataTerminio).Custom((dataTerminio, contexto) =>
            {
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(dataTerminio, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(dataTerminio), ResourceMensagensDeErro.MATRICULA_DATA_TERMINIO_INVALIDO));
                }
            });
        });
    }
    private void ValidarDataInicio()
    {
        RuleFor(c => c.DataInicio)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.MATRICULA_DATA_INICIO_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.DataInicio), () =>
        {
            RuleFor(c => c.DataInicio).Custom((dataInicio, contexto) =>
            {
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(dataInicio, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(dataInicio), ResourceMensagensDeErro.MATRICULA_DATA_INICIO_INVALIDO));
                }
            });
        });
    }
    private void ValidarDataNascimentoResponsavel()
    {
        RuleFor(c => c.Responsavel.DataNascimento)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.MATRICULA_DATA_NASCIMENTO_RESPONSAVEL_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.Responsavel.DataNascimento), () =>
        {
            RuleFor(c => c.Responsavel.DataNascimento).Custom((dataNascimento, contexto) =>
            {
                string padraoData = "^(?:[012]?[0-9]|3[01])[./-](?:0?[1-9]|1[0-2])[./-](?:[0-9]{2}){1,2}$";

                var isMatch = Regex.IsMatch(dataNascimento, padraoData);

                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(dataNascimento), ResourceMensagensDeErro.MATRICULA_DATA_NASCIMENTO_RESPONSAVEL_INVALIDO));
                }
            });
        });
    }
    private void ValidarTelefoneResponsavel()
    {
        RuleFor(c => c.Responsavel.Telefone)
            .NotEmpty().WithMessage(ResourceMensagensDeErro.MATRICULA_TELEFONE_RESPONSAVEL_VAZIO);

        When(c => !string.IsNullOrWhiteSpace(c.Responsavel.Telefone), () =>
        {
            RuleFor(c => c.Responsavel.Telefone).Custom((telefone, contexto) =>
            {
                string padraoTelefone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(telefone, padraoTelefone);
                if (!isMatch)
                {
                    contexto.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(telefone), ResourceMensagensDeErro.MATRICULA_TELEFONE_RESPONSAVEL_INVALIDO));
                }
            });
        });
    }

}
