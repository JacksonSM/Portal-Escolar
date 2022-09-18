using AutoMapper;
using System.Globalization;

namespace PortalEscolar.Application.Services.Mapping;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        RequestForEntity();
    }
    public void RequestForEntity()
    {
        CreateMap<Communication.Request.RequestRegistrarDiretorJson,Domain.Entities.Diretoria.Diretor>()
            .ForMember(c => c.DataNascimento, opt => opt.MapFrom(origem => origem.DataNascimento));

        CreateMap<Communication.Request.RequestRegistrarProfessoraJson, Domain.Entities.SalaAula.Professora>()
            .ForMember(c => c.DataNascimento, opt => opt.MapFrom(origem => origem.DataNascimento));

        CreateMap<Communication.Request.RequestRegistrarAlunoJson, Domain.Entities.SalaAula.Aluno>()
            .ForMember(c => c.DataNascimento, opt => opt.MapFrom(origem => origem.DataNascimento));

    }
}
