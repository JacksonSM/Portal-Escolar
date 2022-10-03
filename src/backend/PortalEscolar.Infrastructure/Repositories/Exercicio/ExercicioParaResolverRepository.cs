using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Enum;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Infrastructure.Helpers;
using PortalEscolar.Infrastructure.MappingDTOs.DTOs.ExercicioParaResolver;

namespace PortalEscolar.Infrastructure.Repositories.Exercicio;
public class ExercicioParaResolverRepository : IExercicioWriteOnlyRepository, IExercicioReadOnlyRepository
{
    private readonly IMongoCollection<ExercicioParaResolverDoc> _exercicioCollection;
    private readonly IMapper _mapper;

    public ExercicioParaResolverRepository(IOptions<ExercicioParaResolverDatabaseSettings> produtoServices,
        IMapper mapper)
    {
        var mongoClient = new MongoClient(produtoServices.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(produtoServices.Value.DatabaseName);

        _exercicioCollection = mongoDatabase.GetCollection<ExercicioParaResolverDoc>
            (produtoServices.Value.ExercicioCollectionName);
        _mapper = mapper;
    }

    public async Task AdicionarAsync(ExercicioParaResolver exercicio)
    {
        var exercicioDto = _mapper.Map<ExercicioParaResolverDoc>(exercicio);

        await _exercicioCollection.InsertOneAsync(exercicioDto);
    }

    public async Task<ExercicioParaResolver> ObterPorId(string id, long turmaId = 0)
    {
        ExercicioParaResolverDoc exercicio;
        if (turmaId == 0) 
        {
            exercicio = await _exercicioCollection
                .Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }
        else
        {
            exercicio = await _exercicioCollection
                .Find(x => x.Id.Equals(id) && x.TurmaId == turmaId).FirstOrDefaultAsync();
        }

        return _mapper.Map<ExercicioParaResolver>(exercicio);
    }

    public async Task<List<ExercicioParaResolver>> ObterListaExercicios(ObterListaExerciciosQuery query)
    {
        var construtor = Builders<ExercicioParaResolverDoc>.Filter;
        FilterDefinition<ExercicioParaResolverDoc> filtro = construtor.Empty;

        filtro = construtor.Eq(c => c.TurmaId, query.TurmaId);

        if (query.Disciplina is not null)       
            filtro = construtor.Eq(c => c.Disciplina ,(Disciplina)query.Disciplina);

        if (!string.IsNullOrEmpty(query.Nome))
            filtro = construtor.Where(c => c.Nome.Contains(query.Nome));

        var listaFiltrada = new List<ExercicioParaResolverDoc>();

        if (query.PaginaAtual.HasValue)
        {
            listaFiltrada = await _exercicioCollection.Find(filtro)
                .Skip((query.PaginaAtual.Value - 1) * query.ExerciciosPorPagina.Value)
                .Limit(query.ExerciciosPorPagina.Value).ToListAsync();
        }
        else
        {
            listaFiltrada = await _exercicioCollection.Find(filtro).ToListAsync();
        }



        return _mapper.Map<List<ExercicioParaResolver>>(listaFiltrada);
    }

}
