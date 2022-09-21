using AutoMapper;

namespace PortalEscolar.Application.Services.Mapping;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Communication.Request.Matricula.DadosAluno, Communication.Request.RequestRegistrarAlunoJson>();

        RequestForEntity();
        EntityForRequest();
    }
    public void RequestForEntity()
    {
        CreateMap<Communication.Request.RequestRegistrarDiretorJson, Domain.Entities.Diretoria.Diretor>();
        CreateMap<Communication.Request.RequestRegistrarProfessoraJson, Domain.Entities.SalaAula.Professora>();
        CreateMap<Communication.Request.RequestRegistrarAlunoJson, Domain.Entities.SalaAula.Aluno>();
        CreateMap<Communication.Request.RequestCriarTurmaJson, Domain.Entities.SalaAula.Turma>();

        CreateMap<Communication.Request.Matricula.RequestMatricularAlunoJson, Domain.Entities.Diretoria.Matricula.Matricula>()
            .ForMember(destino => destino.Aluno, config => config.Ignore()) 
            .ForPath(destino => destino.Responsavel.NomeCompleto, config => config.MapFrom(origem => origem.Responsavel.NomeCompleto))
            .ForPath(destino => destino.Responsavel.DataNascimento, config => config.MapFrom(origem => origem.Responsavel.DataNascimento))
            .ForPath(destino => destino.Responsavel.Telefone, config => config.MapFrom(origem => origem.Responsavel.Telefone))
            .ForPath(destino => destino.Responsavel.CPF, config => config.MapFrom(origem => origem.Responsavel.CPF))
            .ForPath(destino => destino.Responsavel.Cidade, config => config.MapFrom(origem => origem.Responsavel.Cidade));
    }
    public void EntityForRequest()
    {
        CreateMap< Domain.Entities.Diretoria.Diretor, Communication.Response.ResponseInfoPessoalDiretorJson>();
        CreateMap< Domain.Entities.SalaAula.Aluno, Communication.Response.ReponseRegistarAlunoJson>();
    }
}
