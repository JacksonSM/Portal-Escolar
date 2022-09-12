using PortalEscolar.Application.Services.Token;

namespace Utilities.Services.Token;
public class TokenControllerBuilder
{
    public static TokenController Instance()
    {
        return new TokenController(1000, "dDFPaHE3bWhCelI2UFg0eVo1ejI3Vm4yVmlySnZQS3NAdkAhRTZQXiRDdlhAazRA");
    }

    public static TokenController ExpiredToken()
    {
        return new TokenController(0.0166667, "dDFPaHE3bWhCelI2UFg0eVo1ejI3Vm4yVmlySnZQS3NAdkAhRTZQXiRDdlhAazRA");
    }
}
