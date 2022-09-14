using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.SalaAula.Professora;

namespace Utilities.Repositories.Diretor;
public class ProfessoraWriteOnlyRepositoryBuilder
{
    
        private static ProfessoraWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IProfessoraWriteOnlyRepository> _repository;

        private ProfessoraWriteOnlyRepositoryBuilder()
        {
            if (_repository is null)
            {
                _repository = new Mock<IProfessoraWriteOnlyRepository>();
            }
        }

        public static ProfessoraWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new ProfessoraWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IProfessoraWriteOnlyRepository Build()
        {
            return _repository.Object;
       }
    
}
