using AutoMapper;

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
    }
}
