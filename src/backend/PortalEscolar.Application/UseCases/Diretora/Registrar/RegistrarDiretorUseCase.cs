﻿using AutoMapper;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Communication.Request;
using PortalEscolar.Communication.Response;
using PortalEscolar.Domain.Interfaces.Repositories;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Exceptions;
using PortalEscolar.Exceptions.ExceptionsBase;

namespace PortalEscolar.Application.UseCases.Diretora.Registrar;
public class RegistrarDiretorUseCase : IRegistrarDiretorUseCase
{
    private readonly IDiretorWriteOnlyRepository _repoWriteDiretor;
    private readonly IDiretorReadOnlyRepository _repoReadDiretor;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly TokenController _tokenController;


    public RegistrarDiretorUseCase(
        IDiretorWriteOnlyRepository repoWriteDiretor,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        TokenController tokenController,
        IDiretorReadOnlyRepository repoReadDiretor)
    {
        _repoWriteDiretor = repoWriteDiretor;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _tokenController = tokenController;
        _repoReadDiretor = repoReadDiretor;
    }

    public async Task<ResponseTokenJson> ExecutarAsync(RequestRegistrarDiretorJson request)
    {
        await ValidarAsync(request);

        var entity = _mapper.Map<Domain.Entities.Diretoria.Diretor>(request);

        await _repoWriteDiretor.AddAsync(entity);
        await _unitOfWork.CommitAsync();

        var token = _tokenController.GerarToken(entity.Email);
        var response = new ResponseTokenJson
        {
            Nome = entity.NomeCompleto,
            Token = token
        };

        return response;
    }



    private async Task ValidarAsync(RequestRegistrarDiretorJson request)
    {
        var validator = new RegistrarDiretorValidator();
        var resultado = validator.Validate(request);

        var existeEmail = await _repoReadDiretor.ExisteEmailAsync(request.Email);

        if (existeEmail)
        {
            resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(request.Email,
                ResourceMensagensDeErro.USUARIO_EMAIL_EXISTENTE));
        }

        if (!resultado.IsValid)
        {
            var mensagensDeErro = resultado.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrosDeValidacaoException(mensagensDeErro);
        }
    }
}