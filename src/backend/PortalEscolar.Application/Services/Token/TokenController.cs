using Microsoft.IdentityModel.Tokens;
using PortalEscolar.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PortalEscolar.Application.Services.Token;

public class TokenController
{
    private const string EmailAlias = "eml";
    private const string Papel = "ppl";
    private readonly double _duracaoTokenEmMinutos;
    private readonly string _chaveDeSeguranca;

    public TokenController(double duracaoTokenEmMinutos, string chaveDeSeguranca)
    {
        _duracaoTokenEmMinutos = duracaoTokenEmMinutos;
        _chaveDeSeguranca = chaveDeSeguranca;
    }

    public string GerarToken(Usuario usuario)
    {
        var claims = new List<Claim>
        {
            new Claim(EmailAlias, usuario.Email),
            new Claim(Papel, usuario.Papel.ToString())
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_duracaoTokenEmMinutos),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    public string RecuperarEmail(string token)
    {
        var claims = ValidarToken(token);
        return claims.FindFirst(EmailAlias).Value;
    }
    public Domain.Enum.Papel RecuperarPapel(string token)
    {
        var claims = ValidarToken(token);
        var number = claims.FindFirst(Papel).Value;
        var papel = Enum.Parse<Domain.Enum.Papel>(number);
        return papel;
    }

    public ClaimsPrincipal ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

        var claims = tokenHandler.ValidateToken(token, parametrosValidacao, out _);

        return claims;
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var symmetricKey = Convert.FromBase64String(_chaveDeSeguranca);
        return new SymmetricSecurityKey(symmetricKey);
    }
}
