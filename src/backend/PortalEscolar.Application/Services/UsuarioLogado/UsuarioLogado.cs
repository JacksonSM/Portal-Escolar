using Microsoft.AspNetCore.Http;
using PortalEscolar.Application.Services.Token;
using PortalEscolar.Domain.Entities.Diretoria;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Aluno;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;

namespace PortalEscolar.Application.Services.UsuarioLogado;
public class UsuarioLogado : IUsuarioLogado
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly TokenController _tokenController;
    private readonly IDiretorReadOnlyRepository _repoReadDiretor;
    private readonly IProfessoraReadOnlyRepository _professoraReadRepo;

    public UsuarioLogado(IHttpContextAccessor httpContextAccessor,
        TokenController tokenController,
        IDiretorReadOnlyRepository repoReadDiretor,
        IProfessoraReadOnlyRepository professoraReadRepo)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenController = tokenController;
        _repoReadDiretor = repoReadDiretor;
        _professoraReadRepo = professoraReadRepo;
    }
    public async Task<Diretor> ObterDiretor()
    {

        var token = ObterTokenRequisicao();

        var emailDiretor = _tokenController.RecuperarEmail(token);

        var diretor = await _repoReadDiretor.ObterPorEmailAsync(emailDiretor);

        return diretor;
    }

    public async Task<Professora> ObterProfessora()
    {
        var token = ObterTokenRequisicao();
        
        var emailProfessora = _tokenController.RecuperarEmail(token);

        var professora = await _professoraReadRepo.ObterPorEmailAsync(emailProfessora);

        return professora;
    }

    private string ObterTokenRequisicao()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();
        return token;
    }
}