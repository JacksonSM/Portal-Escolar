using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.ExercicioResolvido;
using PortalEscolar.Infrastructure.Helpers;
using PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioResolvido;

namespace PortalEscolar.Infrastructure.Repositories.ExercicioResolvido;
public class ExercicioResolvidoRepository : IExercicioResolvidoWriteOnlyRepository, IExercicioResolvidoReadOnlyRepository
{
    private readonly IMongoCollection<ExercicioResolvidoDoc> _exercicioCollection;
    private readonly IMapper _mapper;

    public ExercicioResolvidoRepository(IOptions<ExercicioResolvidoDatabaseSettings> produtoServices,
        IMapper mapper)
    {
        var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

        _exercicioCollection = mongoDatabase.GetCollection<ExercicioResolvidoDoc>
            (produtoServices.Value.ExercicioResolvidoCollectionName);
        _mapper = mapper;
    }

    public async Task AdicionarAsync(
        Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio.ExercicioResolvido  exercicioResolvido)
    {
        var exercicioDto = _mapper.Map<ExercicioResolvidoDoc>(exercicioResolvido);

        await _exercicioCollection.InsertOneAsync(exercicioDto);
    }

    public Task<bool> ExercicioEstaResolvido(string exercicioResolvidoId, long alunoId) =>
        _exercicioCollection
            .Find(c => c.ExercicioParaResolverId.Equals(exercicioResolvidoId) &&
            c.AlunoId == alunoId).AnyAsync();
}
