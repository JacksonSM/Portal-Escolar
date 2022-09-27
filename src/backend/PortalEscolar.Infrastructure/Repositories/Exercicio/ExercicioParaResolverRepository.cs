using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Infrastructure.Helpers;
using PortalEscolar.Infrastructure.Mapping.DTOs;

namespace PortalEscolar.Infrastructure.Repositories.Exercicio;
public class ExercicioParaResolverRepository : IExercicioWriteOnlyRepository
{
    private readonly IMongoCollection<ExercicioParaResolverDTO> _exercicioCollection;
    private readonly IMapper _mapper;
    public ExercicioParaResolverRepository(IOptions<ExercicioParaResolverDatabaseSettings> produtoServices, IMapper mapper)
    {
        var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

        _exercicioCollection = mongoDatabase.GetCollection<ExercicioParaResolverDTO>
            (produtoServices.Value.ExercicioCollectionName);
        _mapper = mapper;
    }
    public async Task AdicionarAsync(ExercicioParaResolver exercicio)
    {
        var exercicioDto = _mapper.Map<ExercicioParaResolverDTO>(exercicio);

        await _exercicioCollection.InsertOneAsync(exercicioDto);
    }
}
