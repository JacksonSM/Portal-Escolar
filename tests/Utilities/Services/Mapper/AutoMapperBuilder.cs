using AutoMapper;
using PortalEscolar.Application.Services.Mapping;

namespace Utilitario.ParaOsTestes.Mapper;

public class AutoMapperBuilder
{
    public static IMapper Instance()
    {

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperConfig());
        });
        return mockMapper.CreateMapper();
    }
}
