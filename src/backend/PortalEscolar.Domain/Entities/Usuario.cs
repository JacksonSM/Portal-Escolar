using PortalEscolar.Domain.Enum;

namespace PortalEscolar.Domain.Entities;
public class Usuario : EntityBase
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public Papel Papel { get; protected set; }

    public Usuario(){}
    public Usuario(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
}
