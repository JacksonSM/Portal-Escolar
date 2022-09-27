using AutoMapper;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Infrastructure.Mapping.DTOs;

namespace PortalEscolar.Infrastructure.Mapping;
public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<ExercicioParaResolver, ExercicioParaResolverDTO>().ReverseMap();
        CreateMap<QuestoesExercicioParaResolver, QuestoesExercicioParaResolverDTO>().ReverseMap();
    }
}
