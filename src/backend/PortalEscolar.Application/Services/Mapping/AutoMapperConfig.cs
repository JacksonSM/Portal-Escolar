﻿using AutoMapper;
using PortalEscolar.Communication.Request.Aluno;
using PortalEscolar.Communication.Request.Diretor;
using PortalEscolar.Communication.Request.Professora;
using PortalEscolar.Communication.Response.Aluno.ObterListaExercicios;
using PortalEscolar.Domain.Entities.SalaAula.AlunoContext;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext;
using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Exercicio;

namespace PortalEscolar.Application.Services.Mapping;
public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Communication.Request.Matricula.DadosAluno, RequestRegistrarAlunoJson>();
        CreateMap<RequestObterListaExerciciosQuery, ObterListaExerciciosQuery>();


        RequestForEntity();
        EntityForResponse();
    }
    public void RequestForEntity()
    {
        CreateMap<RequestRegistrarDiretorJson, Domain.Entities.Diretoria.Diretor>();
        CreateMap<RequestRegistrarProfessoraJson, Professora>();
        CreateMap<RequestRegistrarAlunoJson, Aluno>();
        CreateMap<RequestCriarTurmaJson, Domain.Entities.SalaAula.Turma>();

        CreateMap<Communication.Request.Exercicio.RequestEnviarExercicioJson, ExercicioParaResolver>();
        CreateMap<Communication.Request.Exercicio.QuestoesExercicioJson, QuestoesExercicioParaResolver>();

        CreateMap<Communication.Request.Matricula.RequestMatricularAlunoJson, Domain.Entities.Diretoria.Matricula.Matricula>()
            .ForMember(destino => destino.Aluno, config => config.Ignore()) 
            .ForPath(destino => destino.Responsavel.NomeCompleto, config => config.MapFrom(origem => origem.Responsavel.NomeCompleto))
            .ForPath(destino => destino.Responsavel.DataNascimento, config => config.MapFrom(origem => origem.Responsavel.DataNascimento))
            .ForPath(destino => destino.Responsavel.Telefone, config => config.MapFrom(origem => origem.Responsavel.Telefone))
            .ForPath(destino => destino.Responsavel.CPF, config => config.MapFrom(origem => origem.Responsavel.CPF))
            .ForPath(destino => destino.Responsavel.Cidade, config => config.MapFrom(origem => origem.Responsavel.Cidade));
    }
    public void EntityForResponse()
    {
        CreateMap< Domain.Entities.Diretoria.Diretor, Communication.Response.ResponseInfoPessoalDiretorJson>();
        CreateMap< Aluno, Communication.Response.ReponseRegistarAlunoJson>();
        CreateMap< ExercicioParaResolver, Communication.Response.Aluno.Exercicio.ExercicioParaResolverJson>();
        CreateMap< QuestoesExercicioParaResolver, Communication.Response.Aluno.Exercicio.QuestoesExercicioParaResolverJson>();
        CreateMap<ExercicioParaResolver, ResponseExercicioParaResolverJson>();
        CreateMap<QuestoesExercicioParaResolver, ResponseQuestoesExercicioParaResolverJson>();
    }
}
