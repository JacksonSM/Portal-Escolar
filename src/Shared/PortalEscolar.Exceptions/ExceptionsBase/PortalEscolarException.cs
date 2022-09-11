using System.Runtime.Serialization;

namespace PortalEscolar.Exceptions.ExceptionsBase;

[Serializable]
public class PortalEscolarException : SystemException
{
    public PortalEscolarException(string mensagem) : base(mensagem)
    {
    }

    protected PortalEscolarException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}