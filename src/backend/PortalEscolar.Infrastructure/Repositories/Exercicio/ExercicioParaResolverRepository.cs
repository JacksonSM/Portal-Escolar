using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Enum;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;
using PortalEscolar.Infrastructure.Helpers;
using PortalEscolar.Infrastructure.Mapping.DTOs;

namespace PortalEscolar.Infrastructure.Repositories.Exercicio;
public class ExercicioParaResolverRepository : IExercicioWriteOnlyRepository, IExercicioReadOnlyRepository
{
    private readonly IMongoCollection<ExercicioParaResolverDTO> _exercicioCollection;
    private readonly IMapper _mapper;

    public ExercicioParaResolverRepository(IOptions<ExercicioParaResolverDatabaseSettings> produtoServices,
        IMapper mapper)
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

    public async Task<ExercicioParaResolver> ObterPorId(string id)
    {
        var exercicio = await _exercicioCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

        return _mapper.Map<ExercicioParaResolver>(exercicio);
    }

    public async Task<List<ExercicioParaResolver>> ObterListaExercicios(ObterListaExerciciosQuery query)
    {
        var construtor = Builders<ExercicioParaResolverDTO>.Filter;
        FilterDefinition<ExercicioParaResolverDTO> filtro = construtor.Empty;

        if(query.Disciplina is not null)       
            filtro = construtor.Eq(c => c.Disciplina ,(Disciplina)query.Disciplina);

        if (!string.IsNullOrEmpty(query.Nome))
            filtro = construtor.Where(c => c.Nome.Contains(query.Nome));

        var listaFiltrada = new List<ExercicioParaResolverDTO>();

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
