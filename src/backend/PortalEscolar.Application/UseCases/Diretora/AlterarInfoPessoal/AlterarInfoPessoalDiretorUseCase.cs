using AutoMapper;
using PortalEscolar.Application.Services.UsuarioLogado;
using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Exceptions.ExceptionsBase;
using System.Globalization;

namespace PortalEscolar.Application.UseCases.Diretora.AlterarInfoPessoal;
public class AlterarInfoPessoalDiretorUseCase : IAlterarInfoPessoalDiretorUseCase
{
    private readonly IDiretorWriteOnlyRepository _repoDiretorWrite;
    private readonly IUsuarioLogado _usuarioLogado;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AlterarInfoPessoalDiretorUseCase(IDiretorWriteOnlyRepository repoDiretorWrite,
        IUsuarioLogado usuarioLogado,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repoDiretorWrite = repoDiretorWrite;
        _usuarioLogado = usuarioLogado;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseInfoPessoalDiretorJson> ExecuteAsync(RequestAlterarInfoPessoalDiretorJson request)
    {
        Validar(request);

        var diretorLogado = await _usuarioLogado.ObterDiretor();

        diretorLogado.NomeCompleto = request.NomeCompleto; 
        diretorLogado.DataNascimento = DateTime.ParseExact(request.DataNascimento,"dd/MM/yyyy", CultureInfo.InvariantCulture);

        _repoDiretorWrite.Atualizar(diretorLogado);
        await _unitOfWork.CommitAsync();
               
        var response = _mapper.Map<ResponseInfoPessoalDiretorJson>(diretorLogado);
        return response;
    }

    private void Validar(RequestAlterarInfoPessoalDiretorJson request)
    {
        var validator = new AlterarInfoPessoalDiretorValidator();

        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(c => c.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(erros);
        }
    }
}
