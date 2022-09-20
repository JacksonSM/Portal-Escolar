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
        CreateMap<Communication.Request.RequestCriarTurmaJson, Domain.Entities.SalaAula.Turma>();
    }
    public void EntityForRequest()
    {
        CreateMap< Domain.Entities.Diretoria.Diretor, Communication.Response.ResponseInfoPessoalDiretorJson>();
    }
}
