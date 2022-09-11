using System.Runtime.Serialization;

namespace PortalEscolar.Exceptions.ExceptionsBase;

[Serializable]
public class ErrosDeValidacaoException : PortalEscolarException
{
    public List<string> MensagensDeErro { get; set; }

    public ErrosDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }

    protected ErrosDeValidacaoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
