using AutoMapper;

namespace PortalEscolar.Application.Services.Mapping;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        RequestForEntity();
        EntityForRequest();
    }
    public void RequestForEntity()
    {
        CreateMap<Communication.Request.RequestRegistrarDiretorJson, Domain.Entities.Diretoria.Diretor>();
        CreateMap<Communication.Request.RequestRegistrarProfessoraJson, Domain.Entities.SalaAula.Professora>();
        CreateMap<Communication.Request.RequestRegistrarAlunoJson, Domain.Entities.SalaAula.Aluno>();
    }
    public void EntityForRequest()
    {
        CreateMap< Domain.Entities.Diretoria.Diretor, Communication.Response.ResponseInfoPessoalDiretorJson>();
    }
}
