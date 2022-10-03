using AutoMapper;
using PortalEscolar.Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioParaResolver;
using PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioResolvido;

namespace PortalEscolar.Infrastructure.Mapping;
public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<ExercicioParaResolver, ExercicioParaResolverDoc>().ReverseMap();
        CreateMap<QuestoesExercicioParaResolver, QuestoesExercicioParaResolverDoc>().ReverseMap();
        
        CreateMap<ExercicioResolvido, ExercicioResolvidoDoc>().ReverseMap();
        CreateMap<QuestoesExercicioResolvido, QuestoesExercicioResolvidoDoc>().ReverseMap();
    }
}
