using Moq;
using PortalEscolar.Domain.Interfaces.Repositories.Diretoria.Diretor;

namespace Utilities.Repositories.Diretor;
public class DiretorWriteOnlyRepositoryBuilder
{
    
        private static DiretorWriteOnlyRepositoryBuilder _instance;
        private readonly Mock<IDiretorWriteOnlyRepository> _repository;

        private DiretorWriteOnlyRepositoryBuilder()
        {
            if (_repository is null)
            {
                _repository = new Mock<IDiretorWriteOnlyRepository>();
            }
        }

        public static DiretorWriteOnlyRepositoryBuilder Instance()
        {
            _instance = new DiretorWriteOnlyRepositoryBuilder();
            return _instance;
        }

        public IDiretorWriteOnlyRepository Build()
        {
            return _repository.Object;
       }
    
}
