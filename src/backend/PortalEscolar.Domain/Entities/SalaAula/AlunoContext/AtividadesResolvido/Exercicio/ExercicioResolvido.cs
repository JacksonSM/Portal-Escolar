using PortalEscolar.Domain.Entities.SalaAula.ProfessoraContext.AtividadesParaResolver.Execicio;
using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities.SalaAula.AlunoContext.AtividadesResolvido.Exercicio;
public class ExercicioResolvido 
{
    public string Id { get; set; }
    public long ProfessoraId { get; set; }
    public long AlunoId { get; set; }
    public long TurmaId { get; set; }
    public string ExercicioParaResolverId { get; set; }
    public string Nome { get; set; }
    public DateTime DataEntrega { get; set; } = DateTime.UtcNow;
    public Disciplina Disciplina { get; set; }
    public float Nota { get; set; }
    public List<QuestoesExercicioResolvido> Questoes { get; set; }

    public void AtribuirNota(ExercicioParaResolver exercicioParaResolver)
    {
        CorrigirQuestoes(exercicioParaResolver);

        var nota = Questoes.Sum(c => c.Pontos);
        Nota = nota;
    }
    private void CorrigirQuestoes(ExercicioParaResolver exercicioParaResolver)
    {
        foreach (var questao in Questoes)
        {
            var gabarito = exercicioParaResolver.Questoes
                .FirstOrDefault(c => c.Ordem == questao.Ordem);


            if (gabarito.AlternativaCorreta.Equals(questao.AlternativaSelecionada))
            {

                questao.Pontos = gabarito.Pontos;
            }
            else
            {
                questao.Pontos = 0;
            }

        }
    }
}
