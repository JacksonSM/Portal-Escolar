﻿using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;

namespace Utilities.Repositories.Diretor;
public class ProfessoraReadOnlyRepositoryBuilder
{

    private static ProfessoraReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IProfessoraReadOnlyRepository> _repository;

    private ProfessoraReadOnlyRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IProfessoraReadOnlyRepository>();
        }
    }

    public static ProfessoraReadOnlyRepositoryBuilder Instance()
    {
        _instance = new ProfessoraReadOnlyRepositoryBuilder();
        return _instance;
    }
    public ProfessoraReadOnlyRepositoryBuilder ExisteEmail(string email)
    {
        if (!string.IsNullOrEmpty(email))
            _repository.Setup(i => i.ExisteEmailAsync(email)).ReturnsAsync(true);

        return this;
    }
   

    public IProfessoraReadOnlyRepository Build()
    {
        return _repository.Object;
    }

}